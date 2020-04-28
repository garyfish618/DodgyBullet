﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    [SerializeField]
    private float speed = 5.5f;
    
    [SerializeField]
    private float sensitivity = 3.5f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
         //Get movement vector
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        Vector3 horizontalMovement = transform.right * x;
        Vector3 verticalMovement = transform.forward * z;

        velocity = (horizontalMovement + verticalMovement).normalized * speed;

        //Rotation -- Only the horizontal rotation (turning), verticle handled by camera
        float yRotation = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3 (0,yRotation,0) * sensitivity;

        //Vertical Rotation
        float xRotation = Input.GetAxisRaw("Mouse Y");
        cameraRotation = new Vector3 (xRotation, 0, 0) * sensitivity;
    }

    void FixedUpdate()
    {
       
        if(velocity != Vector3.zero){
            //Stops rigidbody from moving if it collides
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);     

        }

        if(rotation != Vector3.zero) {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        }

        if(cameraRotation != Vector3.zero) {

            cam.transform.Rotate(-cameraRotation);

            float angle = (cam.transform.rotation.eulerAngles.x > 180) ? cam.transform.rotation.eulerAngles.x - 360 : cam.transform.rotation.eulerAngles.x;


            if(angle < -35) {
                cam.transform.localEulerAngles = new Vector3 (-35, 0, 0);             
            }

            else {
                if(angle > 35) {
                    cam.transform.localEulerAngles = new Vector3(35, 0, 0);
                }
            }
        }
 

    }


}
