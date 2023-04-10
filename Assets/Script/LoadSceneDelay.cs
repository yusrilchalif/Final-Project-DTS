using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDelay : MonoBehaviour
{

    [Header("Main Settings")]
    public string TargetScene;
    public float Delay;

    void LoadScene()
    {
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
        SceneManager.LoadScene(TargetScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", Delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
