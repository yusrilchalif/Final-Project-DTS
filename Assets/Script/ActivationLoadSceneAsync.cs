using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ActivationLoadSceneAsync : MonoBehaviour
{
    [Header("Loading Screen")]
    public LoadSceneMode LoadingType;
    public string LoadingScene;
    string TargetScene;

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    [Header("Delay Settings")]
    public bool usingDelay;
    public float Delay;
    public UnityEvent DelayEvents;

    // Start is called before the first frame update
    void Start()
    {
        StartEvents?.Invoke();
        if (usingDelay)
        {
            Invoke("LoadScene", Delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
    }

    void LoadScene()
    {
        DelayEvents?.Invoke();
    }

    public void LoadScene(string aValue)
    {
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
        TargetScene = aValue;
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        SceneManager.LoadSceneAsync(LoadingScene, LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(TargetScene, LoadingType);

        while (!asyncLoad.isDone)
        {
            PlayerPrefs.SetFloat("Loading", asyncLoad.progress);
            yield return null;
        }

        SceneManager.UnloadSceneAsync(LoadingScene);
    }

}
