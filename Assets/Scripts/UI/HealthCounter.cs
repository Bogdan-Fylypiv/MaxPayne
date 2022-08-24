using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] Text text;
    PlayerHealth playerHealth;

    private void Awake()
    {
        Manager.ManagerInstance.playerJoinedEvent += playerJoinedEventHandler;
    }

    private void playerJoinedEventHandler(Player player)
    {
        playerHealth = player.PlayerHealth;
        playerHealth.OnDamageReceived += UpdateHealth;
        playerHealth.OnPlayerRespawned += UpdateHealth;
        playerHealth.OnHealthPillsTaken += UpdateHealth;
    }

    private void UpdateHealth()
    {
        float healthLeft = playerHealth.HitPointsLeft / playerHealth.HitPoints * 100f;
        healthLeft = (float)System.Math.Round(healthLeft, 1);
        text.text = string.Format($"Health: {healthLeft}");
    }
}
