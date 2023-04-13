using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarAudio)), CanEditMultipleObjects]
    public class VarAudioEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType,
           SoundVolume,
           TargetSlider
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            OperationType = serializedObject.FindProperty("OperationType");
            SoundVolume = serializedObject.FindProperty("SoundVolume");
            TargetSlider = serializedObject.FindProperty("TargetSlider");
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
                EditorGUILayout.HelpBox("Data Type: Audio", MessageType.Info);
                EditorGUILayout.Space(10);
                GUILine(20, "1. Operation Settings");
                EditorGUILayout.PropertyField(OperationType, true);
                if ((VarAudio.COperationType) OperationType.enumValueIndex == VarAudio.COperationType.Initialize)
                {
                    EditorGUILayout.HelpBox("Save Mode: ON", MessageType.Warning);
                }
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Runtime)
                {
                    EditorGUILayout.HelpBox("Publish Mode: ON", MessageType.Info);
                }

                EditorGUILayout.Space(10);
                GUILine(20, "2. Event Settings");
                EditorGUILayout.PropertyField(SoundVolume, true);
                EditorGUILayout.PropertyField(TargetSlider, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}