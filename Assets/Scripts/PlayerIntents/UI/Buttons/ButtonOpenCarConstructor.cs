using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonOpenCarConstructor : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        public void ExecuteButton()
        {
            UIManager.Singleton.LoadingPanel.gameObject.SetActive(true);
            SceneManager.LoadSceneAsync("CarConstructorScene", LoadSceneMode.Single);
        }
    }
}
