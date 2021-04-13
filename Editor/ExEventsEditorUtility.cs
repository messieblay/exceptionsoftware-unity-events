using UnityEditor;

namespace ExceptionSoftware.Events
{
    [InitializeOnLoad]
    public class ExEventsEditorUtility
    {
        static ExEventAsset _assets = null;

        public const string destination_path_default = "Assets/0Game/";
        public const string gamenamespace_default = "Game.Events";

        public const string EVENTS_PATH_BASE = "Assets/0Game/";
        public const string EVENTS_PATH = "Events";
        public const string EVENTS_ASSETS_PATH = "Layers";
        public const string EVENTS_SCRIPTS_PATH = "Scripts";
        static ExEventsEditorUtility() => LoadAsset();


        static void LoadAsset()
        {
            if (_assets == null)
            {
                _assets = ExAssets.FindAssetsByType<ExEventAsset>().First();
            }

            if (_assets == null)
            {
                System.IO.Directory.CreateDirectory(EVENTS_PATH_BASE + EVENTS_PATH);
                System.IO.Directory.CreateDirectory(EVENTS_PATH_BASE + EVENTS_SCRIPTS_PATH);
                System.IO.Directory.CreateDirectory(EVENTS_PATH_BASE + EVENTS_ASSETS_PATH);
                _assets = ExAssets.CreateAsset<ExEventAsset>(EVENTS_PATH_BASE + EVENTS_PATH, "EventsLayers");
            }
        }

        //[MenuItem("Game/Events/Create Asset")]
        //static void CreateAsset()
        //{
        //    SelectAsset();
        //}

        [MenuItem("Game/Events/Select Asset")]
        static void SelectAsset()
        {
            LoadAsset();
            Selection.activeObject = _assets;
        }

    }
}
