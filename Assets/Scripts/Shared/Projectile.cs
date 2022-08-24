using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Projectile represents a bullet which is fired from the weapon muzzle
*/

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float lifetime;
    [SerializeField]
    float damage;
    [SerializeField]
    float range;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red, range);
        if(Physics.Raycast(transform.position, transform.forward, out var hit, range))
        {
            CheckDistructable(hit.transform);
            if (!hit.transform.tag.Equals("Scanner") && !hit.transform.name.Equals("Terrain") && !hit.transform.name.Equals("GunBullet(Clone)") && !hit.transform.name.Equals("ShotgunBullet(Clone)"))
                Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void CheckDistructable(Transform other)
    {
        var destructable = other.GetComponent<Destructable>();
        destructable?.TakeDamage(damage);
    }
}