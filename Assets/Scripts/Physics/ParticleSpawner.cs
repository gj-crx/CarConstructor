using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleSpawner
{
    public static System.Random physicsRandom = new System.Random(); //no seed needed

    public static void SpawnParticle(Vector3 positionToSpawn, int prefabID, bool randomizeScale = false)
    {
        GameObject newParticle = GameObject.Instantiate(PrefabManager.ParticlesPrefabs[prefabID], new Vector3(positionToSpawn.x, positionToSpawn.y, 0), Quaternion.identity);
        newParticle.transform.SetParent(PrefabManager.ParticlesParentObject.transform);

        if (randomizeScale) newParticle.GetComponent<CustomParticle>().RandomizeScale();

        newParticle.SetActive(true);
    }
    public static void SpawnCollectible(Vector3 positionToSpawn, int prefabID)
    {
        GameObject newCollectible = GameObject.Instantiate(PrefabManager.CollectiblesPrefabs[prefabID], new Vector3(positionToSpawn.x, positionToSpawn.y, 0), Quaternion.identity);
        newCollectible.transform.SetParent(PrefabManager.WallsParentObject.transform);
        newCollectible.SetActive(true);
    }
}
