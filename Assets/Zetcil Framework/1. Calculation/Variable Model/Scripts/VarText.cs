using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarText : MonoBehaviour
    {
        [Header("Variable Name")]
        public string VariableName;
        public GlobalVariable.CVariableType VariableType;
        Text CurrentText;


        // Start is called before the first frame update
        void Start()
        {
            CurrentText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerPrefs.HasKey(VariableName))
            {
                if (VariableType == GlobalVariable.CVariableType.healthVar ||
                    VariableType == GlobalVariable.CVariableType.scoreVar ||
                    VariableType == GlobalVariable.CVariableType.floatVar ||
                    VariableType == GlobalVariable.CVariableType.manaVar)
                {
                    CurrentText.text = PlayerPrefs.GetFloat(VariableName).ToString();
                }
                if (VariableType == GlobalVariable.CVariableType.intVar ||
                    VariableType == GlobalVariable.CVariableType.timeVar)
                {
                    CurrentText.text = PlayerPrefs.GetInt(VariableName).ToString();
                }
                if (VariableType == GlobalVariable.CVariableType.stringVar)
                {
                    CurrentText.text = PlayerPrefs.GetString(VariableName);
                }
                if (VariableType == GlobalVariable.CVariableType.boolVar)
                {
                    CurrentText.text = PlayerPrefs.GetString(VariableName);
                }
            }
        }
    }
}
