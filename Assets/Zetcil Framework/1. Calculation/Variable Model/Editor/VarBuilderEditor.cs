using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarBuilder)), CanEditMultipleObjects]
    public class VarBuilderEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           ScriptFileName,
           ScriptVariables,
           UseDialogError,
           ResultScript
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            ScriptFileName = serializedObject.FindProperty("ScriptFileName");
            ScriptVariables = serializedObject.FindProperty("ScriptVariables");
            UseDialogError = serializedObject.FindProperty("UseDialogError");
            ResultScript = serializedObject.FindProperty("ResultScript");
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

            Color saved = GUI.color;

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.HelpBox("Data Type: Class", MessageType.Info); 
                EditorGUILayout.Space(10);
                
                GUILine(20, "1. Operation Settings");
                EditorGUILayout.PropertyField(ScriptFileName);

                EditorGUILayout.PropertyField(ScriptVariables);

                EditorGUILayout.Space(10);
                GUILine(20, "2. Event Settings");
                EditorGUILayout.PropertyField(UseDialogError);

                GUI.color = Color.green;
                EditorGUILayout.PropertyField(ResultScript);
                GUI.color = saved;

            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}