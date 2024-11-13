using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform BulletSpawnpoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float power = 100f;



    void Start()
    {
       
    }

    void Update()
    {

    }

    public void InstantiateBullet(Vector3 shootDirection)
    {
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnpoint.position, BulletSpawnpoint.rotation , GameObject.FindGameObjectWithTag("GameObjectholder").transform );
        bullet.GetComponent<Rigidbody>().AddRelativeForce(BulletSpawnpoint.forward * bulletSpeed, ForceMode.Impulse);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode.Impulse); 
        }
    }

    
}


