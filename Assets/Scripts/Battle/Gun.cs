using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Shooter
{
    public override void Fire()
    {
        base.Fire();
    }

    public void Update()
    {
        // Check if reload button has been pressed
        if (Manager.ManagerInstance.Input.reload)
            Reload();
    }
}
