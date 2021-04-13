using ExceptionSoftware.ExEditor;
using UnityEditor;
using UnityEngine;

namespace ExceptionSoftware.Events
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
                Unityx.SetDirty(Target);
            }

            if (GUILayout.Button("0 - Update Scripts"))
            {
                foreach (Layer l in Target.layersdefinition)
                {
                    ExEventsCodeGenerator.GenerateLayer(l, Target.gamenamespace);
                }

                EditorUtility.SetDirty(Target);
                AssetDatabase.SaveAssets();

                AssetDatabase.Refresh();
                Debug.Log("Layers updated");
            }

            if (GUILayout.Button("1 - Generate Assets"))
            {
                //System.IO.Directory.CreateDirectory(EVENTS_SCRIPTS_PATH);
                foreach (var layers in ExReflect.GetDerivedClassesAllAsseblys<EventLayer>())
                {
                    if (ExAssets.FindAssetsByType(layers).Count > 0)
                    {
                        continue;
                    }

                    ExAssets.CreateAsset(ExEventsEditorUtility.EVENTS_ASSETS_PATH, layers.Name, layers, false);
                }

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("2 - Assign"))
            {
                Target.layers = ExAssets.FindAssetsByType<EventLayer>();
                Unityx.SetDirty(Target);
                AssetDatabase.SaveAssets();
            }
            ExGUI.Separator();

            GUI.color = Color.green;
            if (GUILayout.Button("3 - Save asset"))
            {
                EditorUtility.SetDirty(Target);
                AssetDatabase.SaveAssets();
            }
            GUI.color = Color.white;
        }
    }
}
