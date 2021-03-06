using System.Collections.Generic;
using UnityEngine;

namespace ExceptionSoftware.Events
{
    [System.Serializable]
    public class ExEventAsset : SettingsAsset
    {
        [Header("Runtime")]
        [SerializeField] public List<EventLayer> layers = new List<EventLayer>();


        [Header("Editor Design")]
        public const string folder_default = "Assets/Events/";
        public const string gamenamespace_default = "Game.Events";

        [SerializeField, HideInInspector] public string gamenamespace_generated = string.Empty;
        [SerializeField] public string gamenamespace = gamenamespace_default;

        [SerializeField] public List<Layer> layersdefinition = new List<Layer>();

        private void OnValidate()
        {
            ValidateNamespace();
            ValidateLayers();
        }


        void ValidateNamespace()
        {
            gamenamespace = gamenamespace.Trim();
            if (gamenamespace == string.Empty)
            {
                gamenamespace = gamenamespace_default;
            }
        }

        void ValidateLayers()
        {
            foreach (var layer in layersdefinition)
            {
                layer.name = layer.name.ToNoSpacesSentence();
                for (int i = 0; i < layer.events.Count; i++)
                {
                    layer.events[i].name = layer.events[i].name.ToNoSpacesSentence();
                }
            }
        }

    }
}
