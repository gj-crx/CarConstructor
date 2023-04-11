using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace SaveLoadSystem
{
    public class GameLevelSaverLoader : MonoBehaviour
    {
        public static List<GameLevelData> AllLoadedGameLevels;
        public static GameLevelData CurrentLoadedLevel = null;

        [SerializeField]
        private List<GameLevelData> allLevels;

        private void Awake()
        {
            AllLoadedGameLevels = allLevels;
        }

        public static void SaveNewLevel(GameLevelData levelToSave, string fileSaveName)
        {
        #if UNITY_EDITOR

            Directory.CreateDirectory("Assets/Resources/Levels/" + fileSaveName);
            if (levelToSave.LevelName == "") levelToSave.LevelName = fileSaveName;

            
            //particles
            levelToSave.BasicParticles = new List<GameLevelData.ParticleData>();
            CustomParticle[] allParticles = (CustomParticle[])Resources.FindObjectsOfTypeAll(typeof(CustomParticle));
            foreach (var particle in allParticles)
            {
                if (particle.gameObject.activeInHierarchy) levelToSave.BasicParticles.Add(new GameLevelData.ParticleData(particle));
            }

            //walls
            int wallsCount = 0;
            Wall[] allWalls = (Wall[])Resources.FindObjectsOfTypeAll(typeof(Wall));
            foreach (var wall in allWalls)
            {
                if (wall.gameObject.activeInHierarchy)
                {
                    Debug.Log("Wall saved " + wall.name);
                    SerializationManager.SaveObjectAsAsset(wall.gameObject, "Wall" + wallsCount.ToString(), "Levels/" + fileSaveName + "/Walls/");
                    wallsCount++;
                }
            }

            //decorations --- CURRENTLY BEING SAVED AUTOMATICALLY!
   //         levelToSave.Collectibles = new List<GameLevelData.CollectibleData>();
   //         foreach (var collectible in GameObject.FindGameObjectsWithTag("Collectible"))
   //             levelToSave.Collectibles.Add(new GameLevelData.CollectibleData(collectible.GetComponent<ICollectible>().CollectibleID, collectible.transform.position));


            EditorUtility.SetDirty(levelToSave);
           AllLoadedGameLevels.Add(levelToSave);

        #endif
        }
        
        public static void LoadAllGameLevels()
        {
            foreach (var gameLevelPath in Resources.LoadAll<TextAsset>("Levels"))
            {
                if (gameLevelPath.name.Contains(".meta") == false)
                {
                    AllLoadedGameLevels.Add((GameLevelData)SerializationManager.LoadObject(gameLevelPath + "/LevelData"));
                    Debug.Log(gameLevelPath + " is loaded");
                }
            }
            
        }
       
    }
}
