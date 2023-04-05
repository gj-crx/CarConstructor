using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField]
    private float breakForce = 100;
    [SerializeField]
    private float breakTorque = 100;
    [SerializeField]
    private float frequency = 0;
    [SerializeField]
    private float damping = 0;
    [SerializeField]
    private new Rigidbody2D rigidbody;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActiveAndEnabled && collision.gameObject.CompareTag("Particle") && rigidbody.velocity.magnitude < 0.03f)
        {
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            joint.breakForce = breakForce;
            joint.breakTorque = breakTorque;
            joint.dampingRatio = damping;
            joint.frequency = frequency;
        }
    }
}
