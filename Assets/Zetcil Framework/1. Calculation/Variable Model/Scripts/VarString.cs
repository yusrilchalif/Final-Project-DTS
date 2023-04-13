/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampung nilai global variabel
 **************************************************************************************************************/

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarString : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        [TextArea(5, 20)]
        public string CurrentValue;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        public bool usingEvents;
        public UnityEvent Events;

        public void SetPrefCurrentValue(string aID)
        {
            PlayerPrefs.SetString(aID, CurrentValue);
        }

        public void GetPrefCurrentValue(string aID)
        {
            CurrentValue = PlayerPrefs.GetString(aID);
        }

        public string GetCurrentValue()
        {
            return CurrentValue;
        }

        public void PrintToText(InputField aValue)
        {
            aValue.text = CurrentValue;
        }

        public void PrintToText(Text aValue)
        {
            aValue.text = CurrentValue;
        }

        public void PrintToText(TextMesh aValue)
        {
            aValue.text = CurrentValue;
        }

        public void SetCurrentValue(string aValue)
        {
            CurrentValue = aValue;
        }

        public void SetCurrentValue(VarString aValue)
        {
            CurrentValue = aValue.CurrentValue;
        }

        public void SetCurrentValue(Text aValue)
        {
            CurrentValue = aValue.text;
        }

        public void SetCurrentValue(InputField aValue)
        {
            CurrentValue = aValue.text;
        }

        public void ClearCurrentValue(float Delay)
        {
            Invoke("ClearThisValue", Delay);
        }

        void ClearThisValue()
        {
            CurrentValue = "";
        }

        public void AddToCurrentValue(string aValue)
        {
            CurrentValue += aValue;
        }

        public void InputToCurrentValue(InputField aValue)
        {
            CurrentValue = aValue.text;
        }

        public void SaveData()
        {
            SetPrefCurrentValue(this.transform.name);
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
            GetPrefCurrentValue(this.transform.name);
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

        void Update()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnUpdate && isEnabled)
            {
                InvokeExecutionData();
            }
        }


    }

}