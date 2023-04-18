using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;

public class PlayerLevelEditor : MonoBehaviour
{
    public static GameObject FinishFlag;
    public static GameObject StartFlag;

    [SerializeField]
    private int selectedParticle = 0;
    [SerializeField]
    private int selectedSecondParticle = 0;

    [SerializeField]
    private short selectedCollectibleID = 0;

    [SerializeField]
    private int spawningSpeed = 6;
    [SerializeField]
    private int secondParticleSpawningSpeed = 1;

    [SerializeField]
    private float spawningTimeInterval = 0.05f;
    [SerializeField]
    private float cameraDistance = 15;

    [SerializeField]
    private bool clearMapOnEditorMode = false;

    private static GameLevelData newLevelToSave;

    public static GameLevelData NewLevelToSave
    {
        set { newLevelToSave = value; }
    }

    private int placedLastSecond = 0;
    private bool editorModeActivated = false;

    private void Start()
    {
        StartCoroutine(RecountPlacedParticles());
    }
    void Update()
    {
        if (Application.isMobilePlatform) return;

        if (Input.GetKey(KeyCode.Q) && editorModeActivated)
        {
            if (placedLastSecond < spawningSpeed) ParticleSpawner.SpawnParticle(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedParticle, true);
            placedLastSecond++;
        }
        if (Input.GetKey(KeyCode.F) && editorModeActivated)
        {
            if (placedLastSecond < secondParticleSpawningSpeed) ParticleSpawner.SpawnParticle(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedSecondParticle, true);
            placedLastSecond++;
        }
        if (Input.GetKeyDown(KeyCode.Y) && editorModeActivated)
        {
            RemoveParticle(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
        }
        if (Input.GetKeyDown(KeyCode.T) && editorModeActivated)
        {
            ParticleSpawner.SpawnCollectible(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedCollectibleID);

            newLevelToSave.Collectibles.Add(new GameLevelData.CollectibleData(selectedCollectibleID, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }


        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.E))
        {
            //Initializing new level properties
            if (StartFlag != null) newLevelToSave.StartingPosition = new Position(StartFlag.transform.position + MapEditor.StartingPointOffset);
            if (FinishFlag != null) newLevelToSave.WinCondition.FinishPoint = new Position(FinishFlag.transform.position);

            newLevelToSave.BackgroundMaterial = PrefabManager.BackgroundObject.material;

            SaveLoadSystem.GameLevelSaverLoader.SaveNewLevel(newLevelToSave, newLevelToSave.LevelName);
        }
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.Alpha6)) GoToLevelEditorMode(clearMapOnEditorMode);
    }
    private void GoToLevelEditorMode(bool clearMap = true)
    {
    #if UNITY_EDITOR
        editorModeActivated = true;
        if (PlayerRepresentation.LocalPlayer.SelectedCar != null) PlayerRepresentation.LocalPlayer.SelectedCar.gameObject.SetActive(false);
        Camera.main.orthographicSize = cameraDistance;
        Camera.main.gameObject.GetComponent<CameraFollowing>().ManualControl = true;
        PrefabManager.BackgroundObject.gameObject.SetActive(false);

        if (clearMap)
        {
            MapEditor.ClearMap(false);
            newLevelToSave.Collectibles.Clear();
        }

    #endif
    }
    /// <summary>
    /// very slow function but okay for editor only mode
    /// </summary>
    public static void RemoveParticle(Vector3 removalPosition, float removalRadius = 3f)
    {
    #if UNITY_EDITOR
        foreach (var particle in MapEditor.SpawnedPhysicalObjects)
        {
            if (particle != null && Vector3.Distance(removalPosition, particle.transform.position) < removalRadius)
            {
                Destroy(particle);
            }
        }
    #endif
    }
    private IEnumerator RecountPlacedParticles()
    {
        while (gameObject != null)
        {
            placedLastSecond = 0;
            yield return new WaitForSeconds(spawningTimeInterval);
        }
    }
}
