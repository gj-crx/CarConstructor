using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticle : MonoBehaviour
{
    public byte ParticleTypeID = 0;

    private bool onStayCollisionCheck = false;
    private float randomValue = 0;

    [Header("---  Physics")]
    [SerializeField]
    private new Rigidbody2D rigidbody;
    [SerializeField]
    private float onStayCollisionCheckInterval = 1.0f;
    [SerializeField]
    private float stoppingForce = 0.15f;
    [SerializeField]
    private float breakForce = 2.0f;
    [SerializeField]
    private float accelerationAfterBreaking = 5f;

    [Header("---  Visuals")]
    [SerializeField]
    private float colorRandomizationFactor = 0.1f;
    [SerializeField]
    private float scaleRandomizationFactor = 1.0f;

    void Start()
    {
        randomValue = RandomizeParticle();
        StartCoroutine(OnStayIntervalReset(onStayCollisionCheckInterval + randomValue));
    }

    private void CollisionCheck(Collision2D collision)
    {
        if (gameObject.CompareTag("Static") == false)
        {
            if (collision.gameObject.CompareTag("Static") && rigidbody.velocity.magnitude < stoppingForce)
            {
                gameObject.tag = "Static";
                rigidbody.bodyType = RigidbodyType2D.Kinematic;
                rigidbody.useFullKinematicContacts = true;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = 0;
            }
        }
        else
        {
           // Debug.Log(collision.rigidbody.gameObject.name + " " + collision.rigidbody.velocity.magnitude);
            if (collision.rigidbody != null && collision.rigidbody.velocity.magnitude > breakForce)
            {
                gameObject.tag = "Untagged";
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                rigidbody.AddForce(collision.rigidbody.velocity * accelerationAfterBreaking);
            }
        }
    }
    public void RandomizeScale()
    {
        transform.localScale = new Vector3(transform.localScale.x + randomValue * scaleRandomizationFactor, transform.localScale.y + randomValue * scaleRandomizationFactor, transform.localScale.z);
    }
    private float RandomizeParticle()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float randomNumber = Random.Range(-colorRandomizationFactor, colorRandomizationFactor);

        if (randomNumber > 0)
        {
            spriteRenderer.color = new Color(
                spriteRenderer.color.r + randomNumber,
                spriteRenderer.color.g - randomNumber,
                spriteRenderer.color.b + randomNumber);
        }
        else
        {
            spriteRenderer.color = new Color(
               spriteRenderer.color.r - randomNumber,
               spriteRenderer.color.g + randomNumber,
               spriteRenderer.color.b - randomNumber);
        }

        return randomNumber;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParticleKiller")) Destroy(gameObject);
        else CollisionCheck(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (onStayCollisionCheck)
        {
            onStayCollisionCheck = false;
            CollisionCheck(collision);
        }
    }

    private IEnumerator OnStayIntervalReset(float onStayCollisionCheckInterval)
    {
        while (gameObject != null)
        {
            onStayCollisionCheck = true;
            yield return new WaitForSeconds(onStayCollisionCheckInterval);
        }
    }
}
