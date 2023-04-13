using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(VarExp)), CanEditMultipleObjects]
    public class VarExpEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentValue,
            Configuration,
            ExpLevel,
            ExpSetting
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Configuration = serializedObject.FindProperty("Configuration");
            ExpLevel = serializedObject.FindProperty("ExpLevel");
            ExpSetting = serializedObject.FindProperty("ExpSetting");
        }

        void GUILine(int i_height, string aText)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.richText = true;
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            EditorGUI.LabelField(rect, " <b>" + aText + "</b>", style);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.HelpBox("Data Type: Float", MessageType.Info);
                EditorGUILayout.Space(10);
                GUILine(20, "1. Data Settings"); EditorGUILayout.PropertyField(CurrentValue, true);
                EditorGUILayout.PropertyField(Configuration, true);
                if (Configuration.boolValue)
                {
                    EditorGUILayout.PropertyField(ExpLevel, true);
                    EditorGUILayout.PropertyField(ExpSetting, true);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}