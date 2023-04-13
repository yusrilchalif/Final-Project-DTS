using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageCapture : MonoBehaviour
{
    [Header("Capture Setting")]
    public string Path;
    public int Resolution;
    string filename;

    [Header("UI Objects Setting")]
    public List<GameObject> UIObjects;

    [Header("After Capture Setting")]
    public UnityEvent AfterCaptureEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void WaitCapture()
    {
        filename = Path + "/" + DateTime.Now.ToString("MM_dd_yyyy_h_mm_ss") + ".jpg";
        ScreenCapture.CaptureScreenshot(filename, Resolution);
        Debug.Log(filename);
    }

    void ShowObjects()
    {
        SetStatusObjects(true);
    }

    void HideObjects()
    {
        SetStatusObjects(false);
    }

    void SetStatusObjects(bool aStatus)
    {
        for (int i = 0; i < UIObjects.Count; i++)
        {
            UIObjects[i].SetActive(aStatus);
        }
    }

    public void InvokeCameraCapture()
    {
        HideObjects();
        WaitCapture();
        Invoke("ShowObjects", 1);
        AfterCaptureEvent.Invoke();
    }
}


