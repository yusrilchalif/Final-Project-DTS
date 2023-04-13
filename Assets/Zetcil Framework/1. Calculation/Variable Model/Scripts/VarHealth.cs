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
    public class VarHealth : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        [ConditionalField("isEnabled")]
        public float CurrentValue;

        [Header("Constraint Settings")]
        public bool usingConstraint;
        [ConditionalField("usingConstraint")] public float MinValue;
        [ConditionalField("usingConstraint")] public float MaxValue;

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
            PlayerPrefs.SetFloat(aID, CurrentValue);
        }

        public void GetPrefCurrentValue(string aID)
        {
            if (PlayerPrefs.HasKey(aID))
            {
                CurrentValue = PlayerPrefs.GetFloat(aID);
            }
        }

        public float GetMinValue()
        {
            return MinValue;
        }

        public float GetMaxValue()
        {
            return MaxValue;
        }

        public void SetMinValue(float aValue)
        {
            MinValue = aValue;
        }

        public void SetMaxValue(float aValue)
        {
            MaxValue = aValue;
        }


        public float GetCurrentValue()
        {
            return CurrentValue;
        }

        public void GetCurrentValue(InputField aValue)
        {
            aValue.text = CurrentValue.ToString();
        }
        public void PrintToText(InputField aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        public void PrintToText(Text aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        public void PrintToText(TextMesh aValue)
        {
            aValue.text = CurrentValue.ToString();
        }


        public void SetCurrentValue(float aValue)
        {
            CurrentValue = aValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SetCurrentValue(VarFloat aValue)
        {
            CurrentValue = aValue.CurrentValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SetCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue = float.Parse(aValue.text);
            }
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(float aValue)
        {
            CurrentValue += aValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
        }

        public void AddToCurrentValue(VarFloat aValue)
        {
            CurrentValue += aValue.CurrentValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue += float.Parse(aValue.text);
            }
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SubFromCurrentValue(float aValue)
        {
            CurrentValue -= aValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SubtractFromCurrentValue(VarFloat aValue)
        {
            CurrentValue -= aValue.CurrentValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SubtractFromCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue -= float.Parse(aValue.text);
            }
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public bool IsShutdown()
        {
            return CurrentValue <= 0;
        }

        public void InputToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue = float.Parse(aValue.text);
            }
            else
            {
                Debug.Log("Error type: Invalid InputField.ContentType.DecimalNumber");
            }
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
            if (MaxValue == 0)
            {
                MaxValue = CurrentValue;
            }
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