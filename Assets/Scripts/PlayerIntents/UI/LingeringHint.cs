using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class LingeringHint : MonoBehaviour
    {
        public float HintTotalLifeTime = 4f;
        public float HintFullVisibilityTime = 2f;
        private TMP_Text hintText;
        private Color normalColor;

        [SerializeField]
        private Color AlternativeColor;

        private float timer_Lingering = 0;

        private void Awake()
        {
            hintText = GetComponent<TMP_Text>();
            normalColor = hintText.color;

            Debug.Log("nigro");
        }

        public void ShowError(string errorText, bool alternativeColor = false)
        {
            gameObject.SetActive(true);
            if (hintText == null) hintText = GetComponent<TMP_Text>();
            hintText.text = errorText;

            if (alternativeColor == false) hintText.color = normalColor;
            else hintText.color = AlternativeColor;


            StartCoroutine(HintLingeringCoroutine());
        }

        private IEnumerator HintLingeringCoroutine()
        {
            while (gameObject.activeInHierarchy)
            {
                if (timer_Lingering < HintTotalLifeTime)
                {
                    if (timer_Lingering > HintFullVisibilityTime)
                    {

                    }
                    timer_Lingering += Time.deltaTime;
                }
                else
                {
                    timer_Lingering = 0;
                    hintText.color = normalColor;
                    gameObject.SetActive(false);
                }
                yield return null;
            }
        }
    }
}
