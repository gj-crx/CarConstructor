using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public bool ManualControl = false;
    public static GameObject ObjectToFollow = null;

    [SerializeField]
    private float staticZCord = 0;
    [SerializeField]
    private float manualSpeed = 4;

    private void Start()
    {
        staticZCord = transform.position.z;
    }
    void Update()
    {
        if (ManualControl == false)
        {
            if (ObjectToFollow != null && ObjectToFollow.activeInHierarchy)
            {
                transform.position = new Vector3(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y, staticZCord);
            }
        }
        else
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * manualSpeed * Time.deltaTime);
        }
    }
}
