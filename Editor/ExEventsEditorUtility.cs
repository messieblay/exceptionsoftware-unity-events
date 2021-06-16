using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ExceptionSoftware.Events
{
    [InitializeOnLoad]
    public class ExEventsEditorUtility
    {
        static ExEventAsset _settings = null;

        public const string EVENTS_PATH = ExConstants.SETTINGS_PATH + "Events/";
        public const string EVENTS_PATH_RESOURCES = EVENTS_PATH + "Resources/";
        public const string EVENTS_ASSETS_PATH = EVENTS_PATH + "Layers";
        public const string EVENTS_SCRIPTS_PATH = EVENTS_PATH + "Scripts";
        public const string EVENTS_SETTINGS_FILENAME = "ExEventsSettings";
        static ExEventsEditorUtility() => LoadAsset();


        static void LoadAsset()
        {
            if (!System.IO.Directory.Exists(EVENTS_PATH))
                System.IO.Directory.CreateDirectory(EVENTS_PATH);

            if (!System.IO.Directory.Exists(EVENTS_SCRIPTS_PATH))
                System.IO.Directory.CreateDirectory(EVENTS_SCRIPTS_PATH);

            if (!System.IO.Directory.Exists(EVENTS_ASSETS_PATH))
                System.IO.Directory.CreateDirectory(EVENTS_ASSETS_PATH);

            if (!System.IO.Directory.Exists(EVENTS_PATH_RESOURCES))
                System.IO.Directory.CreateDirectory(EVENTS_PATH_RESOURCES);


            if (_settings == null)
            {
                _settings = ExAssets.FindAssetsByType<ExEventAsset>().FirstOrDefault();
            }

            if (_settings == null)
            {
                _settings = Resources.FindObjectsOfTypeAll<ExEventAsset>().FirstOrDefault();
            }

            if (_settings == null)
            {
                _settings = ExAssets.CreateAsset<ExEventAsset>(EVENTS_PATH, EVENTS_SETTINGS_FILENAME);
            }
        }

        [MenuItem("Tools/Events/Settings", priority = ExConstants.MENU_ITEM_PRIORITY)]
        static void SelectAsset()
        {
            LoadAsset();
            Selection.activeObject = _settings;
        }

    }
}
