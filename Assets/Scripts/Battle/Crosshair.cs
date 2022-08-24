using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Crosshair class is responsible for displaying crosshair image 
*/

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    Texture2D image;
    [SerializeField]
    int size;
    [SerializeField]
    float maxAngle, minAngle;

    float lookHeight;

    void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size), image, ScaleMode.ScaleToFit, true);
    }
}
