using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarLanguage)), CanEditMultipleObjects]
    public class VarLanguageEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType,
           LanguageType,
           usingArabic,
           ArabicEvent,
           usingIndonesia,
           IndonesiaEvent,
           usingEnglish,
           EnglishEvent,
           usingJapanese,
           JapaneseEvent,
           usingKorean,
           KoreanEvent,
           usingChinese,
           ChineseEvent,
           usingOther,
           OtherEvent
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            OperationType = serializedObject.FindProperty("OperationType");
            LanguageType = serializedObject.FindProperty("LanguageType");
            usingArabic = serializedObject.FindProperty("usingArabic");
            ArabicEvent = serializedObject.FindProperty("ArabicEvent");
            usingIndonesia = serializedObject.FindProperty("usingIndonesia");
            IndonesiaEvent = serializedObject.FindProperty("IndonesiaEvent");
            usingEnglish = serializedObject.FindProperty("usingEnglish");
            EnglishEvent = serializedObject.FindProperty("EnglishEvent");
            usingJapanese = serializedObject.FindProperty("usingJapanese");
            JapaneseEvent = serializedObject.FindProperty("JapaneseEvent");
            usingKorean = serializedObject.FindProperty("usingKorean");
            KoreanEvent = serializedObject.FindProperty("KoreanEvent");
            usingChinese = serializedObject.FindProperty("usingChinese");
            ChineseEvent = serializedObject.FindProperty("ChineseEvent");
            usingOther = serializedObject.FindProperty("usingOther");
            OtherEvent = serializedObject.FindProperty("OtherEvent");
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
                EditorGUILayout.HelpBox("Data Type: XML", MessageType.Info);
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
                EditorGUILayout.PropertyField(LanguageType, true);
                EditorGUILayout.PropertyField(usingArabic, true);
                if (usingArabic.boolValue)
                {
                    EditorGUILayout.PropertyField(ArabicEvent, true);
                }
                EditorGUILayout.PropertyField(usingIndonesia, true);
                if (usingIndonesia.boolValue)
                {
                    EditorGUILayout.PropertyField(IndonesiaEvent, true);
                }
                EditorGUILayout.PropertyField(usingEnglish, true);
                if (usingEnglish.boolValue)
                {
                    EditorGUILayout.PropertyField(EnglishEvent, true);
                }
                EditorGUILayout.PropertyField(usingJapanese, true);
                if (usingJapanese.boolValue)
                {
                    EditorGUILayout.PropertyField(JapaneseEvent, true);
                }
                EditorGUILayout.PropertyField(usingKorean, true);
                if (usingKorean.boolValue)
                {
                    EditorGUILayout.PropertyField(KoreanEvent, true);
                }
                EditorGUILayout.PropertyField(usingChinese, true);
                if (usingChinese.boolValue)
                {
                    EditorGUILayout.PropertyField(ChineseEvent, true);
                }
                EditorGUILayout.PropertyField(usingOther, true);
                if (usingOther.boolValue)
                {
                    EditorGUILayout.PropertyField(OtherEvent, true);
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