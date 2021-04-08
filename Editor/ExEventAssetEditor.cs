using ExSoftware.ExEditor;
using UnityEditor;
using UnityEngine;

namespace ExSoftware.Events
{
    [CustomEditor(typeof(ExEventAsset))]
    public class ExEventAssetEditor : ExEditor<ExEventAsset>
    {
        protected override void DoInspector()
        {
            EditorGUI.BeginChangeCheck();
            {
                DrawDefaultInspector();
            }
            if (EditorGUI.EndChangeCheck())
            {

            }

            if (GUILayout.Button("Update Scripts"))
            {
                foreach (Layer l in Target.layersdefinition)
                {
                    ExEventsCodeGenerator.GenerateLayer(l, Target.gamenamespace);
                }

                EditorUtility.SetDirty(Target);

                AssetDatabase.Refresh();
                Debug.Log("Layers updated");
            }

            if (GUILayout.Button("Generate Assets"))
            {
                //System.IO.Directory.CreateDirectory(EVENTS_SCRIPTS_PATH);
                foreach (var layers in ExReflect.GetDerivedClassesAllAsseblys<EventLayer>())
                {
                    if (ExAssets.FindAssetsByType(layers).Count > 0)
                    {
                        continue;
                    }

                    ExAssets.CreateAsset(ExEventsEditor.EVENTS_PATH, layers.Name, layers, false);
                }

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Assign"))
            {
                Target.layers = ExAssets.FindAssetsByType<EventLayer>();
                Unityx.SetDirty(Target);
            }

            ExGUI.Separator();
            if (GUILayout.Button("Migrate old events to new"))
            {
                foreach (var l in Target.layersdefinition)
                {
                    l.eventsnew.Clear();
                    foreach (var e in l.events)
                    {
                        l.eventsnew.Add(new Layer.Event() { name = e });
                    }
                }
                Unityx.SetDirty(Target);
            }

        }
    }
}
