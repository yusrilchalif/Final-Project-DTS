using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarFile)), CanEditMultipleObjects]
    public class VarFileEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           ExecutionType,
           DirectoryName,
           FileName,
           ContentValue,
           ShowDebugLog,
           usingDelay,
           Delay,
           usingInterval,
           Interval,
           usingEvents,
		   Events
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            ExecutionType = serializedObject.FindProperty("ExecutionType");
            DirectoryName = serializedObject.FindProperty("DirectoryName");
            FileName = serializedObject.FindProperty("FileName");
            ShowDebugLog = serializedObject.FindProperty("ShowDebugLog");
            ContentValue = serializedObject.FindProperty("ContentValue");
            usingDelay = serializedObject.FindProperty("usingDelay");
            Delay = serializedObject.FindProperty("Delay");
            usingInterval = serializedObject.FindProperty("usingInterval");
            Interval = serializedObject.FindProperty("Interval");
            usingEvents = serializedObject.FindProperty("usingEvents");
            Events = serializedObject.FindProperty("Events");
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
                EditorGUILayout.HelpBox("Data Type: File", MessageType.Info);
                EditorGUILayout.Space(10);

                GUILine(20, "1. Invoke Settings");
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(ExecutionType, true);
                EditorGUILayout.PropertyField(ShowDebugLog, true);

                EditorGUILayout.Space(10);
                GUILine(20, "2. File Settings");
                EditorGUILayout.PropertyField(DirectoryName, true);
                if (DirectoryName.stringValue.Length == 0)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(FileName, true);
                if (FileName.stringValue.Length == 0)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ContentValue, true);
                if (!ContentValue.objectReferenceValue)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }

                if (GlobalVariable.CInvokeType.OnDelay == (GlobalVariable.CInvokeType)InvokeType.enumValueIndex)
                {
                    EditorGUILayout.PropertyField(usingDelay, true);
                    if (usingDelay.boolValue)
                    {
                        EditorGUILayout.PropertyField(Delay, true);
                    }
                }
                if (GlobalVariable.CInvokeType.OnInterval == (GlobalVariable.CInvokeType)InvokeType.enumValueIndex)
                {
                    EditorGUILayout.PropertyField(usingInterval, true);
                    if (usingInterval.boolValue)
                    {
                        EditorGUILayout.PropertyField(Interval, true);
                    }
                }
                EditorGUILayout.Space(10);
                GUILine(20, "3. Events Settings");
                EditorGUILayout.PropertyField(usingEvents, true);
                if (usingEvents.boolValue)
                {
                    EditorGUILayout.PropertyField(Events, true);
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