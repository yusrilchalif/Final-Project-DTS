using ReadyPlayerMe.AvatarLoader;
using ReadyPlayerMe.Core;
using UnityEngine;

/* GIT: https://github.com/readyplayerme/rpm-unity-sdk-core.git#v1.1.0/ */
public class RPMAvatarLoader : MonoBehaviour
{
    [Header("ReadyPlayer URL")]
    public string avatarUrl = "https://api.readyplayer.me/v1/avatars/638df693d72bffc6fa17943c.glb";
    public bool OnStartLoad;

    [Header("Parent Settings")]
    public GameObject TargetParent;
    public GameObject TargetReference;
    public RuntimeAnimatorController TargetAnimator;

    GameObject CurrentAvatar;
    private GameObject avatar;

    public void LoadCharacter()
    {
        Debug.Log("Starting Load Avatar...");
        ApplicationData.Log();
        var avatarLoader = new AvatarObjectLoader();
        // use the OnCompleted event to set the avatar and setup animator
        avatarLoader.OnCompleted += (_, args) =>
        {
            avatar = args.Avatar;
            avatar.GetComponent<Animator>().runtimeAnimatorController = TargetAnimator;
            avatar.GetComponent<Animator>().applyRootMotion = false;
            ChangeCharacter(avatar);
        };
        avatarLoader.LoadAvatar(avatarUrl);
        Debug.Log("Loading Avatar Success!");
    }

    void Start()
    {
        if (OnStartLoad)
        {
            LoadCharacter();
        }
    }

    void OnDestroy()
    {
        if (avatar != null) Destroy(avatar);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePrefabs(GameObject NewPrefabs)
    {
        if (NewPrefabs != null)
        {
            CurrentAvatar = GameObject.Instantiate(NewPrefabs);
        }
        CurrentAvatar.transform.parent = TargetParent.transform;
        CurrentAvatar.transform.position = TargetReference.transform.position;
        CurrentAvatar.transform.rotation = TargetReference.transform.rotation;
        HideAllCharacter();
        CurrentAvatar.SetActive(true);
        GetComponent<RPMMovementController>().Init();
    }

    public void ChangeCharacter(GameObject NewAvatar)
    {
        if (NewAvatar != null)
        {
            CurrentAvatar = NewAvatar;
        }
        CurrentAvatar.transform.parent = TargetParent.transform;
        CurrentAvatar.transform.position = TargetReference.transform.position;
        CurrentAvatar.transform.rotation = TargetReference.transform.rotation;
        HideAllCharacter();
        CurrentAvatar.SetActive(true);
        GetComponent<RPMMovementController>().Init();
    }

    public void HideAllCharacter()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            // Mendapatkan child ke-i dari transform
            Transform child = transform.GetChild(i);

            // Mengecek apakah child tersebut memiliki komponen Animator
            Animator animator = child.GetComponent<Animator>();
            if (animator != null)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
