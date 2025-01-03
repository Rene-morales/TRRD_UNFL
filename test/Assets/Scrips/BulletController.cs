using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody bulletRb;
    public float power = 100f;
    public float lifetime;
    public int damage = 1;


    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();

        bulletRb.velocity = this.transform.forward * power;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.activeInHierarchy == true)
        {
            print(collision.gameObject.name);
            Destroy(this.gameObject, 0.02f);
        }
    }
}
