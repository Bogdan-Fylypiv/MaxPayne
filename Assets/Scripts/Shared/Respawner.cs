using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void Despawn(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        Manager.ManagerInstance.Timer.Add(() => { go.SetActive(true); }, inSeconds);
    }
}
