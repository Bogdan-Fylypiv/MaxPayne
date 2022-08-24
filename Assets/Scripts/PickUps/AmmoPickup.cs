using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem
{
    [SerializeField]
    WeaponType weaponType;
    [SerializeField]
    int amount;

    public override void OnPickup(Transform item)
    {
        var inventory = item.GetComponentInChildren<Container>();
        inventory.Put(weaponType.ToString(), amount);
        item.GetComponent<Player>().playerShoot.CurrentWeapon.reloader.HandleOnAmmoChanged();
    }
}