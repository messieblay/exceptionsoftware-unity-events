using UnityEngine;

namespace ExceptionSoftware.Events
{
    public class EventLayer : ScriptableObject
    {
        private void OnDisable()
        {
            //ExEventsInternal.CleanEvents(this);
        }
        private void OnEnable()
        {
            //ExEventsInternal.CleanEvents(this);
        }
    }
}
