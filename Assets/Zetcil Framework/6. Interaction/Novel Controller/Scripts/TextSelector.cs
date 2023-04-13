using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{

    public class TextSelector : MonoBehaviour
    {

        [Header("Main Settings")]
        public Text TargetText;

        [Header("Language Settings")]
        public VarLanguage TargetLanguage;
        [TextArea(5, 10)]
        public string IndonesianText;
        [TextArea(5, 10)]
        public string EnglishText;
        [TextArea(5, 10)]
        public string ArabicText;
        [TextArea(5, 10)]
        public string ChineseText;
        [TextArea(5, 10)]
        public string KoreanText;
        [TextArea(5, 10)]
        public string JapaneseText;

        bool isStart = false;

        void InitLanguage()
        {
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Indonesian)
            {
                TargetText.text = IndonesianText;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.English)
            {
                TargetText.text = EnglishText;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Arabic)
            {
                TargetText.text = ArabicText;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Chinese)
            {
                TargetText.text = ChineseText;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Korean)
            {
                TargetText.text = KoreanText;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Japanese)
            {
                TargetText.text = JapaneseText;
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
                InitLanguage();
                isStart = true;
            }
        }
    }
}
