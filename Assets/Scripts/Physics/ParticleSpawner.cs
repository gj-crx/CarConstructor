using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleSpawner
{
    public static System.Random physicsRandom = new System.Random(); //no seed needed

    public static void SpawnParticle(Vector3 positionToSpawn, int particleToSpawnID)
    {
        if (positionToSpawn.y > 0)
        {
            GameObject newParticle = GameObject.Instantiate(PrefabManager.ParticlesPrefabs[particleToSpawnID], new Vector3(positionToSpawn.x, positionToSpawn.y, 0), Quaternion.identity);
            newParticle.transform.SetParent(PrefabManager.ParticlesParentObject.transform);
            newParticle.SetActive(true);
        }
    }
}
