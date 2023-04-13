using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

namespace Zetcil
{

    public class VarFile : MonoBehaviour
    {
        public enum COperationType { None, Save, Load }

        [Space(10)]
        public bool isEnabled;

        [Header("File Settings")]
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        public string DirectoryName;
        public string FileName;
        public VarString ContentValue;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay = 0;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval = 0;

        public bool usingEvents;
        public UnityEvent Events;

        void Awake()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake && isEnabled)
            {
                InvokeExecutionData();
            }
        }

        void Start()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnStart && isEnabled)
                {
                    InvokeExecutionData();
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnDelay && isEnabled)
                {
                    Invoke("InvokeExecutionData", Delay);
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnInterval && isEnabled)
                {
                    InvokeRepeating("InvokeExecutionData", Interval, 1);
                }
            }
        }

        void InvokeExecutionData()
        {
            if (ExecutionType == GlobalVariable.CDataExecution.Save)
            {
                SaveFile();
            }
            if (ExecutionType == GlobalVariable.CDataExecution.Load)
            {
                LoadFile();
            }
        }

        string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        public void SaveFile()
        {
            string DirName = GetDirectory(DirectoryName);
            var sr = File.CreateText(DirName + FileName);
            sr.WriteLine(ContentValue.CurrentValue);
            sr.Close();
            if (usingEvents)
            {
                Events.Invoke();
            }
            if (ShowDebugLog)
            {
                Debug.Log("Save data " + this.transform.name + " = " + DirName + FileName);
            }
        }

        public void LoadFile()
        {
            string result = "NULL";
            string FullPathFile = GetDirectory(DirectoryName) + FileName;
            if (File.Exists(FullPathFile))
            {
                string temp = System.IO.File.ReadAllText(FullPathFile);
                result = temp;
            }
            ContentValue.CurrentValue = result;
            if (usingEvents)
            {
                Events.Invoke();
            }
            if (ShowDebugLog)
            {
                Debug.Log("Load data " + this.transform.name + " = " + result);
            }
        }
    }
}
