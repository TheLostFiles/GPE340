using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    public bool isTriggerPulled = false;
    public UnityEvent OnMainActionStart;
    public UnityEvent OnMainActionEnd;
    public UnityEvent OnMainActionHold;

    public Transform rightHandPoint;
    public Transform leftHandPoint;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed;

    public float fireRate;
    private float nextFire = 0;


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void OnTriggerPull();
    public void OnTriggerHold()
    {
        // Timer for fire speed.
        isTriggerPulled = true;
        if (Time.time > nextFire)
        {
            // More Fire rate.
            nextFire = Time.time + fireRate;
            // Run Shoot.
            Shoot();
        }
    }

    public abstract void OnTriggerRelease();
    public virtual void Shoot()
    {
        // Instatiates bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        // Makes the bullet move.
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.TransformDirection(Vector3.forward * bulletSpeed);
        // Starts Coroutine.
        StartCoroutine(DestroyBullet(bullet));
    }

    private IEnumerator DestroyBullet(GameObject bullet)
    {
        // Waits 2 seconds.
        yield return new WaitForSeconds(2);
        // Destroys bullet.
        Destroy(bullet);
    }

}
