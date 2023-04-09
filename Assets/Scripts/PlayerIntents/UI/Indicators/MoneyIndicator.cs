using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

namespace UI
{
    public class MoneyIndicator : MonoBehaviour
    {
        private TMP_Text valueText;

        private void Start()
        {
            valueText = transform.Find("Text_Value").GetComponent<TMP_Text>();
        }
        private void Update()
        {
            valueText.text = PlayerRepresentation.LocalPlayer.Money.ToString();
        }
    }
}
