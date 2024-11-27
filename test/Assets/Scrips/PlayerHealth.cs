using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int Currenthealth;
    public int MaxHealth = 4;
    public TextMeshProUGUI TxtVidas;

    void Start()
    {
        Currenthealth = MaxHealth;
    }


    public void TakeDamage(int amount)
    {
        print("El jugador recibe daño: " + amount);
        Currenthealth -= amount;       
    }

    // Update is called once per frame
    void Update()
    {
        TxtVidas.text = Currenthealth.ToString();
        if (Currenthealth <= 0)
        {
            //Destroy(gameObject.tag"player");
            print(gameObject.name + " ded");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(1);
        }
    }
}
