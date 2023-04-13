using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarVector2 : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        
        public Vector2 CurrentValue;
        [Header("Vector Details")]
        public VarFloat Vector2X;
        public VarFloat Vector2Y;

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

        public float GetCurrentValueX()
        {
            return Vector2X.CurrentValue;
        }

        public float GetCurrentValueY()
        {
            return Vector2Y.CurrentValue;
        }

        public Vector2 GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(Vector2 aValue)
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

        public void AddToCurrentValue(Vector2 aValue)
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

        public void SubFromCurrentValue(Vector2 aValue)
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

        public void SetPrefCurrentValueX(string aID)
        {
            PlayerPrefs.SetFloat(aID+"_x", CurrentValue.x);
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

        public void SaveData()
        {
            SetPrefCurrentValueX(this.transform.name);
            SetPrefCurrentValueY(this.transform.name);
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
            Vector2X.CurrentValue = CurrentValue.x;
            Vector2Y.CurrentValue = CurrentValue.y;
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
