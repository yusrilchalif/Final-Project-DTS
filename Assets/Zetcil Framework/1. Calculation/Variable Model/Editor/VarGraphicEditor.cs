using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarGraphic)), CanEditMultipleObjects]
    public class VarGraphicEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType,
           AntiAlias,
           Reflection,
           LightCorrection
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            OperationType = serializedObject.FindProperty("OperationType");
            AntiAlias = serializedObject.FindProperty("AntiAlias");
            Reflection = serializedObject.FindProperty("Reflection");
            LightCorrection = serializedObject.FindProperty("LightCorrection");
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
                EditorGUILayout.HelpBox("Data Type: Grpahic", MessageType.Info);
                EditorGUILayout.Space(10);
                GUILine(20, "1. Operation Settings");
                EditorGUILayout.PropertyField(OperationType, true);
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Initialize)
                {
                    EditorGUILayout.HelpBox("Save Mode: ON", MessageType.Warning);
                }
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Runtime)
                {
                    EditorGUILayout.HelpBox("Publish Mode: ON", MessageType.Info);
                }

                EditorGUILayout.Space(10);
                GUILine(20, "2. Event Settings");
                EditorGUILayout.PropertyField(AntiAlias, true);
                EditorGUILayout.PropertyField(Reflection, true);
                EditorGUILayout.PropertyField(LightCorrection, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}