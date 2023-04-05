using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SaveLoadSystem
{
    [CreateAssetMenu(fileName = "New level", menuName = "Game level", order = 51)]
    public class GameLevelData : ScriptableObject
    {
        public string LevelName = "TestLevel";
        public List<ParticleData> BasicParticles;

        public bool LevelIsActive { get; set; } = false;

        public short LevelID = 0;
        public LevelWinCondition WinCondition = new LevelWinCondition();
        public int CoinRewardPerStar = 100;
        public Position StartingPosition;



        public async Task GenerateLevel()
        {
            MapEditor.ClearMap();

            Resources.UnloadUnusedAssets();
            MapEditor.SpawnLevelWalls(Resources.LoadAll<GameObject>("Levels/" + LevelName + "/Walls"));
            MapEditor.SpawnParticles(BasicParticles);
            MapEditor.SpawnFlags(StartingPosition.ToVector3() - MapEditor.StartingPointOffset, WinCondition.FinishPoint.ToVector3());

            GameLevelSaverLoader.CurrentLoadedLevel = this;
            LevelIsActive = true;

            await Task.Delay(10);
        }

        [System.Serializable]
        public class ParticleData
        {
            public Position particlePosition;
            public byte ParticleID = 0;
            public bool DynamicState = false;

            public ParticleData(CustomParticle particle)
            {
                particlePosition = new Position(particle.transform.position);
                ParticleID = particle.ParticleTypeID;
                DynamicState = particle.gameObject.CompareTag("Static") == false;
            }
        }
        [System.Serializable]
        public class WallData
        {
            public Position wallPosition;

            public float RotationZ;

            public float ScaleX;
            public float ScaleY;

            public WallData(Wall wall)
            {
                wallPosition = new Position(wall.transform.position);

                ScaleX = wall.transform.localScale.x;
                ScaleY = wall.transform.localScale.y;
                RotationZ = wall.transform.eulerAngles.z;
            }
        }
        [System.Serializable]
        public enum MissionWinConditionType : byte
        {
            ReachFinishPoint = 0,
            TimeRace = 1,
            Transportation = 2
        }
    }
}
