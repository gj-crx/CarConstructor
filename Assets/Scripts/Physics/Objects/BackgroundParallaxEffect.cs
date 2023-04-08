using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Transform followingTarget;
    [SerializeField, Range(0f, 1f)]
    private float parallaxStrength = 0.1f;
    [SerializeField]
    private bool disableVerticalParallax;

    private Vector3 targetPreviousPosition;

    void Start()
    {
        if (!followingTarget) followingTarget = Camera.main.transform;

        targetPreviousPosition = followingTarget.position;
    }

    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax) delta.y = 0;

        targetPreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
}
