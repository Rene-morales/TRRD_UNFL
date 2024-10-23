using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody bulletRb;

    public float power = 100f;
    public float lifetime = 2f;

    private float time = 0f;


    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();

        bulletRb.velocity = this.transform.forward * power;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= lifetime)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.activeInHierarchy == true)
        {
            Destroy(this.gameObject, 0.02f);
        }
    }
}
