using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESTAC_PLAT_CODE : MonoBehaviour
{
    public Rigidbody rb3d;
    public Collider coll;
    public float vel;
    //private float time = 0.0f;
    public float pushPower = 50.0f;
    Collider m_ObjectCollider;
    public bool fallfree;
    public bool nailed;

    // Start is called before the first frame update
    void Start()
    {
        m_ObjectCollider = GetComponent<Collider>();
        m_ObjectCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    
    void OnCollisionEnter(Collision collision)
    {
        print("hola");
        
        rb3d.useGravity = true;
        fallfree = (rb3d.velocity.y > 0);


        Rigidbody body = collision.collider.attachedRigidbody;

        //no rigibodyy
        if (body == null)
        {
            return;
        }

        if (fallfree)
        {
            print(body.gameObject.name);
            Vector3 pushDir = new Vector3(body.transform.position.x - transform.position.x, 0, body.transform.position.z - transform.position.z);
            body.AddForce(pushDir.normalized * pushPower);
            body.AddForce(Vector3.up * pushPower);
        }

        print(collision.gameObject.tag);        

        if (collision.gameObject.tag == "LIMIT")
        {
            rb3d.isKinematic = true;

        }
        //nailed = (collision.gameObject.tag == "LIMIT");
        
    }

    public void Fall()
    {
        print("funcion estalac");

    }

}
 
