using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class will be used to enable enemy's ragdoll when they get killed 
*/

public class Ragdoll : MonoBehaviour
{
    public Animator animator;
    Rigidbody[] bodyParts;

    private void Start()
    {
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
    }

    public void EnableRagdoll(bool value)
    {
        animator.enabled = !value;
        foreach (var part in bodyParts)
            part.isKinematic = !value;
    }
}
