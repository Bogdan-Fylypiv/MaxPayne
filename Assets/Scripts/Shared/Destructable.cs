using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destructable : MonoBehaviour
{

    [SerializeField]
    float hitPoints;

    public event Action OnDeath;
    public event Action OnDamageReceived;
    public event Action OnHealthPillsTaken;

    float amountOfDamage;

    public float HitPointsLeft
    {
        get
        {
            return hitPoints - amountOfDamage;
        }
    }

    public float HitPoints => hitPoints;

    public bool IsAlive => HitPointsLeft > 0;

    public virtual void Die()
    {
         OnDeath?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        amountOfDamage += damage;
        OnDamageReceived?.Invoke();

        if (HitPointsLeft <= 0)
            Die();
    }

    public void TakePills(int amount)
    {
        amountOfDamage = (amountOfDamage - amount) < 0 ? 0 : amountOfDamage - amount;
        OnHealthPillsTaken?.Invoke();
    }

    public void Reset() => amountOfDamage = 0;

}
