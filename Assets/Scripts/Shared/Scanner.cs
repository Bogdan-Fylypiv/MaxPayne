using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Scanner is used to determine whether or not there is a player in the specified range 
and if it's whithin the field of view of an enemy
*/

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField]
    float scaningSpeed;

    [Range(0, 360)]
    [SerializeField]
    float fieldOfView;

    SphereCollider rangeTrigger;
    [SerializeField]
    LayerMask mask; // a mask for specifying which colliders to ignore
    int layerMask;

    public event Action OnScanReady;
    public event Action<Player> OnPlayerSelected;

    public float ScanRange
    {
        get
        {
            if (rangeTrigger == null)
                rangeTrigger = GetComponent<SphereCollider>();
            return rangeTrigger.radius;
        }
    }

    private void Awake()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    Vector3 GetViewAngle(float angle)
    {
        float radians = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Math.Sign(radians), 0, Mathf.Cos(radians));
    }

    void PrepareScan()
    {
        //print("Preparing Scan with timer " + scaningSpeed);
        Manager.ManagerInstance.Timer.Add(() => {
            OnScanReady?.Invoke();
        }, scaningSpeed);
    }

    public Player ScanForTarget()
    {
        Player target = null;
        Collider[] results = Physics.OverlapSphere(transform.position, ScanRange);

        for(int i = 0; i < results.Length; i++)
        {
            var isPlayer = results[i].tag.Equals("Player");
            if (!isPlayer)
                continue;
            if (!IsInLineOfSight(Vector3.up, results[i].transform.position))
                break;
            target = results[i].GetComponent<Player>();
            break;
        }

        if (target != null)
            OnPlayerSelected?.Invoke(target);

        PrepareScan();
        return target;
    }

    public bool IsInLineOfSight(Vector3 eyeHeight, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Vector3.Angle(transform.forward, direction.normalized);
        if (angle < fieldOfView / 2){
            float distToTarget = Vector3.Distance(transform.position, targetPosition);
            if(Physics.Raycast(transform.position + eyeHeight + transform.forward * 0.3f, direction.normalized, out var hit, distToTarget, mask, QueryTriggerInteraction.Ignore))
            {
                if(!hit.transform.tag.Equals("Player"))
                    return false;
            }
            return true;
        }

        return false;
    }
}
