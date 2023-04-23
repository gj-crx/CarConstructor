using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cars.CarEditor
{
    public static class CarPartListFormer
    {
        private static List<GameObject> visualizedCarParts = new List<GameObject>();

        public static void GenerateAviableCarPartsList(int shownCategoryID)
        {
            CarConstructor.CurrentSelectedCategory = (CarPartType.CarPartTypes)shownCategoryID;
            //clearing previously visualized car parts
            for (int i = 0; i < visualizedCarParts.Count; i++) GameObject.Destroy(visualizedCarParts[i]);
            visualizedCarParts = new List<GameObject>();

            for (int i = 0; i < PrefabManager.CarPartsPrefabs.Length; i++)
            {
                CarPartType currentCarPartType = PrefabManager.CarPartsPrefabs[i].GetComponent<CarPartType>();
                if (currentCarPartType.Type == CarConstructor.CurrentSelectedCategory)
                {
                    var newListObject = GameObject.Instantiate(CarConstructor.PrefabUICarPart);
                    newListObject.transform.SetParent(CarConstructor.ContentAviableCarPartsList.transform);
                    newListObject.transform.localScale = Vector3.one;
                    visualizedCarParts.Add(newListObject);

                    newListObject.transform.Find("Icon").GetComponent<Image>().sprite = currentCarPartType.Icon;
                    newListObject.transform.Find("CostPanel").Find("CostText").GetComponent<TMPro.TMP_Text>().text = currentCarPartType.Cost.ToString();
                    newListObject.transform.Find("PartName").GetComponent<TMPro.TMP_Text>().text = currentCarPartType.CarPartName;
                    newListObject.GetComponent<ButtonAddCarPart>().CarPartPrefabToPlace = currentCarPartType;
                }
            }
        }
    }
}
