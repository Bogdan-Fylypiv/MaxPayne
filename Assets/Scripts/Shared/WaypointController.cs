using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This controller is responsible for handling waypoints
It can be used to set the next waypoint
*/

public class WaypointController : MonoBehaviour
{
    Waypoint[] waypoints;

    public Waypoint GuardingWaypoint
    {
        get
        {
            if (waypoints.Length == 1)
                return waypoints[0];
            else
                return null;
        }
    }

    int currentWaypointIndex = -1;
    public event Action<Waypoint> OnWaypointChanged; 

    private void Awake()
    {
        waypoints = GetWaypoints();
    }

    private Waypoint[] GetWaypoints()
    {
        return GetComponentsInChildren<Waypoint>();
    }

    public void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        OnWaypointChanged?.Invoke(waypoints[currentWaypointIndex]);
    }
}
