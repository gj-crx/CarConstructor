using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public int Value = 25;

    [SerializeField]
    private short collectibleID = 0;

    [SerializeField]
    private Animator animator;

    public short CollectibleID
    {
        get { return collectibleID; }
        set { collectibleID = value; }
    }

    public int OnCollected()
    {
        animator.Play("Collected");

        return Value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.layer == 9)
        {
            if (PlayerRepresentation.LocalPlayer != null) PlayerRepresentation.LocalPlayer.Money += Value;
            animator.Play("Collected");
        }
    }
}
