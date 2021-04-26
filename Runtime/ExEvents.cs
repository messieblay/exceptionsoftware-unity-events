namespace ExceptionSoftware.Events
{

    public static class ExEvents
    {
        static ExEventAsset _assets = null;
        static ExEventAsset Assets
        {
            get
            {
                if (_assets == null)
                {
                    _assets = ExAssets.FindAssetsByType<ExEventAsset>().First();
                }
                return _assets;
            }
        }

        public static T GetLayer<T>() where T : EventLayer
        {
            if (Assets == null) return null;

            var asset = Assets?.layers.Find(s => s.GetType() == typeof(T));
            if (asset == null) return null;

            return asset as T;
        }
        public static EventLayer GetLayer(System.Type type)
        {
            if (Assets == null) return null;

            var asset = Assets?.layers.Find(s => s.GetType() == type);
            if (asset == null) return null;

            return asset;
        }


        public static void Throw(EventModel evt) => ExEventsInternal.Throw(evt);
        public static void Catch<T>(System.Action<T> evt) where T : EventModel => ExEventsInternal.Catch(evt);
        public static void CatchOnce<T>(System.Action<T> evt) where T : EventModel => ExEventsInternal.CatchOnce(evt);
        public static void RemoveCatch<T>(System.Action<T> evt) where T : EventModel => ExEventsInternal.RemoveCatch(evt);
        public static void Clear<T>() where T : EventLayer => ExEventsInternal.Clear<T>();

    }
}
