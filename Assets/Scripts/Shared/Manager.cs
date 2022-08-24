using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Instance of Manager class will be created once it is needed.
Singleton class
Manager will be used to handle basic routine, such as input.
*/

public class Manager
{
    // Will hold Manager
    GameObject gameObject;
    // An instance of manager
    static Manager instance;

    InputController input;
    Player player;
    Timer timer;
    Respawner respawner;

    public event System.Action<Player> playerJoinedEvent;

    // Instantiates a manager
    public static Manager ManagerInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new Manager();
                instance.gameObject = new GameObject("manager");
                instance.gameObject.AddComponent<InputController>();
                instance.gameObject.AddComponent<Timer>();
                instance.gameObject.AddComponent<Respawner>();
            }
            return instance;
        }
    }

    public InputController Input
    {
        get
        {
            if (input == null)
                input = gameObject.GetComponent<InputController>();
            return input;
        }
    }

    public Player Player
    {
        get => player;
        set {
            player = value;
            playerJoinedEvent?.Invoke(player);
        }
    }

    public Timer Timer
    {
        get
        {
            if (timer == null)
                timer = gameObject.GetComponent<Timer>();
            return timer;
        }
    } 
    public Respawner Respawner
    {
        get
        {
            if (respawner == null)
                respawner = gameObject.GetComponent<Respawner>();
            return respawner;
        }
    }
}
