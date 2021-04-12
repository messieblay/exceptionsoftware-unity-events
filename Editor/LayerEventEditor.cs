using UnityEditor;
using UnityEngine;

namespace ExceptionSoftware.Events
{
    [CustomPropertyDrawer(typeof(ExceptionSoftware.Events.Layer.Event))]
    public class LayerEventEditor : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            EditorGUI.PropertyField(position, property.FindPropertyRelative("name"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
