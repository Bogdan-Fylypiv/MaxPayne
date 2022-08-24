using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    EnemyPlayer class is responsible for enemy behaviour
    It scans for player, checks if he is visible etc
*/

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : MonoBehaviour
{
    [SerializeField]
    Scanner scanner;
    [SerializeField]
    Animator animator;
    PathFinder pathFinder;
    Player target;
    GameObject player;
    EnemyHealth enemyHealth;
    EnemyState enemyState;
    public bool chasePlayer;
    internal bool onGuard;
    [SerializeField]
    public float stoppingDistance;
    bool playerVisible;

    public event Action<Player> OnTargetSelected;
    public event Action OnTargetLost;

    Coroutine PlayerObstructedCoroutine;

    public EnemyHealth EnemyHealth{
        get
        {
            if (enemyHealth == null)
                enemyHealth = GetComponent<EnemyHealth>();
            return enemyHealth;
        }
    }

    public EnemyState EnemyState{
        get
        {
            if (enemyState == null)
                enemyState = GetComponent<EnemyState>();
            return enemyState;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        scanner.OnScanReady += OnScanReadyHandler;
        scanner.OnPlayerSelected += OnPlayerSelectedHandler;
        OnScanReadyHandler();
        EnemyState.OnModeChanged += OnModeChangedHandler;
        chasePlayer = name.Split(' ')[1].Equals("Guard");
        if (stoppingDistance == 0)
            stoppingDistance = 5;
    }

    void OnPlayerSelectedHandler(Player player)
    {
        playerVisible = true;
        target = player;
    }

    void OnModeChangedHandler(EnemyState.Mode state)
    {
        pathFinder.agent.speed = 2;
        if(state == EnemyState.Mode.Aware)
            pathFinder.agent.speed = 5;
    }

    void Update()
    {
        if (!enemyHealth.IsAlive)
            return;
        
        if (chasePlayer && onGuard)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, GetComponent<EnemyPatrol>().GuardingWaypoint.transform.rotation, Time.deltaTime * 30);

        if (target == null)
        {
            OnScanReadyHandler();
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > 50 || !playerVisible)
        {
            print(playerVisible);
            target = null;
            OnTargetLost?.Invoke();
            return;
        }

        if (!scanner.IsInLineOfSight(Vector3.up, target.transform.position)){
            PlayerObstructedCoroutine = StartCoroutine(PlayerObstructed());
        }
        else if(PlayerObstructedCoroutine != null)
        {
            StopCoroutine(PlayerObstructedCoroutine);
            playerVisible = true;
        }

        if (chasePlayer)
        {
            onGuard = false;
            if(Vector3.Distance(transform.position, target.transform.position) < stoppingDistance)
            {
                animator.SetFloat("Vertical", 0);
                pathFinder.agent.isStopped = true;
                transform.LookAt(target.transform.transform.position);
            }
            else
            {
                pathFinder.agent.isStopped = false;
                animator.SetFloat("Vertical", 5);
                pathFinder.SetTarget(target.transform.position);
            }
        }
        else
        {
            transform.LookAt(target.transform.transform.position);
        }
    }

    IEnumerator PlayerObstructed()
    {
        yield return new WaitForSeconds(5);
        playerVisible = false;
    }

    void OnScanReadyHandler()
    {
        if(target != null)
            return;

        var player = scanner.ScanForTarget();
        if (player != null)
            target = player;
        if (target != null)
            OnTargetSelected?.Invoke(target);
    }
}
