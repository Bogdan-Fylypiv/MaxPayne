using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] Text text;
    PlayerShoot playerShoot;
    WeaponReloader reloader;
    string currentWeaponName;

    private void Awake()
    {
        Manager.ManagerInstance.playerJoinedEvent += playerJoinedEventHandler;
    }

    private void playerJoinedEventHandler(Player player)
    {
        playerShoot = player.playerShoot;
        currentWeaponName = player.GetComponentInChildren<Shooter>().name;
        playerShoot.OnWeaponSwitch += OnWeaponSwitchHandler;
        reloader = playerShoot.CurrentWeapon.reloader;
        reloader.OnAmmoChanged += OnAmmoChangedHandler;
        OnAmmoChangedHandler();
    }

    private void OnWeaponSwitchHandler(Shooter weapon)
    {
        reloader = weapon.reloader;
        currentWeaponName = weapon.name;
        reloader.OnAmmoChanged += OnAmmoChangedHandler;
        OnAmmoChangedHandler();
    }

    void OnAmmoChangedHandler()
    {
        int amountInClip = reloader.RoundsRemainingInClip;
        int amountInInv = reloader.RoundsRemainingInInventory;
        text.text = string.Format("{0}: {1} / {2}", currentWeaponName, amountInClip, amountInInv);
    }
}
