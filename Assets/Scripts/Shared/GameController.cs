using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int idleStateID;
    int LocomotionStateID;

    private void Awake()
    {
        idleStateID = Animator.StringToHash("Idle");
        LocomotionStateID = Animator.StringToHash("Walk");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
