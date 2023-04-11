using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventsManager
{
    public static OnCollectibleCollected CollectibleCollectedEvent;




    public delegate void OnCollectibleCollected(GameObject collectibleObject);
}
