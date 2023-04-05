using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

namespace UI
{
    public class MoneyIndicator : MonoBehaviour
    {
        private const float updateInterval = 0.25f;

        private TMP_Text valueText;

        private void Start()
        {
            valueText = transform.Find("Text_Value").GetComponent<TMP_Text>();
            StartCoroutine(UpdateMoneyValuesCoroutine());
        }

        private IEnumerator UpdateMoneyValuesCoroutine()
        {
            while (gameObject != null)
            {
                valueText.text = PlayerRepresentation.LocalPlayer.Money.ToString();
                yield return new WaitForSeconds(updateInterval);
            }
            Debug.Log("run out");
        }
    }
}
