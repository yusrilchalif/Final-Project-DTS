using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarJSON)), CanEditMultipleObjects]
    public class VarJSONEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            JSONValue,
            JSONRoot,
            CurrentRootIndex,
            CurrentItemIndex,
            CurrentKeyword
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            JSONValue = serializedObject.FindProperty("JSONValue");
            JSONRoot = serializedObject.FindProperty("JSONRoot");
            CurrentRootIndex = serializedObject.FindProperty("CurrentRootIndex");
            CurrentItemIndex = serializedObject.FindProperty("CurrentItemIndex");
            CurrentKeyword = serializedObject.FindProperty("CurrentKeyword");
        }

        void GUILine(int i_height, string aText)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.richText = true;
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.4f, 0.4f, 0.4f, 1));
            EditorGUI.LabelField(rect, " <b>" + aText + "</b>", style);
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.HelpBox("Data Type: JSON", MessageType.Info);
                EditorGUILayout.Space(10);
                GUILine(20, "1. Data Settings");
                EditorGUILayout.PropertyField(JSONValue, true);
                EditorGUILayout.PropertyField(JSONRoot, true);

                EditorGUILayout.Space(10); 
                GUILine(20, "2. Index Status");
                EditorGUILayout.PropertyField(CurrentRootIndex, true);
                EditorGUILayout.PropertyField(CurrentItemIndex, true);
                EditorGUILayout.PropertyField(CurrentKeyword, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}