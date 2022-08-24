using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum Mode
    {
        Aware,
        Unaware
    }

    Mode currentMode;
    public Mode CurrentMode
    {
        get
        {
            return currentMode;
        }
        set
        {
            if (currentMode == value)
                return;

            currentMode = value;

            OnModeChanged?.Invoke(value);
        }
    }

    public event Action<Mode> OnModeChanged;

    private void Start()
    {
        CurrentMode = Mode.Unaware;
    }

    private void Awake()
    {
        currentMode = Mode.Unaware;
    }

    [ContextMenu("Set Aware")]
    void SetToAware()
    {
        currentMode = Mode.Aware;
    }

    [ContextMenu("Set unwware")]
    void SetToUnware()
    {
        currentMode = Mode.Unaware;
    }
}
