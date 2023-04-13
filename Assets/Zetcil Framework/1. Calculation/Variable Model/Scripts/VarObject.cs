/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampung nilai global variabel
 **************************************************************************************************************/

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarObject : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public GlobalVariable.CDataExecution ExecutionType;
        public bool ShowDebugLog;

        [ConditionalField("isEnabled")] 
        public GameObject CurrentValue;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        public bool usingEvents;
        public UnityEvent Events;



        public string GetObjectName()
        {
            string objName = "";
            if (CurrentValue) objName = CurrentValue.name;
            return objName;
        }

        public string GetObjectTag()
        {
            string objTag = "";
            if (CurrentValue) objTag = CurrentValue.tag;
            return objTag;
        }

        public void ShowDebugObjectName()
        {
            if (CurrentValue) Debug.Log(CurrentValue.name);
        }

        public void ShowDebugObjectTag()
        {
            if (CurrentValue) Debug.Log(CurrentValue.tag);
        }

        public Vector3 GetObjectPosition()
        {
            Vector3 objPosition = Vector3.zero;
            if (CurrentValue) objPosition = CurrentValue.transform.position;
            return objPosition;
        }

        public Quaternion GetObjectRotation()
        {
            Quaternion objRotation = new Quaternion();
            if (CurrentValue) objRotation = CurrentValue.transform.rotation;
            return objRotation;
        }

        public void SetObjectActive(bool aValue) {
            if (CurrentValue) CurrentValue.SetActive(aValue);
        }

        public void SetObjectPosition(Vector3 aValue)
        {
            if (CurrentValue) CurrentValue.transform.position = aValue; 
        }

        public void SetCurrentValue(GameObject aValue)
        {
            CurrentValue = aValue;
        }

        public void ClearCurrentValue()
        {
            CurrentValue = null;
        }

        public void SetPrefCurrentValue(string aID)
        {
            PlayerPrefs.SetString(aID, "GAMEOBJECT");
            PlayerPrefs.SetFloat(aID + "_posx", CurrentValue.transform.position.x);
            PlayerPrefs.SetFloat(aID + "_posy", CurrentValue.transform.position.y);
            PlayerPrefs.SetFloat(aID + "_posz", CurrentValue.transform.position.z);
            PlayerPrefs.SetFloat(aID + "_rotx", CurrentValue.transform.eulerAngles.x);
            PlayerPrefs.SetFloat(aID + "_roty", CurrentValue.transform.eulerAngles.y);
            PlayerPrefs.SetFloat(aID + "_rotz", CurrentValue.transform.eulerAngles.z);
            PlayerPrefs.SetFloat(aID + "_scalex", CurrentValue.transform.localScale.x);
            PlayerPrefs.SetFloat(aID + "_scaley", CurrentValue.transform.localScale.y);
            PlayerPrefs.SetFloat(aID + "_scalez", CurrentValue.transform.localScale.z);
        }

        public void GetPrefCurrentValue(string aID)
        {
            if (PlayerPrefs.HasKey(aID) && PlayerPrefs.GetString(aID).Equals("GAMEOBJECT"))
            {
                CurrentValue.transform.position = new Vector3(PlayerPrefs.GetFloat(aID + "_posx"),
                                                              PlayerPrefs.GetFloat(aID + "_posy"),
                                                              PlayerPrefs.GetFloat(aID + "_posz"));
                CurrentValue.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat(aID + "_rotx"),
                                                                   PlayerPrefs.GetFloat(aID + "_roty"),
                                                                   PlayerPrefs.GetFloat(aID + "_rotz"));
                CurrentValue.transform.localScale = new Vector3(PlayerPrefs.GetFloat(aID + "_scalex"),
                                                                PlayerPrefs.GetFloat(aID + "_scaley"),
                                                                PlayerPrefs.GetFloat(aID + "_scalez"));
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
