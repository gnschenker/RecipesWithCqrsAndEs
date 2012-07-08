using System;
using System.Diagnostics;

namespace Recipes.Wires
{
    public static class Context
    {
        [ThreadStatic] static Action<string> _explanations;

        static Action<string> _globalTrace;

        public static void Debug(string format, params object[] args)
        {
            if (_globalTrace != null)
            {
                _globalTrace(string.Format(format, args));
                return;
            }
            Trace.WriteLine(string.Format(format, args));
        }

        public static void Explain(string format, params object[] args)
        {
            if (null != _explanations)
            {
                _explanations(string.Format(format, args));
                return;
            }


            if (_globalTrace != null)
            {
                _globalTrace(string.Format(format, args));
                return;
            }
            Trace.WriteLine(string.Format(format, args));
        }

        public static Action<string> SwapFor(Action<string> builder = null)
        {
            var old = _explanations;
            _explanations = builder;
            return old;
        }

        public static void SwapForDebug(Action<string> builder = null)
        {
            _globalTrace = builder;
        }
    }
}