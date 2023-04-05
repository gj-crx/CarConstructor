using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SaveLoadSystem;

namespace Cars.CarEditor
{
    [RequireComponent(typeof(Button))]
    public class ButtonCreateNewCar : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField carNameInput;
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        public void ExecuteButton()
        {
            if (CarConstructor.CurrentConstructedCar != null) Destroy(CarConstructor.CurrentConstructedCar.gameObject);
            CarConstructor.CurrentConstructedCar = GameObject.Instantiate(PrefabManager.PreCreatedCars[0]).GetComponent<Car>();
            CarConstructor.CurrentConstructedCar.CarName = carNameInput.text;
        }
    }
}
