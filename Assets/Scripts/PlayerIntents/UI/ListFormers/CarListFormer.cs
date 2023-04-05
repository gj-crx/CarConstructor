using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cars;
using UnityEngine;

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
            var carItem = Instantiate(static_carItemPrefab);
            carItem.transform.SetParent(static_carListContent.transform);
            carItem.transform.localScale = Vector3.one;

            Car.CarData carData = (Car.CarData)SaveLoadSystem.SerializationManager.LoadObject(carPath);

            carItem.GetComponent<UI.Buttons.ButtonSelectCar>().representedCarData = carData;
            carItem.GetComponent<UI.Buttons.ButtonSelectCar>().CarDataDirectory = carPath;

            carItem.transform.Find("CarIcon").Find("CarName").GetComponent<TMPro.TMP_Text>().text = carData.CarName;
            carItem.transform.Find("IconCost").Find("CostValue").GetComponent<TMPro.TMP_Text>().text = carData.TotalCost.ToString();

            spawnedCarItems.Add(carItem);
        }

    }
    private static void ClearCarList()
    {
        for (int i = 0; i < spawnedCarItems.Count; i++) Destroy(spawnedCarItems[i]);
    }
}
