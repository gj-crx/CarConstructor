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
    private int spawningSpeed = 6;
    [SerializeField]
    private int secondParticleSpawningSpeed = 1;

    [SerializeField]
    private float spawningTimeInterval = 0.05f;
    [SerializeField]
    private float cameraDistance = 15;

    private static GameLevelData newLevelToSave;

    public static GameLevelData NewLevelToSave
    {
        set { newLevelToSave = value; }
    }

    private int placedLastSecond = 0;

    private void Start()
    {
        StartCoroutine(RecountPlacedParticles());
    }
    void Update()
    {
        if (Application.isMobilePlatform == false)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                if (placedLastSecond < spawningSpeed) ParticleSpawner.SpawnParticle(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedParticle);
                placedLastSecond++;
            }
            if (Input.GetKey(KeyCode.F))
            {
                if (placedLastSecond < secondParticleSpawningSpeed) ParticleSpawner.SpawnParticle(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedSecondParticle);
                placedLastSecond++;
            }

            if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.E))
            {
                if (StartFlag != null) newLevelToSave.StartingPosition = new Position(StartFlag.transform.position + MapEditor.StartingPointOffset);
                if (FinishFlag != null) newLevelToSave.WinCondition.FinishPoint = new Position(FinishFlag.transform.position);

                SaveLoadSystem.GameLevelSaverLoader.SaveNewLevel(newLevelToSave, newLevelToSave.LevelName);
            }
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKey(KeyCode.Alpha6)) GoToLevelEditorMode(Input.GetKey(KeyCode.Alpha5));
        }
        else
        {
            return;
            if (Input.touchCount > 0)
            {
                if (placedLastSecond < spawningSpeed) ParticleSpawner.SpawnParticle(Camera.main.ScreenToWorldPoint(Input.touches[0].position), selectedParticle);
            }                placedLastSecond++;

        }
    }
    private void GoToLevelEditorMode(bool clearMap = true)
    {
    #if UNITY_EDITOR

        if (PlayerRepresentation.LocalPlayer.SelectedCar != null) PlayerRepresentation.LocalPlayer.SelectedCar.gameObject.SetActive(false);
        Camera.main.orthographicSize = cameraDistance;
        Camera.main.gameObject.GetComponent<CameraFollowing>().ManualControl = true;

        if (clearMap) MapEditor.ClearMap(false);

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
