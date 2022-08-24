using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerAim playerAim;

    public PlayerAim PlayerAim
    {
        get
        {
            if (playerAim == null)
                playerAim = Manager.ManagerInstance.Player.playerAim;
            return playerAim;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", Manager.ManagerInstance.Input.vertical);
        animator.SetFloat("Horizontal", Manager.ManagerInstance.Input.horizontal);
        animator.SetBool("isRunning", Manager.ManagerInstance.Input.isRunning);
        animator.SetFloat("AimAngle", PlayerAim.GetAngle());
    }
}
