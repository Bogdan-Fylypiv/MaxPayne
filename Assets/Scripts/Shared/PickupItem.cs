using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
            return;

        OnPickup(other.transform);
    }

    public virtual void OnPickup(Transform transform)
    {
        //print("pickup");
    }

    void PickUp(Transform item)
    {
        OnPickup(item);
    }
}
