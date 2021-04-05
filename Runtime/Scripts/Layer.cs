using System.Collections.Generic;

namespace ExSoftware.Events
{
    [System.Serializable]
    public class Layer
    {
        [UnityEngine.SerializeField]
        public string name;

        [UnityEngine.SerializeField]
        public List<string> events = new List<string>();
    }
}
