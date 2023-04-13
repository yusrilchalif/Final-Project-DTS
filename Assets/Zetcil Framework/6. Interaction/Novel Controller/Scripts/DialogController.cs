using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogController : MonoBehaviour 
{
	[Header("Start")]
	[SerializeField] private bool startOnStart = false;
	[SerializeField]
	private float StartTime = 1;

	[Header("Settings")]
	[SerializeField]
	private Text TargetText;
	[SerializeField]
	private GameObject TargetPointer;
	[SerializeField] private float typeSpeed		= 0.1f;
	[SerializeField] private float startDelay 		= 0.5f;
	[SerializeField] private float volumeVariation 	= 0.1f;
	[SerializeField] private bool usingTypeAnimation; 

	[Header("Components")]
	[SerializeField]
	private bool usingAudioSource;
	[SerializeField] private AudioSource mainAudioSource;

	[Header("Status")]
	public int ChildIndex;
	public int counter;
	public string textToType;
	public bool typing;
	int isCanSkip = 0;

	[Header("Initialize Event")]
	public UnityEvent InitializeEvent;

	[Header("After FirstLine Event")]
	public UnityEvent AfterFirstLineEvent;

	[Header("Finish Dialog Event")]
	public bool HideAfterFinish;
	public UnityEvent DialogFinishEvent;


	private void Awake()
	{
        for (int i = 0; i < TargetText.transform.childCount; i++)
        {
			TargetText.transform.GetChild(i).gameObject.GetComponent<Text>().enabled = false;
		}
	}

	public void Initialize()
    {
		isCanSkip = 0;
		ChildIndex = 0;
		TargetText.text = "";
		InitializeEvent.Invoke();
	}

	void PrepareTyping()
    {
		if (ChildIndex < TargetText.transform.childCount)
		{
			counter = 0;
			textToType = TargetText.transform.GetChild(ChildIndex).GetComponent<Text>().text;
			TargetText.text = "";
		}
	}

	private void Start()
	{
		if(startOnStart)
		{
			Invoke("StartTyping", StartTime);
		}
	}


	void StopTyping()
	{
        counter = 0;
        typing = false;
		CancelInvoke("Type");
	}

	public void StopDialog()
	{
		counter = 0;
		typing = false;
		CancelInvoke("Type");
	}

	public void UpdateText(string newText)
    {   
        StopTyping();
		TargetText.text = "";
        textToType = newText;
        StartTyping();
    }

	void QuickSkip()
	{
		if(typing)
		{
			StopTyping();
			TargetText.text = textToType;
			if (ChildIndex == 0)
			{
				AfterFirstLineEvent.Invoke();
			}
		}
	}

	public void StartDialog()
	{
		Initialize();
		PrepareTyping();
		Invoke("StartTyping", StartTime);
	}

	void StartTyping()
	{
		if (!typing)
		{
			PrepareTyping();
			InvokeRepeating("Type", startDelay, typeSpeed);
		}
		else
		{
			Debug.LogWarning(gameObject.name + " : Is already typing!");
		}
	}

	public void LateUpdate()
    {
		isCanSkip++;
		if (isCanSkip > 200)
        {
			if (!TargetPointer.activeSelf)
            {
				TargetPointer.SetActive(true);
			}
			if (Input.GetKeyDown(KeyCode.Return))
            {
				NextDialog();
            }
        }
	}

	public void NextDialog()
	{
		if (isCanSkip > 200)
		{
			if (IsTyping())
			{
				QuickSkip();
			}
			else
			{
				if (ChildIndex == 0)
				{
					AfterFirstLineEvent.Invoke();
				}
				if (ChildIndex == TargetText.transform.childCount - 1)
				{
					DialogFinishEvent.Invoke();
					if (HideAfterFinish)
					{
						this.gameObject.SetActive(false);
					}
				}
				if (ChildIndex < TargetText.transform.childCount - 1)
				{
					ChildIndex++;
					PrepareTyping();
					StartTyping();
				}
			}
		}
	}

	private void Type()
	{
		if (usingTypeAnimation)
		{
			typing = true;
			TargetText.text = TargetText.text + textToType[counter];
			counter++;

			if (usingAudioSource)
			{
				if (mainAudioSource)
				{
					mainAudioSource.Play();
					RandomiseVolume();
				}
			}

			if (counter == textToType.Length)
			{
				typing = false;
				CancelInvoke("Type");

				if (ChildIndex == 0)
				{
					AfterFirstLineEvent.Invoke();
				}

			}
		} else
        {
			QuickSkip();
			typing = false;
			CancelInvoke("Type");
			TargetText.text = textToType;

			if (ChildIndex == 0)
			{
				AfterFirstLineEvent.Invoke();
			}
		}
	}

	private void RandomiseVolume()
	{
		mainAudioSource.volume = Random.Range(1 - volumeVariation, volumeVariation + 1);
	}

    public bool IsTyping() { return typing; }
}

/*
Copyright 2019 George Blackwell

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR 
ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH 
THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */