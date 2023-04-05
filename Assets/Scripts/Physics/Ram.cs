using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    [SerializeField]
    private float rammingForce = 100;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) //affect only to particles (layer 7)
        {
            if (collision.gameObject.CompareTag("Static"))
            {
                collision.gameObject.tag = "Untagged";
                collision.rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            collision.rigidbody.AddForce(gameObject.transform.up * rammingForce);
        }
    }
}
