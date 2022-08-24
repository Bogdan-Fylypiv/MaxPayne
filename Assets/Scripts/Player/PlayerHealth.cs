using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Destructable
{
    [SerializeField]
    public SpawnPoint[] spawnPoints;
    [SerializeField] Ragdoll ragdoll;
    [SerializeField] Image bloodScreen;

    public event Action OnPlayerRespawned;


    private void Awake()
    {
        spawnPoints = GameObject.Find("/SpawnPoints").GetComponentsInChildren<SpawnPoint>();
        var tempColor = bloodScreen.color;
        tempColor.a = 0;
        bloodScreen.color = tempColor;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        var tempColor = bloodScreen.color;
        tempColor.a = 1;
        bloodScreen.color = tempColor;
        Manager.ManagerInstance.Timer.Add(() =>
        {
            var tempColorA = bloodScreen.color;
            tempColor.a = 0;
            bloodScreen.color = tempColor;
        }, 1);
    }

    public override void Die()
    {
        base.Die();
        SpawnAt(GetClosestSpawnPoint());
        Reset();
        OnPlayerRespawned?.Invoke();
        GetComponent<PlayerShoot>().PlayerAlive = true;
    }

    private SpawnPoint GetClosestSpawnPoint()
    {
        if (spawnPoints.Length == 0)
            return null;
        var closest = spawnPoints[0];
        foreach(var point in spawnPoints)
            if (Vector3.Distance(transform.position, point.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                closest = point;
        return closest;
    }

    void SpawnAt(SpawnPoint point)
    {
        GetComponent<CharacterController>().enabled = false;
        gameObject.transform.position = point.transform.position;
        gameObject.transform.rotation = point.transform.rotation;
        GetComponent<CharacterController>().enabled = true;
        Reset();
    }
}
