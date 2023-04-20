using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cars;
using Cars.CarEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CarListFormer : MonoBehaviour
    {

        [SerializeField]
        private GameObject carItemPrefab;
        [SerializeField]
        private GameObject carsListContent;

        private string carDirectory = "/Cars/";

        private static List<GameObject> spawnedCarItems = new List<GameObject>();
        private static GameObject static_carItemPrefab;
        private static GameObject static_carListContent;
        private static string static_carDirectory;

        private void Awake()
        {
            static_carItemPrefab = carItemPrefab;
            static_carListContent = carsListContent;
            static_carDirectory = carDirectory;
        }
        private void OnEnable()
        {
            LoadAndEnlistCars();
        }

        public static void LoadAndEnlistCars()
        {
            if (Directory.Exists(Application.persistentDataPath + static_carDirectory) == false)
            {
                Directory.CreateDirectory(Application.persistentDataPath + static_carDirectory);
                return; //nothing to load
            }

            ClearCarList();

            foreach (string carPath in Directory.GetFiles(Application.persistentDataPath + static_carDirectory))
            {
                CarData carData = (CarData)SaveLoadSystem.SerializationManager.LoadObject(carPath);
                if (carData == null) File.Delete(carPath);
                else
                {
                    var carItem = Instantiate(static_carItemPrefab);
                    carItem.transform.SetParent(static_carListContent.transform);
                    carItem.transform.localScale = Vector3.one;

                    carItem.GetComponent<UI.Buttons.ButtonSelectCar>().representedCarData = carData;
                    carItem.GetComponent<UI.Buttons.ButtonSelectCar>().CarDataDirectory = carPath;

                    carItem.transform.Find("CarIcon").Find("CarName").GetComponent<TMPro.TMP_Text>().text = carData.CarName;
                    carItem.transform.Find("IconCost").Find("CostValue").GetComponent<TMPro.TMP_Text>().text = carData.TotalCost.ToString();

                    spawnedCarItems.Add(carItem);
                }
            }
        }
        private static void ClearCarList()
        {
            for (int i = 0; i < spawnedCarItems.Count; i++) Destroy(spawnedCarItems[i]);
        }


        /// <summary>
        /// Return true if at least 1 car is ready to be loaded
        /// </summary>
        public static bool GetLoadedCarsCount()
        {
            if (Directory.Exists(Application.persistentDataPath + static_carDirectory) == false) return false;
            if (Directory.GetFiles(Application.persistentDataPath + static_carDirectory).Length <= 0) return false;

            foreach (string carPath in Directory.GetFiles(Application.persistentDataPath + static_carDirectory))
            {
                try
                {
                    var cardata = (CarData)SaveLoadSystem.SerializationManager.LoadObject(carPath);
                    return true;
                }
                catch
                {
                    return false;
                }

            }

            return false;
        }
    }
}
