using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    /// <summary>
    /// Constains references to various UI panels
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [HideInInspector]
        public static UIManager Singleton;

        [Header("Main panels")]
        public GameObject MainMenu;
        public GameObject StageSelection;
        public GameObject CarSelection;
        public GameObject InGameControls;
        public GameObject DialoguePanel;
        public GameObject LoadingPanel;


        [Header("Secondary panels")]
        public GameObject PausePopup;

        [Header("List contents")]
        public GameObject contentGameLevels;

        [Header("Indicators")]
        public TMP_Text StarStatusIndicator;


        private void Awake()
        {
            Singleton = this;
        }

        private void OnEnable()
        {
            LoadingPanel.gameObject.SetActive(false);
        }

        public void ShowDialogue(string dialogueName, string[] dialogueValues, bool additionalValue = false)
        {
            if (dialogueName == "MissionCompleted")
            {
                GameObject shownDialogue = DialoguePanel.transform.Find("MissionCompleteDialogue").gameObject;
                shownDialogue.gameObject.SetActive(true);

                //stars indication
                int starsCount = int.Parse(dialogueValues[0]);
                //resetting stars
                for (int i = 0; i < 3; i++) shownDialogue.transform.Find("Popup").Find("Stars").Find("Star" + i.ToString()).gameObject.SetActive(false);
                //visualizing amount of achived stars
                for (int i = 0; i < starsCount; i++) shownDialogue.transform.Find("Popup").Find("Stars").Find("Star" + i.ToString()).gameObject.SetActive(true);

                //reward
                shownDialogue.transform.Find("Popup").Find("RewardCoins").Find("Text_Value").GetComponent<TMP_Text>().text = dialogueValues[1];
            }

            if (dialogueName == "CarStuck")
            {
                GameObject shownDialogue = DialoguePanel.transform.Find("CarStuckDialogue").gameObject;
                shownDialogue.gameObject.SetActive(additionalValue);
            }
        }
    }
}
