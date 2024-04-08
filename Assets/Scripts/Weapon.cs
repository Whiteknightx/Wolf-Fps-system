using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletvelocity = 15;
    public float bulletlife = 3f;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;
    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    void Update()
    {
        
        if(currentShootingMode == ShootingMode.Auto)
        {
            //Holding left mouse click 
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            //click left mouse button
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            Fire();
        }
    }

    private void Fire()
    {
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //Instantiate the bullets
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);

        //point the bullet to face the shooting direction
        bullet.transform.rotation = Quaternion.LookRotation(shootingDirection);

        //shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection *  bulletvelocity, ForceMode.Impulse);

        // destroy the bullets
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletlife));

        //check shooting done
        if(allowReset)
        {
            Invoke(nameof(ResetShot), shootingDelay);
            allowReset = false;
        }

        //burst mode
        if(currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke(nameof(Fire), shootingDelay);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        //Shooting from the middle of screen to check where are we are pointing at
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetpoint;
        if(Physics.Raycast(ray,out hit))
        {
            //Hitting some objects
            targetpoint = hit.point;
        }
        else
        {
            //Shooting anywhere air
            targetpoint = ray.GetPoint(100);
        }
        Vector3 direction = targetpoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        //returning the shooting direction
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
       yield return new WaitForSeconds(delay);
    }
}
