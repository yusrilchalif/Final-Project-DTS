using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class CharacterSelector : MonoBehaviour
    {
        [Header("Main Settings")]
        public VarInteger TargetCharacter;

        [Header("Group Settings")]
        public GameObject PortraitGroup;
        public GameObject NameGroup;

        bool isStart = false;

        void InitCharacter()
        {
            for (int i = 0; i < PortraitGroup.transform.childCount; i++)
            {
                PortraitGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < NameGroup.transform.childCount; i++)
            {
                NameGroup.transform.GetChild(i).gameObject.SetActive(false);
            }

            if (TargetCharacter.CurrentValue - 1 < PortraitGroup.transform.childCount)
            {
                PortraitGroup.transform.GetChild(TargetCharacter.CurrentValue - 1).gameObject.SetActive(true);
            }
            if (TargetCharacter.CurrentValue - 1 < NameGroup.transform.childCount)
            {
                NameGroup.transform.GetChild(TargetCharacter.CurrentValue - 1).gameObject.SetActive(true);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (!isStart)
            {
                InitCharacter();
                isStart = true;
            }
        }
    }
}
