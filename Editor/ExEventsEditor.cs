using UnityEditor;

namespace ExceptionSoftware.Events
{
    [InitializeOnLoad]
    public class ExEventsEditor
    {
        static ExEventAsset _assets = null;
        public static string EVENTS_PATH = "Assets/ExEvents";
        public static string EVENTS_ASSETS_PATH = "Assets/ExEvents/Layers";
        public static string EVENTS_SCRIPTS_PATH = "Assets/ExEvents/Scripts";
        static ExEventsEditor() => LoadAsset();
        static void LoadAsset()
        {
            if (_assets == null)
            {
                _assets = ExAssets.FindAssetsByType<ExEventAsset>().First();
            }

            if (_assets == null)
            {
                System.IO.Directory.CreateDirectory(EVENTS_PATH);
                System.IO.Directory.CreateDirectory(EVENTS_SCRIPTS_PATH);
                System.IO.Directory.CreateDirectory(EVENTS_ASSETS_PATH);
                _assets = ExAssets.CreateAsset<ExEventAsset>(EVENTS_PATH, "EventsLayers");
            }
        }

        [MenuItem("Tools/Ex Software/Events/Asset")]
        static void SelectAsset()
        {
            LoadAsset();
            Selection.activeObject = _assets;
        }
    }
}
