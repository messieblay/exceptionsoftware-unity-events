﻿using System.Collections.Generic;
using UnityEngine;

namespace ExceptionSoftware.Events
{
    [System.Serializable]
    public class ExEventAsset : ScriptableObject
    {
        [Header("Runtime")]
        [SerializeField] public List<EventLayer> layers = new List<EventLayer>();


        [Header("Editor Design")]
        [SerializeField] public string gamenamespace = "Game.Events";
        [SerializeField] public List<Layer> layersdefinition = new List<Layer>();

        private void OnValidate()
        {
            gamenamespace = gamenamespace.Trim();
            if (gamenamespace == string.Empty)
            {
                gamenamespace = "Game.Events";
            }

            foreach (var layer in layersdefinition)
            {
                layer.name = layer.name.ToSentence();
                for (int i = 0; i < layer.events.Count; i++)
                {
                    layer.events[i].name = layer.events[i].name.ToSentence();
                }
            }
        }
    }
}
