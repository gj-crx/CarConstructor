using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cars;
using SaveLoadSystem;
using UI;
using System.Threading.Tasks;

namespace Cars.CarEditor {
    public class CarConstructor : MonoBehaviour
    {
        public GameObject ContentAviableCarPartsList;
        public GameObject PrefabUICarPart;

        public static int OldCarCost = 0;

        public static TMP_Text TotalCostTextIndicator;
        public static Car CurrentConstructedCar {
            get { return currentConstructedCar; }
            set
            {
                currentConstructedCar = value;
                if (currentConstructedCar != null) TotalCostTextIndicator.text = currentConstructedCar.TotalCost.ToString();
                Debug.Log(currentConstructedCar.TotalCost);
                Debug.Log("car cost changes 2");
            }
        }
        public static bool RemoverToolsIsOn
        {
            get { return removerToolStatus; }
            set
            {
                removerToolStatus = value;
                RemoverToolLabel.SetActive(value);
            }
        }
        public static CarPartType CurrentPlacedCarPart
        {
            get { return currentPlacedCarPart; }
            set
            {
                currentPlacedCarPart = value;
                PlaceCarPartButton.SetActive(value != null);
            }
        }
        public static CarPartType.CarPartTypes CurrentSelectedCategory = CarPartType.CarPartTypes.Wheel;
        public static GameObject RemoverToolLabel;
        public static LingeringHint CarEditorErrorHint;
        public static Joystick PartPlacementJoystick;
        public static RectTransform ClickPlaceArea;
        public static bool ManualCarPartPlacementActive = true;

        private static CarPartType currentPlacedCarPart;
        private static bool removerToolStatus = false;
        private static GameObject PlaceCarPartButton;


        [SerializeField]
        private LingeringHint carEditorErrorHint;
        [SerializeField]
        private TMP_Text totalCostTextIndicator;
        [SerializeField]
        private GameObject carSelectionPanel;
        [SerializeField]
        private GameObject buttonPlaceCarPart;
        [SerializeField]
        private GameObject removerToolLabel;
        [SerializeField]
        private RectTransform clickPlaceArea;
        [SerializeField]
        private Joystick joystick;

        private List<GameObject> visualizedCarParts = new List<GameObject>();
        private static Car currentConstructedCar = null;
        
        [SerializeField]
        private float carPartTranslationSpeed = 0.75f;

        private void Awake()
        {
            TotalCostTextIndicator = totalCostTextIndicator;
            CarEditorErrorHint = carEditorErrorHint;
            RemoverToolLabel = removerToolLabel;
            PartPlacementJoystick = joystick;
            PlaceCarPartButton = buttonPlaceCarPart;
            ClickPlaceArea = clickPlaceArea;

            GetIDsForCarParts();
        }
        private void Update()
        {
            if (CurrentPlacedCarPart != null)
            {
                if (PartPlacementJoystick.Horizontal != 0 || PartPlacementJoystick.Vertical != 0)
                {
                    ManualCarPartPlacementActive = false;

                    Vector3 rotation = CurrentPlacedCarPart.transform.eulerAngles;
                    CurrentPlacedCarPart.transform.Translate(new Vector3(PartPlacementJoystick.Horizontal, PartPlacementJoystick.Vertical) * carPartTranslationSpeed * Time.deltaTime);
                    currentPlacedCarPart.transform.eulerAngles = rotation;
                    
                }
                else Task.Run(ReactiveManualCarPartPlacement);
            }
        }
        public static void CarCostUpdate()
        {
            Debug.Log("car cost changes 1");
            if (CurrentConstructedCar.gameObject.activeInHierarchy) TotalCostTextIndicator.text = CurrentConstructedCar.TotalCost.ToString();
        }
        private async Task ReactiveManualCarPartPlacement()
        {
            await Task.Delay(100);
            ManualCarPartPlacementActive = true;
        }

        public void GenerateAviableCarPartsList(int shownCategoryID)
        {
            CurrentSelectedCategory = (CarPartType.CarPartTypes)shownCategoryID;
            //clearing previously visualized car parts
            for (int i = 0; i < visualizedCarParts.Count; i++) Destroy(visualizedCarParts[i]);
            visualizedCarParts = new List<GameObject>();

            for (int i = 0; i < PrefabManager.CarPartsPrefabs.Length; i++)
            {
                CarPartType currentCarPartType = PrefabManager.CarPartsPrefabs[i].GetComponent<CarPartType>();
                if (currentCarPartType.Type == CurrentSelectedCategory)
                {
                    var newListObject = Instantiate(PrefabUICarPart);
                    newListObject.transform.SetParent(ContentAviableCarPartsList.transform);
                    newListObject.transform.localScale = Vector3.one;
                    visualizedCarParts.Add(newListObject);

                    newListObject.transform.Find("Icon").GetComponent<Image>().sprite = currentCarPartType.Icon;
                    newListObject.transform.Find("CostPanel").Find("CostText").GetComponent<TMPro.TMP_Text>().text = currentCarPartType.Cost.ToString();
                    newListObject.transform.Find("PartName").GetComponent<TMPro.TMP_Text>().text = currentCarPartType.CarPartName;
                    newListObject.GetComponent<ButtonAddCarPart>().CarPartPrefabToPlace = currentCarPartType;
                }
            }
        }
        public static Car LoadCar(CarData carDataToLoad)
        {
            Car loadedCar = GameObject.Instantiate(PrefabManager.CarPartsPrefabs[carDataToLoad.FrameID], Vector3.zero, Quaternion.identity).GetComponent<Car>();


            loadedCar.TotalCost = carDataToLoad.TotalCost;
            loadedCar.CarName = carDataToLoad.CarName;

            //car initialization
            loadedCar.Engine = loadedCar.GetComponentInChildren<EngineType>();

            //frame parts
            foreach (var framePartData in carDataToLoad.FrameParts)
            {
                var loadedCarPart = GameObject.Instantiate(PrefabManager.CarPartsPrefabs[framePartData.CarPartTypeID]);
                loadedCarPart.transform.SetParent(loadedCar.transform);

                loadedCarPart.transform.localPosition = framePartData.CarPartPlacementPosition.ToVector3();
                loadedCarPart.GetComponent<PrePlacedCarPart>().ForcePlaceToCar(loadedCar, framePartData);
            }
            //other parts
            foreach (var carPartData in carDataToLoad.CarParts)
            {
                var loadedCarPart = GameObject.Instantiate(PrefabManager.CarPartsPrefabs[carPartData.CarPartTypeID]);
                loadedCarPart.transform.SetParent(loadedCar.transform);

                loadedCarPart.transform.localPosition = carPartData.CarPartPlacementPosition.ToVector3();
                loadedCarPart.GetComponent<PrePlacedCarPart>().ForcePlaceToCar(loadedCar, carPartData);
            }

            OldCarCost = loadedCar.TotalCost;

            return loadedCar;
        }

        private void OnEnable()
        {
            GenerateAviableCarPartsList((int)CurrentSelectedCategory);
            carSelectionPanel.SetActive(true);
        }


        public enum CarConstructorStatus
        {
            ReadyToBuild = 0,
            BadPartsPlacement = 1,
            NotEnoughMoney = 2,
            UnknownError = 3
        }

        private void GetIDsForCarParts()
        {
            for (short i = 0; i < PrefabManager.CarPartsPrefabs.Length; i++)
            {
                PrefabManager.CarPartsPrefabs[i].GetComponent<CarPartType>().CarPartTypeID = i;
            }
        }
    }
}
