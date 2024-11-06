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

    public void InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnpoint.position, Quaternion.identity , GameObject.FindGameObjectWithTag("GameObjectholder").transform );
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawnpoint.forward * bulletSpeed, ForceMode.Impulse);

    }

    

}


