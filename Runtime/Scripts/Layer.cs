using System.Collections.Generic;

namespace ExceptionSoftware.Events
{
    [System.Serializable]
    public class Layer
    {
        [UnityEngine.SerializeField]
        public string name = string.Empty;

        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        string _lastType = string.Empty;

        public string LastType
        {
            get => _lastType;
            set => _lastType = value;
        }


        [UnityEngine.SerializeField]
        public List<Event> events = new List<Event>();

        [System.Serializable]
        public class Event
        {
            [UnityEngine.SerializeField]
            public string name = string.Empty;

            [UnityEngine.SerializeField, UnityEngine.HideInInspector]
            string _lastName = string.Empty;

            public string LastName
            {
                get => _lastName;
                set => _lastName = value;
            }
        }
    }

}
