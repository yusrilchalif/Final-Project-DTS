using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarVector3 : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        [ConditionalField("usingVector3")] public Vector3 CurrentValue;
        [ConditionalField("usingVector3")] public VarFloat Vector3X;
        [ConditionalField("usingVector3")] public VarFloat Vector3Y;
        [ConditionalField("usingVector3")] public VarFloat Vector3Z;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        public bool usingEvents;
        public UnityEvent Events;

        [Header("Visualization Settings")]
        public GlobalVariable.CVectorVisualization VectorVisualization;
        // Start is called before the first frame update
        

        public float GetCurrentValueX()
        {
            return Vector3X.CurrentValue;
        }

        public float GetCurrentValueY()
        {
            return Vector3Y.CurrentValue;
        }

        public float GetCurrentValueZ()
        {
            return Vector3Z.CurrentValue;
        }

        public Vector3 GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(Vector3 aValue)
        {
            CurrentValue = aValue;
        }

        public void SetCurrentValueX(float aValue)
        {
            CurrentValue.x = aValue;
        }

        public void SetCurrentValueY(float aValue)
        {
            CurrentValue.y = aValue;
        }

        public void SetCurrentValueZ(float aValue)
        {
            CurrentValue.z = aValue;
        }

        public void AddToCurrentValue(Vector3 aValue)
        {
            CurrentValue += aValue;
        }

        public void AddToCurrentValueX(float aValue)
        {
            CurrentValue.x += aValue;
        }

        public void AddToCurrentValueY(float aValue)
        {
            CurrentValue.y += aValue;
        }

        public void AddToCurrentValueZ(float aValue)
        {
            CurrentValue.z += aValue;
        }

        public void SubFromCurrentValue(Vector3 aValue)
        {
            CurrentValue -= aValue;
        }

        public void SubFromCurrentValueX(float aValue)
        {
            CurrentValue.x -= aValue;
        }

        public void SubFromCurrentValueY(float aValue)
        {
            CurrentValue.y -= aValue;
        }

        public void SubFromCurrentValueZ(float aValue)
        {
            CurrentValue.z -= aValue;
        }

        public void SetPrefCurrentValueX(string aID)
        {
            PlayerPrefs.SetFloat(aID + "_x", CurrentValue.x);
        }

        public void GetPrefCurrentValueX(string aID)
        {
            if (PlayerPrefs.HasKey(aID + "_x"))
            {
                CurrentValue.x = PlayerPrefs.GetFloat(aID + "_x");
            }
        }

        public void SetPrefCurrentValueY(string aID)
        {
            PlayerPrefs.SetFloat(aID + "_y", CurrentValue.y);
        }

        public void GetPrefCurrentValueY(string aID)
        {
            if (PlayerPrefs.HasKey(aID + "_y"))
            {
                CurrentValue.y = PlayerPrefs.GetFloat(aID + "_y");
            }
        }

        public void SetPrefCurrentValueZ(string aID)
        {
            PlayerPrefs.SetFloat(aID + "_z", CurrentValue.z);
        }

        public void GetPrefCurrentValueZ(string aID)
        {
            if (PlayerPrefs.HasKey(aID + "_z"))
            {
                CurrentValue.z = PlayerPrefs.GetFloat(aID + "_z");
            }
        }

        public void SaveData()
        {
            SetPrefCurrentValueX(this.transform.name);
            SetPrefCurrentValueY(this.transform.name);
            SetPrefCurrentValueZ(this.transform.name);
            if (ShowDebugLog)
            {
                Debug.Log("Save data " + this.transform.name + " = " + CurrentValue.ToString());
            }
            if (usingEvents)
            {
                Events.Invoke();
            }
        }

        public void LoadData()
        {
            GetPrefCurrentValueX(this.transform.name);
            GetPrefCurrentValueY(this.transform.name);
            GetPrefCurrentValueZ(this.transform.name);
            if (ShowDebugLog)
            {
                Debug.Log("Load data " + this.transform.name + " = " + CurrentValue.ToString());
            }
            if (usingEvents)
            {
                Events.Invoke();
            }
        }

        public void InvokeExecutionData()
        {
            if (ExecutionType == GlobalVariable.CDataExecution.Save && isEnabled)
            {
                SaveData();
            }
            else if (ExecutionType == GlobalVariable.CDataExecution.Load && isEnabled)
            {
                LoadData();
            }
        }

        void Awake()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake && isEnabled)
            {
                InvokeExecutionData();
            }
        }

        void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnStart && isEnabled)
            {
                InvokeExecutionData();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay && isEnabled)
            {
                if (usingDelay)
                {
                    Invoke("InvokeExecutionData", Delay);
                }
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnInterval && isEnabled)
            {
                if (usingInterval)
                {
                    InvokeRepeating("InvokeExecutionData", 1, Interval);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3X.CurrentValue = CurrentValue.x;
            Vector3Y.CurrentValue = CurrentValue.y;
            Vector3Z.CurrentValue = CurrentValue.z;
            if (VectorVisualization == GlobalVariable.CVectorVisualization.Rotation)
            {
                transform.localRotation = Quaternion.Euler(CurrentValue);
            }
            if (VectorVisualization == GlobalVariable.CVectorVisualization.Position)
            {
                transform.position = CurrentValue;
            }
            if (VectorVisualization == GlobalVariable.CVectorVisualization.Scale)
            {
                transform.localScale = CurrentValue;
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnUpdate && isEnabled)
            {
                InvokeExecutionData();
            }
        }
    }
}
