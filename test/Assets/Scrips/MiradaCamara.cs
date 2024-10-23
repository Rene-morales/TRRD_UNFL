using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiradaCamara : MonoBehaviour
{
    public float Velocity = 100f;
    float RotationX = 0f;

    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * Velocity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * Velocity * Time.deltaTime;

        RotationX -= MouseY;
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        player.Rotate(Vector3.up * MouseX);
    }
}
