using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    /// <summary>
    /// false = finish flag
    /// </summary>
    public bool StartFlag = true;
    private void Awake()
    {
        if (StartFlag) PlayerLevelEditor.StartFlag = gameObject;
        else PlayerLevelEditor.FinishFlag = gameObject;
    }
}
