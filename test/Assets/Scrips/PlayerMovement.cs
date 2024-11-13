using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public Collider coll;
    public Rigidbody rb3d;
    public float walkSpeed = 6f;
    public float runSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
  
    public Animator CamSwitch;
    public WeaponController weaponController;
    float pushPower = 2.0f;


    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private bool CameraUnlock = false;
    private Quaternion originalRotation;

    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        {
            coll = GetComponent<Collider>();
        }

        originalRotation = CamSwitch.transform.rotation;

        if(weaponController == null)
        {
            weaponController = GetComponentInChildren<WeaponController>();
        }
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
            CamSwitch.transform.rotation *= Quaternion.Euler(Input.GetAxis("Mouse Y") * lookSpeedY, 0, 0);
        }

        if (CameraUnlock)
        {
            rotationX += Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        }

        else
        {
            CamSwitch.transform.localRotation = originalRotation;

        }


        if (Input.GetMouseButtonDown(1))
        {
            CameraUnlock = !CameraUnlock;
            CamSwitch.SetTrigger("Switch trigger");
            

        }

        if (Input.GetMouseButtonDown(0) && weaponController != null)
        {
            Vector3 shootDirection = playerCamera.transform.forward;

            Debug.Log("Dirección de disparo: " + shootDirection);

            weaponController.InstantiateBullet(shootDirection);
        }



    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print(hit.gameObject.name); 

        if (hit.gameObject.CompareTag("PLATF"))
        {
            hit.gameObject.GetComponent<ESTAC_PLAT_CODE>().Fall();
        }

        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.2)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower;

    }
}

