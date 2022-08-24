using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    int amount;

    private void Awake()
    {
        amount = new System.Random().Next(10, 30);
    }

    public override void OnPickup(Transform item)
    {
        print(amount + " of health added");
        item.GetComponent<PlayerHealth>().TakePills(amount);
    }
}
