using System;

namespace Audit.Views
{
    public sealed class ViewMapInfo
    {
        public readonly Type[] Events;
        public readonly Type Projection;
        public readonly Type ViewType;
        public readonly Type KeyType;


        public ViewMapInfo(Type viewType, Type keyType, Type projection, Type[] events)
        {
            ViewType = viewType;
            Projection = projection;
            Events = events;
            KeyType = keyType;
        }
    }
}