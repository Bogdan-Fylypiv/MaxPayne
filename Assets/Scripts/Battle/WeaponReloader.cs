using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    float reloadPeriod;
    [SerializeField]
    int clipSize;
    [SerializeField]
    Container inventory;
    [SerializeField]
    WeaponType weaponType; 
    Guid id;

    public event Action OnAmmoChanged;

    public int shotsFired;
    bool isReloading;

    public int RoundsRemainingInClip => clipSize - shotsFired;

    public int RoundsRemainingInInventory => inventory.GetAmountLeft(id);

    public void Awake()
    {
        inventory.OnContainerReady += () =>
        {
            id = inventory.Add(weaponType.ToString(), maxAmmo);
        };
    }

    public bool IsReloading => isReloading;

    public void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
        //print("About to reload");

        int amountFromInventory = inventory.Take(id, clipSize - RoundsRemainingInClip);

        Manager.ManagerInstance.Timer.Add(() => { ExecuteReload(amountFromInventory); }, reloadPeriod);
    }

    void ExecuteReload(int amount)
    {
        //print("Reloaded");
        isReloading = false;
        shotsFired -= amount;
        OnAmmoChanged?.Invoke();
        HandleOnAmmoChanged();
    }

    public void AddShots(int amount)
    {
        shotsFired += amount;
        HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        OnAmmoChanged?.Invoke();
    }
}
