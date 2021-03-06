namespace ExceptionSoftware.Events
{
    public class EventModel<T> : EventModel where T : EventModel
    {
        public static T Create() => System.Activator.CreateInstance<T>();
        public static T Create(params object[] objs) => System.Activator.CreateInstance(typeof(T), objs) as T;
        public static void CreateAndThrow() => (System.Activator.CreateInstance<T>() as EventModel<T>).Throw();
        public void Throw() => ExEvents.Throw(this as EventModel);
        public static void Catch(System.Action<T> action) => ExEvents.Catch<T>(action);
        public static void CatchOnce(System.Action<T> action) => ExEvents.CatchOnce<T>(action);
        public static void RemoveCatch(System.Action<T> action) => ExEvents.RemoveCatch<T>(action);

    }

    public class EventModel { }
}
