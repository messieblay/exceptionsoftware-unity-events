using UnityEditor;

namespace ExceptionSoftware.Events
{
    [InitializeOnLoad]
    public class ExEventsEditorUtility
    {
        static ExEventAsset _assets = null;

        //public const string destination_path_default = "Assets/0Game/Events/";
        //public const string gamenamespace_default = "Game.Events";

        public const string EVENTS_PATH = "Assets/0Game/Events/";
        public const string EVENTS_ASSETS_PATH = EVENTS_PATH + "Layers";
        public const string EVENTS_SCRIPTS_PATH = EVENTS_PATH + "Scripts";
        static ExEventsEditorUtility() => LoadAsset();


        static void LoadAsset()
        {
            if (!System.IO.Directory.Exists(EVENTS_PATH))
                System.IO.Directory.CreateDirectory(EVENTS_PATH);

            if (!System.IO.Directory.Exists(EVENTS_SCRIPTS_PATH))
                System.IO.Directory.CreateDirectory(EVENTS_SCRIPTS_PATH);

            if (!System.IO.Directory.Exists(EVENTS_ASSETS_PATH))
                System.IO.Directory.CreateDirectory(EVENTS_ASSETS_PATH);


            if (_assets == null)
            {
                _assets = ExAssets.FindAssetsByType<ExEventAsset>().First();
            }

            if (_assets == null)
            {

                _assets = ExAssets.CreateAsset<ExEventAsset>(EVENTS_PATH, "EventsSettings");
            }
        }

        [MenuItem("Game/Events/Select Asset")]
        static void SelectAsset()
        {
            LoadAsset();
            Selection.activeObject = _assets;
        }

    }
}
