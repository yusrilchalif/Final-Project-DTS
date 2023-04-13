using UnityEngine;
using UnityEngine.UI;

public class ImageCamera : MonoBehaviour
{
    [Header("WebCamera Testing")]
    private WebCamTexture webCameraTexture;
    private RawImage rawImage;

    void Start()
    {
        InvokeImageCamera();
    }
	
	public void Stop()
	{
		webCameraTexture.Stop();
	}

    public void Play()
    {
        rawImage = GetComponent<RawImage>();
        webCameraTexture = new WebCamTexture();
        webCameraTexture.requestedWidth = 640; // Atur sesuai kebutuhan
        webCameraTexture.requestedHeight = 480; // Atur sesuai kebutuhan
        webCameraTexture.Play();
    }

    public void InvokeImageCamera()
    {
        rawImage = GetComponent<RawImage>();
        webCameraTexture = new WebCamTexture();
        webCameraTexture.requestedWidth = 640; // Atur sesuai kebutuhan
        webCameraTexture.requestedHeight = 480; // Atur sesuai kebutuhan
        webCameraTexture.Play();
    }

    void Update()
    {
        if (webCameraTexture.isPlaying)
        {
            rawImage.texture = webCameraTexture;
        }
    }
}
