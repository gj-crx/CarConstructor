using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    public static class MapEditor
    {
        public static Vector3 StartingPointOffset = new Vector3(0, 1.5f, 0);

        public static Stack<GameObject> SpawnedPhysicalObjects = new Stack<GameObject>();
        public static Stack<GameObject> SpawnedDecorations = new Stack<GameObject>();

        public static void ClearMap(bool removeAllWallsAndDecorations = true)
        {
            foreach (var physicalObject in SpawnedPhysicalObjects)
            {
                GameObject.Destroy(physicalObject);
            }
            SpawnedPhysicalObjects = new Stack<GameObject>();

            if (removeAllWallsAndDecorations)
            {
                foreach (var decoration in SpawnedDecorations) GameObject.Destroy(decoration);
                SpawnedDecorations = new Stack<GameObject>();
            }
        }

        public static void SpawnParticles(List<GameLevelData.ParticleData> data)
        {
            foreach (var particleData in data)
            {
                GameObject newParticle = GameObject.Instantiate(PrefabManager.ParticlesPrefabs[particleData.ParticleID], particleData.particlePosition.ToVector3(), Quaternion.identity);

                if (particleData.DynamicState == false)
                {
                    newParticle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    newParticle.gameObject.tag = "Static";

                }

                newParticle.transform.SetParent(PrefabManager.ParticlesParentObject.transform);
                newParticle.gameObject.SetActive(true);

                SpawnedPhysicalObjects.Push(newParticle);
            }
        }
        public static void SpawnLevelWalls(GameObject[] walls)
        {
            foreach (GameObject newWall in walls)
            {
                var spawnedWall = GameObject.Instantiate(newWall);
                spawnedWall.transform.SetParent(PrefabManager.WallsParentObject.transform);
                MapEditor.SpawnedDecorations.Push(spawnedWall);
            }
        }

        public static void SpawnFlags(Vector3 startingPoint, Vector3 finishPoint)
        {
            SpawnedDecorations.Push(GameObject.Instantiate(PrefabManager.FinishFlag, finishPoint, Quaternion.identity));
            SpawnedDecorations.Push(GameObject.Instantiate(PrefabManager.StartFlag, startingPoint, Quaternion.identity));
        }
    }
}
