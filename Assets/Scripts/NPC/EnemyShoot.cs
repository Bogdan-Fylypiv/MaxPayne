using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 EnemyShoot handles enemy's shooting when target is selected and lost
*/

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController
{
    [SerializeField]
    float shootingSpeed;
    [SerializeField]
    float burstMaxDuration;
    [SerializeField]
    float burstMinDuration;
    Player player;

    bool shouldFire;
    EnemyPlayer enemyPlayer;

    private void Start()
    {
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTargetSelected += OnTargetSelectedHandler;
        enemyPlayer.OnTargetLost += OnTargetLostHandler;
    }

    private void Update()
    {
        if (!shouldFire || !canFire || !enemyPlayer.EnemyHealth.IsAlive || player == null || !player.PlayerHealth.IsAlive)
            return;

        CurrentWeapon.Fire();
    }

    private void OnTargetSelectedHandler(Player target)
    {
        CurrentWeapon.aimTarget = target.transform;
        CurrentWeapon.aimTargetOffset = Vector3.up * 1.5f;
        player = target;
        StartBurst();
    }

    void OnTargetLostHandler()
    {
        CurrentWeapon.aimTarget = null;
        CurrentWeapon.aimTargetOffset = Vector3.zero;
        player = null;
    }

    void StartBurst()
    {
        if (!enemyPlayer.EnemyHealth.IsAlive)
            return;

        CheckReload();
        shouldFire = true;

        Manager.ManagerInstance.Timer.Add(EndBurst, Random.Range(burstMinDuration, burstMaxDuration));
    }

    void EndBurst()
    {
        shouldFire = false;
        if (!enemyPlayer.EnemyHealth.IsAlive)
            return;

        CheckReload();
        Manager.ManagerInstance.Timer.Add(StartBurst, shootingSpeed);
    }

    void CheckReload()
    {
        if (CurrentWeapon.reloader.RoundsRemainingInClip == 0 && CurrentWeapon.reloader.RoundsRemainingInInventory > 0)
            CurrentWeapon.Reload();
    }
}
