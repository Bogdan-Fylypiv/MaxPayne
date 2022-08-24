using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 EnemyPatrol handles patrolling and is based on events
e.g set the next waypoint once the current one has been reached
*/

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    WaypointController waypointController;
    [SerializeField]
    float minTime, maxTime;

    PathFinder pathFinder;
    EnemyPlayer enemyPlayer;
    public EnemyPlayer EnemyPlayer
    {
        get
        {
            if (enemyPlayer == null)
                enemyPlayer = GetComponent<EnemyPlayer>();
            return enemyPlayer;
        }
    }

    public Waypoint GuardingWaypoint => waypointController.GuardingWaypoint;

    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.OnDestinationReached += OnDestinationReachedHandler;
        waypointController.OnWaypointChanged += OnWaypointChangedHandler;
        EnemyPlayer.EnemyHealth.OnDeath += OnDeathHandler;
        EnemyPlayer.OnTargetSelected += OnTargetSelectedHandler;
        EnemyPlayer.OnTargetLost += OnTargetLostHandler;
    }


    private void Start()
    {
        waypointController.SetNextWaypoint();
    }

    private void OnTargetSelectedHandler(Player obj)
    {
        if(!enemyPlayer.chasePlayer)
            pathFinder.agent.isStopped = true;
    }

    void OnTargetLostHandler()
    {
        pathFinder.agent.isStopped = false;
        waypointController.SetNextWaypoint();
    }

    private void OnDeathHandler()
    {
        pathFinder.agent.isStopped = true;
    }

    private void OnWaypointChangedHandler(Waypoint wp)
    {
        pathFinder.SetTarget(wp.transform.position);
    }

    private void OnDestinationReachedHandler()
    {
        if (enemyPlayer.chasePlayer)
            enemyPlayer.onGuard = true;
        //Go to the next waypoint after some time
        Manager.ManagerInstance.Timer.Add(waypointController.SetNextWaypoint, Random.Range(minTime, maxTime));
    }
}