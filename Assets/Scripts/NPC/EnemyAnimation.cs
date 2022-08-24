using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    PathFinder pathFinder;

    EnemyPlayer enemyPlayer;

    Vector3 lastPosition;

    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer = GetComponent<EnemyPlayer>();
    }

    void Update()
    {
        float velocity = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("IsWalking", enemyPlayer.EnemyState.CurrentMode == EnemyState.Mode.Aware);
        animator.SetFloat("Vertical", velocity / pathFinder.agent.speed); 
    }
}
