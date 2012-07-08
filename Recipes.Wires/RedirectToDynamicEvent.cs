using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Recipes.Wires
{
    /// <summary>
    /// Creates convention-based routing rules
    /// </summary>
    public sealed class RedirectToDynamicEvent
    {
        public readonly IDictionary<Type, List<Wire>> Dict = new Dictionary<Type, List<Wire>>();

        public sealed class Wire
        {
            public MethodInfo Method;
            public object Instance;
        }

        static readonly MethodInfo InternalPreserveStackTraceMethod =
            typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);


        public void WireToWhen(object o)
        {
            var infos = o.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1);

            foreach (var methodInfo in infos)
            {
                var type = methodInfo.GetParameters().First().ParameterType;

                List<Wire> list;
                if (!Dict.TryGetValue(type, out list))
                {
                    list = new List<Wire>();
                    Dict.Add(type, list);
                }
                list.Add(new Wire
                    {
                        Instance = o,
                        Method = methodInfo
                    });
            }
        }

        [DebuggerNonUserCode]
        public void InvokeEvent(object @event)
        {
            List<Wire> info;
            var type = @event.GetType();
            if (!Dict.TryGetValue(type, out info))
            {
                //Trace.WriteLine(string.Format("Discarding {0} - failed to locate event handler", type.Name));
                return;
            }
            try
            {
                foreach (var wire in info)
                {
                    wire.Method.Invoke(wire.Instance, new[] {@event});
                }
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                throw ex.InnerException;
            }
        }
    }


    public sealed class RedirectToCommand
    {
        public readonly IDictionary<Type, Action<object>> Dict = new Dictionary<Type, Action<object>>();


        static readonly MethodInfo InternalPreserveStackTraceMethod =
            typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);


        public void WireToWhen(object o)
        {
            var infos = o.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1);

            foreach (var methodInfo in infos)
            {
                var type = methodInfo.GetParameters().First().ParameterType;

                var info = methodInfo;
                Dict.Add(type, message => info.Invoke(o, new[] {message}));
            }
        }

        public void WireToLambda<T>(Action<T> handler)
        {
            Dict.Add(typeof(T), o => handler((T) o));
        }

        [DebuggerNonUserCode]
        public void Invoke(object message)
        {
            Action<object> handler;
            var type = message.GetType();
            if (!Dict.TryGetValue(type, out handler))
            {
                //Trace.WriteLine(string.Format("Discarding {0} - failed to locate event handler", type.Name));
            }
            try
            {
                handler(message);
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                throw ex.InnerException;
            }
        }
    }
}