using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float Speed = 1;

    [SerializeField]
    private Renderer renderer;

    private float oldXPosition = 0;

    void Update()
    {
        renderer.material.mainTextureOffset += new Vector2(Speed * Mathf.Min(transform.parent.position.x - oldXPosition, 2) * Time.deltaTime, 0);
        oldXPosition = transform.parent.position.x;
    }

}
