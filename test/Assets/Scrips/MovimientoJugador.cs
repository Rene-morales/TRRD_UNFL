using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    #region Public_Variables
    [Header("Movement")]
    public float accelerationSpeed;
    public float deaccelerationSpeed;
    public int maxSpeed;

    [Header("Jump")]
    public float jumpForce;

    [Header("Raycast - Ground")]
    public LayerMask groundMask;
    public float rayLength;
    #endregion

    #region Private_Variables
    Vector2 horizontalMovement;
    Vector3 slowdown;

    bool isGrounded;
    bool jumpPressed;
    Ray ray;
    RaycastHit hit;
    Rigidbody rb;
    float horizontal;
    float vertical;
    #endregion


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        InputPlayer();
        JumpPressed();
    }
    private void FixedUpdate()
    {
        IsGrounded();
        Movement();
        jump();
    }
    #region Jump
    void JumpPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            jumpPressed = true;
        }
    }
    void jump()
    {
        if(jumpPressed == true)
        {
            jumpPressed = false;
            rb.AddRelativeForce(Vector3.up * jumpForce);
        }
    }
    #endregion
    void Movement()
    {
        //Limitar la velocidad del jugador
        //guardar la velocidad que tiene rigidbody en variable horizontalMovement
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
        if(horizontalMovement.magnitude > maxSpeed)
        {
            // Se va a limitar la velocidad del jugador porque si cumple la condición del if
            // significa que supero la velocidad máxima

            //Se normaliza el vector, conservando la dirección y haciendo que su longitud valga 1

            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement = horizontalMovement * maxSpeed;
        }
        rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.y);

        //Desacelerar el rigidbody
        if (isGrounded)
            rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(0, rb.velocity.y, 0), ref slowdown, deaccelerationSpeed);

        // Si estás en el suelo, añado movimiento al jugador
        if(isGrounded)
        {
            rb.AddRelativeForce(horizontal * accelerationSpeed * Time.deltaTime, 0,
                vertical * accelerationSpeed * Time.deltaTime);
        }
        else// si no está tocando el suelo, añado fuerza de menor magnitud
        {
            rb.AddRelativeForce(horizontal * accelerationSpeed /2 * Time.deltaTime, 0,
                vertical * accelerationSpeed /2 * Time.deltaTime);
        }
    }
    void InputPlayer()
    {
        //AD, Si el jugador no pulsa tecla, el axis se devuelve a 0, 1 si pulsa D, -1 si pulsa A
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");//WS
    }
   //Función que se encarga de decirme si el jugador toca el suelo o no mediante un
   //raycast
    void IsGrounded()
    {
        ray.origin = transform.position;
        ray.direction = -transform.up;

        //Se usa el raycast, con una longitud y selectivo (Solo va a detectar los objetos
        //de una determinada capa
        if(Physics.Raycast(ray, out hit, rayLength, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
    }

}
