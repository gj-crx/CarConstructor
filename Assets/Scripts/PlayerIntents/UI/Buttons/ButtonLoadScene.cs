using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonLoadScene : MonoBehaviour
    {
        [SerializeField]
        private string sceneNameToLoad;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        public void ExecuteButton()
        {
            SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
        }
    }
}
