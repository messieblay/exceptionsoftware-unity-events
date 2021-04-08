using System.Collections.Generic;

namespace ExSoftware.Events
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
        public List<string> events = new List<string>();


        [UnityEngine.SerializeField]
        public List<Event> eventsnew = new List<Event>();

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
