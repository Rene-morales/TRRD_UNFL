using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Currenthealth;
    public int MaxHealth = 4;

    void Start()
    {
        Currenthealth = MaxHealth;
    }


    public void TakeDamage(int amount)
    {
        Currenthealth -= amount;
        if (Currenthealth <= 0)
        {
            //Destroy(gameObject);
            print("ded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
