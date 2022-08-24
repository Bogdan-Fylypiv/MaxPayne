using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
PathFinder handles enemy path finding
*/

[RequireComponent(typeof(NavMeshAgent))]
public class PathFinder : MonoBehaviour
{
    public NavMeshAgent agent;
    public event Action OnDestinationReached;

    bool destinationReached;
    public float stoppingDistance;

    public bool DestinationReached
    {
        get
        {
            return destinationReached;
        }
        set
        {
            destinationReached = value;
            if (destinationReached)
                OnDestinationReached?.Invoke();
        }
    }

    void Update()
    {
        if (DestinationReached || !agent.hasPath)
            return;

        if (agent.remainingDistance <= stoppingDistance)
            DestinationReached = true;
    }

    public void SetTarget(Vector3 targetPosition)
    {
        DestinationReached = false;
        agent.SetDestination(targetPosition);
    }
}