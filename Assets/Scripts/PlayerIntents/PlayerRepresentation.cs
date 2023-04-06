using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;
using Cars;
using System;

public class PlayerRepresentation : MonoBehaviour
{
    public Player PlayerProfile;

    public static Player LocalPlayer = null;


    private void Awake()
    {
        if (GameStateController.GameInitted) return;
        
        if (System.IO.Directory.Exists(Application.persistentDataPath + "/PlayerData/"))
        {
            try
            {
                PlayerProfile = (Player)SerializationManager.LoadObject(Application.persistentDataPath + "/PlayerData/MainPlayerSave.save");
                PlayerProfile.OwnedCars = new List<Car>();
            }
            catch
            {
                Debug.Log("Player data was not stored or failed to load");
                GameObject.Find("UI").transform.Find("Debug info").GetComponent<UnityEngine.UI.Text>().text += " not found or failed to load";
            }
            if (PlayerProfile == null) PlayerProfile = new Player();
            LocalPlayer = PlayerProfile;
        }
        else
        {
            PlayerProfile = new Player();
            LocalPlayer = PlayerProfile;
            GameObject.Find("UI").transform.Find("Debug info").GetComponent<UnityEngine.UI.Text>().text += " not exist";
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void LocalPlayerInit()
    {
        if (LocalPlayer == null) LocalPlayer = GameObject.Find("Player").GetComponent<PlayerRepresentation>().PlayerProfile;
    }

    private void OnApplicationPause(bool pause)
    {
        SerializationManager.SaveObject("MainPlayerSave.save", Application.persistentDataPath + "/PlayerData/", LocalPlayer);
    }
    private void OnApplicationQuit()
    {
        SerializationManager.SaveObject("MainPlayerSave.save", Application.persistentDataPath + "/PlayerData/", LocalPlayer);
    }

    [System.Serializable]
    public class Player
    {
        public int Money = 0;
        public List<LevelStatus> LevelProgressData = new List<LevelStatus>(100);

        [NonSerialized()]
        public List<Car> OwnedCars = new List<Car>();

        [NonSerialized()]
        public Car SelectedCar = null;

        public Player(int startingMoney = 100000)
        {
            //Init player level data first first 3 levels being unlocked
            LevelProgressData.Add(0);
            LevelProgressData.Add(0);
            LevelProgressData.Add(0);

            Money = startingMoney;
        }

        public void ExpandLevelsProgressData(int totalLevelsCount)
        {
            int countDifference = totalLevelsCount - LevelProgressData.Count;
            for (int levelID = LevelProgressData.Count; levelID < totalLevelsCount; levelID++)
            {
                if (GameLevelSaverLoader.AllLoadedGameLevels[levelID].AlwaysUnlocked || LevelProgressData[levelID - 1] != LevelStatus.Locked) LevelProgressData.Add(LevelStatus.Unlocked);
                else LevelProgressData.Add(LevelStatus.Locked);
            }
        }

        [System.Serializable]
        public enum LevelStatus : sbyte
        {
            Locked = -1,
            Unlocked = 0,
            OneStar = 1,
            TwoStars = 2,
            ThreeStars = 3
        }
    }
}
