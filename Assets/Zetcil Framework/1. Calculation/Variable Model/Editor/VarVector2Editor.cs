using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarVector2)), CanEditMultipleObjects]
    public class VarVector2Editor : Editor
    {
        public SerializedProperty
           isEnabled,
           CurrentValue,
           Vector2X,
           Vector2Y,
           VectorVisualization,
           InvokeType,
           ExecutionType,
           ShowDebugLog,
           usingDelay,
           Delay,
           usingInterval,
           Interval,
           usingEvents,
           Events;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentValue = serializedObject.FindProperty("CurrentValue");
            Vector2X = serializedObject.FindProperty("Vector2X");
            Vector2Y = serializedObject.FindProperty("Vector2Y");
            VectorVisualization = serializedObject.FindProperty("VectorVisualization");
            InvokeType = serializedObject.FindProperty("InvokeType");
            ExecutionType = serializedObject.FindProperty("ExecutionType");
            ShowDebugLog = serializedObject.FindProperty("ShowDebugLog");
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
                EditorGUILayout.HelpBox("Data Type: Vector2", MessageType.Info);
                EditorGUILayout.Space(10);

                GUILine(20, "1. Invoke Settings");
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(ExecutionType, true);
                EditorGUILayout.PropertyField(ShowDebugLog, true);

                EditorGUILayout.Space(10);
                GUILine(20, "2. Data Settings");
                EditorGUILayout.PropertyField(CurrentValue);
                EditorGUILayout.PropertyField(Vector2X);
                EditorGUILayout.PropertyField(Vector2Y);
                EditorGUILayout.PropertyField(VectorVisualization);

                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnDelay)
                {
                    EditorGUILayout.PropertyField(usingDelay, true);
                    usingInterval.boolValue = false;
                    if (usingDelay.boolValue)
                    {
                        EditorGUILayout.PropertyField(Delay, true);
                    }
                }
                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnInterval)
                {
                    EditorGUILayout.PropertyField(usingInterval, true);
                    usingDelay.boolValue = false;
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