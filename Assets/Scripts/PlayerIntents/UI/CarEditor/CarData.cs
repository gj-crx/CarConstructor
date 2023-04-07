using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cars.CarEditor
{
    [System.Serializable]
    public class CarData
    {
        public string CarName = "Custom unnamed car";
        public List<CarPartType.SerializableCarPart> CarParts = new List<CarPartType.SerializableCarPart>();
        public List<CarPartType.SerializableCarPart> FrameParts = new List<CarPartType.SerializableCarPart>();
        public int TotalCost = 0;
        public short FrameID = 0;

        public CarData(Car sourceCar)
        {
            CarName = sourceCar.CarName;
            TotalCost = sourceCar.TotalCost;
            FrameID = sourceCar.GetComponent<CarPartType>().CarPartTypeID;

            foreach (CarPartType carPart in sourceCar.gameObject.GetComponentsInChildren<CarPartType>())
            {
                if (carPart.Type != CarPartType.CarPartTypes.Frame)
                {
                    if (carPart.Type == CarPartType.CarPartTypes.FrameParts) FrameParts.Add(new CarPartType.SerializableCarPart(carPart));
                    else
                    {
                        CarParts.Add(new CarPartType.SerializableCarPart(carPart));
                        Debug.Log(carPart.gameObject.name + " is added to parts " + carPart.Type);
                        if (carPart.Type == CarPartType.CarPartTypes.Wheel) Debug.Log(carPart.ConnectedFramePartID + " wheel connection after save");
                    }
                }
            }
            Debug.Log("Total parts: " + CarParts.Count);
        }
    }
}
