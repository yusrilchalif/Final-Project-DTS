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

    public class VarTime : MonoBehaviour
    {
        public enum CTimeCalculation { None, Increment, Decrement }

        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        [ConditionalField("isEnabled")] public int CurrentValue;

        [Header("Time Settings")]
        [ConditionalField("isEnabled")] public CTimeCalculation TimeCalculation;

        [Header("Constraint Settings")]
        [ConditionalField("isEnabled")] public bool usingConstraint;
        [ConditionalField("usingConstraint")] public int MinValue;
        [ConditionalField("usingConstraint")] public int MaxValue;

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
            PlayerPrefs.SetInt(aID, CurrentValue);
        }

        public void GetPrefCurrentValue(string aID)
        {
            if (PlayerPrefs.HasKey(aID))
            {
                CurrentValue = PlayerPrefs.GetInt(aID);
            }
        }

        void ExecuteIncrement()
        {
            if (isEnabled)
            {
                CurrentValue = CurrentValue + 1;
                if (usingConstraint && CurrentValue >= MaxValue)
                {
                    isEnabled = false;
                    CancelInvoke();
                }
            }
        }

        void ExecuteDecrement()
        {
            if (isEnabled)
            {
                CurrentValue = CurrentValue - 1;
                if (usingConstraint && CurrentValue <= MinValue)
                {
                    isEnabled = false;
                    CancelInvoke();
                }
            }
        }


        public void OutputFromCurrentValue(InputField aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        public void OutputFromCurrentValue(Text aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        public void OutputFromCurrentValue(TextMesh aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        void ActivateTimer()
        {
            if (MaxValue == 0)
            {
                MaxValue = CurrentValue;
            }

            if (TimeCalculation == CTimeCalculation.Increment)
            {
                InvokeRepeating("ExecuteIncrement", 1, 1);
            }
            if (TimeCalculation == CTimeCalculation.Decrement)
            {
                InvokeRepeating("ExecuteDecrement", 1, 1);
            }
        }

        public void StartTimer()
        {
            isEnabled = true;
            ActivateTimer();
        }

        public void StopTimer()
        {
            isEnabled = false;
            CancelInvoke();
        }

        public int GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(int aValue)
        {
            CurrentValue = aValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(int aValue)
        {
            CurrentValue += aValue;
            if (usingConstraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
        }

        public void SubFromCurrentValue(int aValue)
        {
            CurrentValue -= aValue;
            if (usingConstraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public bool IsShutdown()
        {
            return CurrentValue <= 0;
        }

        public void InputToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.IntegerNumber)
            {
                CurrentValue = int.Parse(aValue.text);
            }
            else
            {
                Debug.Log("Error type: Invalid InputField.ContentType.IntegerNumber");
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
            if (isEnabled)
            {
                ActivateTimer();
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
