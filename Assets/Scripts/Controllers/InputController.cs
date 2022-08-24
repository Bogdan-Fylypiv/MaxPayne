using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
InputController class is used for gatherting input from the user; 
*/

public class InputController : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public Vector2 mouseInput;

    public bool fire;
    public bool reload;

    public bool isWalking;
    public bool isRunning;

    public bool mouseWheelUp;
    public bool mouseWheelDown;

    public bool numberOne;
    public bool numberTwo;

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        fire = Input.GetButton("Fire1");
        reload = Input.GetKey(KeyCode.R);

        //isWalking = !Input.GetKey(KeyCode.LeftAlt);
        if (vertical != 0 || horizontal != 0)
            isRunning = Input.GetKey(KeyCode.LeftShift);
        else
            isRunning = false;

        numberOne = Input.GetKeyDown(KeyCode.Alpha1);
        numberTwo = Input.GetKeyDown(KeyCode.Alpha2);

        mouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        mouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
    }
}
