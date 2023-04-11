using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    public short CollectibleID { get; set; }

    public int OnCollected();
}
