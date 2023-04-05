using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position 
{
    public float X;
    public float Y;


    public Position(Vector3 vector)
    {
        X = vector.x;
        Y = vector.y;
    }
    public Position(Vector2 vector)
    {
        X = vector.x;
        Y = vector.y;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(X, Y);
    }
}
