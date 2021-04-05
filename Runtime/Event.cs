﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExSoftware.Events
{
    [System.Serializable]
    public class Event<T> where T : EventModel
    {
        [SerializeField] List<EventInternal> _listeners = new List<EventInternal>();

        public void Catch(Action<T> evt)
        {
            EventInternal test = new EventInternal(evt);
            if (!_listeners.Contains(test))
            {
                _listeners.Add(test);
            }
        }
        public void RemoveCatch(Action<T> evt)
        {
            EventInternal test = new EventInternal(evt);
            _listeners.Remove(test);
        }

        public void Throw(T evt)
        {
            Debug.Log($"{GetType()} Throws {evt.GetType()}");
            for (int i = _listeners.Count - 1; -1 < i; i--)
            {
                if (!_listeners[i].Invoke(evt))
                {
                    _listeners.RemoveAt(i);
                }
            }
        }

        public void Clear() => _listeners.Clear();

        [System.Serializable]
        private class EventInternal
        {
            public System.Action<T> action;
            public UnityEngine.Object target;
            public object otarget;
            public int hash;
            public string type;
            public string method;

            public EventInternal(System.Action<T> action)
            {
                this.action = action;
                foreach (var e in action.GetInvocationList())
                {
                    otarget = e.Target;
                    hash = otarget.GetHashCode();
                    type = e.Target.GetType().ToString();
                    method = e.Method.ToString();

                    if (e.Target is UnityEngine.Object)
                    {
                        target = e.Target as UnityEngine.Object;
                    }
                    else
                    {
                        target = null;
                    }

                }
            }

            public bool Invoke(T data)
            {
                if (otarget == null)
                {
                    Log("Imposible throw event because target is NULL");
                    return false;
                }
                if (action != null)
                {
                    try
                    {
                        action(data);
                    }
                    catch (System.Exception ex)
                    {
                        Log("Event Exception catched ;) ", ex);
                    }
                    return true;
                }

                Log("Imposible throw event because action is NULL");
                return false;
            }

            void Log(string msg, System.Exception ex = null)
            {
                Debug.LogError($"[{typeof(T).Name}] {msg}\ntarget={otarget}\ntype={type}\nmethod={method}");
                if (ex != null) Debug.LogException(ex);
            }


            public static bool operator ==(EventInternal a, EventInternal b)
            {
                bool anull = object.ReferenceEquals(a, null);
                bool bnull = object.ReferenceEquals(b, null);
                if (anull == false && bnull == false)
                {
                    return a.otarget == b.otarget && a.type == b.type && a.method == b.method && a.hash == b.hash;
                }
                else
                {
                    if (anull && bnull)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public static bool operator !=(EventInternal a, EventInternal b)
            {
                bool anull = object.ReferenceEquals(a, null);
                bool bnull = object.ReferenceEquals(b, null);
                if (anull == false && bnull == false)
                {
                    return a.otarget != b.otarget || a.type != b.type || a.method != b.method || a.hash != b.hash;
                }
                else
                {
                    if (anull && bnull)
                    {
                        return false;
                    }
                    return true;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }
                try
                {
                    EventInternal o = (EventInternal)obj;
                    return otarget == o.otarget && type == o.type && hash == o.hash && method == o.method;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                int hashCode = 499090158;
                hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(otarget);
                hashCode = hashCode * -1521134295 + hash.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(type);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(method);
                return hashCode;
            }

        }
    }
}
