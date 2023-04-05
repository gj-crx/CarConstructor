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

        private float timer_Lingering = 0;

        private void Awake()
        {
            hintText = GetComponent<TMP_Text>();
            normalColor = hintText.color;
        }

        public void ShowError(string errorText)
        {
            gameObject.SetActive(true);
            if (hintText == null) hintText = GetComponent<TMP_Text>();
            hintText.text = errorText;
            hintText.color = normalColor;

            StartCoroutine(HintLingeringCoroutine());
        }

        private IEnumerator HintLingeringCoroutine()
        {
            while (gameObject.activeInHierarchy)
            {
                if (timer_Lingering < HintFullVisibilityTime)
                {
                    hintText.color = new Color(hintText.color.r, hintText.color.g, hintText.color.b, hintText.color.a * (1 - timer_Lingering / (HintTotalLifeTime - HintFullVisibilityTime)));
                    timer_Lingering += Time.deltaTime;
                }
                else
                {
                    timer_Lingering = 0;
                    gameObject.SetActive(false);
                }
                yield return null;
            }
        }
    }
}
