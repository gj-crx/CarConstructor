using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [Header("---  Static objects")]
    [SerializeField]
    private GameObject wallsParentObject;
    [SerializeField]
    private GameObject particlesParentObject;
    [SerializeField]
    private MeshRenderer backgroundObject;

    [Header("---  Regular prefabs")]
    [SerializeField]
    private GameObject[] preCreatedCars;
    [SerializeField]
    private GameObject[] particlesPrefabs;
    [SerializeField]
    private GameObject[] wallsPrefabs;
    [SerializeField]
    private GameObject[] collectiblesPrefabs;

    [SerializeField]
    private GameObject[] carPartsPrefabs;

    [SerializeField]
    private GameObject startFlag;
    [SerializeField]
    private GameObject finishFlag;

    [Header("---  UI prefabs")]
    [SerializeField]
    private GameObject[] gameLevelItemPrefabs;


   //-----------
   //public static copies
    
    public static GameObject WallsParentObject;
    public static GameObject ParticlesParentObject;
    public static MeshRenderer BackgroundObject;

    public static GameObject StartFlag;
    public static GameObject FinishFlag;

    public static GameObject[] ParticlesPrefabs;
    public static GameObject[] WallsPrefabs;
    public static GameObject[] CollectiblesPrefabs;
    public static GameObject[] CarPartsPrefabs;
    public static GameObject[] PreCreatedCars;
    /// <summary>
    /// 0 - unlocked, 1 - 1 star, 2 - 2 stars, 3 - 3 stars, 4 - locked
    /// </summary>
    public static GameObject[] GameLevelItemPrefabs;



    private void Awake()
    {
        Application.targetFrameRate = 45;

        WallsParentObject = wallsParentObject;
        ParticlesParentObject = particlesParentObject;
        BackgroundObject = backgroundObject;


        WallsPrefabs = wallsPrefabs;
        ParticlesPrefabs = particlesPrefabs;
        CollectiblesPrefabs = collectiblesPrefabs;

        StartFlag = startFlag;
        FinishFlag = finishFlag;

        PreCreatedCars = preCreatedCars;
        CarPartsPrefabs = carPartsPrefabs;
        GameLevelItemPrefabs = gameLevelItemPrefabs;
    }
}

