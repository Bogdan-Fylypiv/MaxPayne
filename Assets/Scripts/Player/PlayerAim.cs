using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Player Aim class handles the movement of crosshairs 
*/

public class PlayerAim : MonoBehaviour
{
    public void SetRotation(float amount)
    {
        float newAngle = CheckAngle(transform.eulerAngles.x - amount);
        newAngle = Mathf.Clamp(newAngle, -20, 20);
        transform.eulerAngles = new Vector3(newAngle, transform.eulerAngles.y, transform.eulerAngles.z); 
    }

    public float CheckAngle(float value)
    {
        float angle = value - 180;

        return angle > 0 ? angle - 180 : angle + 180;
    }

    public float GetAngle() => CheckAngle(transform.eulerAngles.x);
}
