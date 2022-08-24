using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Weapon controller is used for handling guns
For example it can change player weapon
*/

public class WeaponController : MonoBehaviour
{
    Shooter[] weapons;
    [SerializeField]
    Shooter currentWeapon;
    int currentWeaponIndex = 0;
    [SerializeField] float switchingTime;

    [HideInInspector]
    public bool canFire;
    Transform weaponHolder;

    public event Action<Shooter> OnWeaponSwitch;

    public Shooter CurrentWeapon => currentWeapon;

    void Awake()
    {
        canFire = true;
        weaponHolder = transform.Find("Weapon Holder");
        weapons = weaponHolder.GetComponentsInChildren<Shooter>();

        if (weapons.Length > 0)
            Equip(0);
    }

    internal void SwitchWeapon(int dir)
    {
        canFire = false;
        currentWeaponIndex += dir;

        if (currentWeaponIndex > weapons.Length - 1)
            currentWeaponIndex = 0;
        else if (currentWeaponIndex < 0)
            currentWeaponIndex = weapons.Length - 1;

        Manager.ManagerInstance.Timer.Add(() => { Equip(currentWeaponIndex); }, switchingTime);
        Equip(currentWeaponIndex);
    }

    void DeactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolder);
        }
    }

    internal void Equip(int index)
    {
        DeactivateWeapons();
        canFire = true;
        currentWeapon = weapons[index];
        currentWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        OnWeaponSwitch?.Invoke(CurrentWeapon);
    }
}
