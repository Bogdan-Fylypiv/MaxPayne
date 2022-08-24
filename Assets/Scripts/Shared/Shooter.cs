using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Shooter class will be used as a base class for any gun 
Methods for reloading and shooting
*/

public class Shooter : MonoBehaviour
{
    [SerializeField]
    float fireRate;
    [SerializeField] Projectile projectile;
    [HideInInspector] public WeaponReloader reloader;
    [SerializeField] AudioController audioShoot;
    [SerializeField] AudioController audioReload;

    // Muzzle position
    Transform muzzleTransform;
    [SerializeField] Transform hand;
    ParticleSystem fireParticleSystem; 


    float nextTimeToFire;
    public bool canFire;
    public Transform aimTarget; // cross hair
    public Vector3 aimTargetOffset; 

    private void Awake()
    {
        muzzleTransform = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        fireParticleSystem = muzzleTransform.GetComponent<ParticleSystem>();
    }

    public void Equip()
    {
        transform.SetParent(hand);
    }

    virtual public void Fire()
    {
        canFire = false;

        if (Time.time < nextTimeToFire)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading || reloader.RoundsRemainingInClip == 0)
                return;

            reloader.AddShots(1);
        }

        nextTimeToFire = Time.time + fireRate;

        muzzleTransform.LookAt(aimTarget.position + aimTargetOffset);

        Instantiate(projectile, muzzleTransform.position + muzzleTransform.forward, muzzleTransform.rotation);
        audioShoot.Play();
        fireParticleSystem?.Play();
        canFire = true;
    }

    public void Reload()
    {
        if (reloader == null)
            return;
        reloader?.Reload();
        audioReload.Play();
    }
}