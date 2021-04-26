using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExceptionSoftware.Events
{
    internal class ExEventsInternal
    {
        public static void Catch<T>(System.Action<T> evt) where T : EventModel
        {
            Type t = evt.GetType().GetGenericArguments().FirstOrDefault();
            ReflectThrow thrower = GetReflect(t);
            thrower.catcher.Invoke(thrower.oevt, new object[] { evt });
        }
        public static void CatchOnce<T>(System.Action<T> evt) where T : EventModel
        {
            Type t = evt.GetType().GetGenericArguments().FirstOrDefault();
            ReflectThrow thrower = GetReflect(t);
            thrower.catcherOnce.Invoke(thrower.oevt, new object[] { evt });
        }

        public static void RemoveCatch<T>(System.Action<T> evt) where T : EventModel
        {
            Type t = evt.GetType().GetGenericArguments().FirstOrDefault();
            ReflectThrow thrower = GetReflect(t);
            thrower.remover.Invoke(thrower.oevt, new object[] { evt });
        }

        public static void Throw(EventModel evt)
        {
            Type t = evt.GetType();
            ReflectThrow thrower = GetReflect(t);
            thrower.thrower.Invoke(thrower.oevt, new object[] { evt });
        }
        public static void Clear<T>() where T : EventLayer
        {
            Type t = typeof(T);
            ReflectThrow thrower = GetReflect(t);
            thrower.clear.Invoke(thrower.oevt, new object[] { });
        }

        static ReflectThrow GetReflect(Type t)
        {
            if (!_cachedThrows.TryGetValue(t, out ReflectThrow thrower))
            {
                Type owner = ExReflect.GetDerivedClassesAllAsseblys<EventLayer>().Where(s => s.GetNestedTypes().Where(s => s == t).Count() > 0).FirstOrDefault();
                EventLayer layer = ExEvents.GetLayer(owner);

                object oevt = layer.GetType().GetFields().Where(s => s.FieldType.IsGenericType && s.FieldType.GetGenericArguments().Contains(t)).FirstOrDefault().GetValue(layer);

                _cachedThrows.Add(t, thrower = new ReflectThrow(oevt, oevt.GetType().GetMethod("Throw"), oevt.GetType().GetMethod("Catch"), oevt.GetType().GetMethod("CatchOnce"), oevt.GetType().GetMethod("RemoveCatch"), oevt.GetType().GetMethod("Clear")));
            }
            return thrower;
        }

        public static void CleanEvents(EventLayer layer)
        {
            foreach (var obj in layer.GetType().GetFields().Where(s => s.FieldType.IsGenericType))
            {
                Type t = obj.FieldType;
                MethodInfo method = t.GetMethod("Clear");
                method.Invoke(obj.GetValue(layer), null);
            }
        }

        static Dictionary<Type, ReflectThrow> _cachedThrows = new Dictionary<Type, ReflectThrow>();
        struct ReflectThrow
        {
            public object oevt;
            public MethodInfo thrower;
            public MethodInfo catcher;
            public MethodInfo catcherOnce;
            public MethodInfo remover;
            public MethodInfo clear;

            public ReflectThrow(object oevt, MethodInfo thrower, MethodInfo catcher, MethodInfo catcherOnce, MethodInfo remover, MethodInfo clear)
            {
                this.oevt = oevt;
                this.thrower = thrower;
                this.catcher = catcher;
                this.catcherOnce = catcherOnce;
                this.remover = remover;
                this.clear = clear;
            }
        }

    }
}
