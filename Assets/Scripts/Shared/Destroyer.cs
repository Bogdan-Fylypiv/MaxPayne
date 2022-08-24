using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

/*
Destroyer class is used for game objects that can inflict damage
e.g. fire
*/

public class Destroyer : MonoBehaviour
{
    System.Timers.Timer timer;
    Coroutine inflictDamageCoroutine;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        player.PlayerHealth.OnDeath += () => StopCoroutine(inflictDamageCoroutine);
        inflictDamageCoroutine = StartCoroutine(InflictDamage(player));
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
            StopCoroutine(inflictDamageCoroutine);
                //timer.Stop();
    }

    public IEnumerator InflictDamage(Player player)
    {
        while(true){
            player.PlayerHealth.TakeDamage(1);
            yield return new WaitForSeconds(1);
        }
    }
}
