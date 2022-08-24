using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attached to Player

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController
{
    bool playerAlive;

    public bool PlayerAlive
    {
        get
        {
            return playerAlive;
        }
        set
        {
            playerAlive = value;
        }
    }

    void Start()
    {
        GetComponent<Player>().PlayerHealth.OnDeath += () => playerAlive = false;
        playerAlive = true;
    }

    void Update()
    {
        if (!playerAlive)
            return;

        if (Manager.ManagerInstance.Input.numberOne)
            Equip(0);
        else if (Manager.ManagerInstance.Input.numberTwo)
            Equip(1);

        if (!canFire)
            return; 

        if (Manager.ManagerInstance.Input.fire)
            CurrentWeapon.Fire();

    }
}
