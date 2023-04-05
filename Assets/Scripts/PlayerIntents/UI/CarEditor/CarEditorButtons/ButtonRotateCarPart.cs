using UnityEngine;
using UnityEngine.UI;

namespace Cars.CarEditor
{
    [RequireComponent(typeof(Button))]
    public class ButtonRotateCarPart : MonoBehaviour
    {
        public float AngleIncrement = 15;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        public void ExecuteButton()
        {
            if (CarConstructor.CurrentPlacedCarPart == null) return;

            CarConstructor.CurrentPlacedCarPart.transform.eulerAngles = new Vector3(0, 0, CarConstructor.CurrentPlacedCarPart.transform.eulerAngles.z + AngleIncrement);
        }
    }
}
