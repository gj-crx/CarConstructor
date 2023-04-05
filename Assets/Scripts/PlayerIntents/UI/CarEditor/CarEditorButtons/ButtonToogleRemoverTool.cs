using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

namespace Cars.CarEditor
{
    [RequireComponent(typeof(Button))]
    public class ButtonToogleRemoverTool : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        public void ExecuteButton()
        {
            CarConstructor.RemoverToolsIsOn = !CarConstructor.RemoverToolsIsOn;
        }
        public void TurnOffRemoverTool()
        {
            CarConstructor.RemoverToolsIsOn = false;
        }
    }
}
