using UnityEngine;

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

    public float jumpForce = 7;
    private float distToGround;
    private PersistenceController pc;

    public bool godMode = false;

    void Start()
    {
        pc = PersistenceController.Instance;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        //If in main menu, do nothing
        if(!pc.inGame) {
            return;
        }

        if(pc.isDead) {
            return;
        }

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

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb.AddForce(0, 5, 0, ForceMode.Impulse);
        }
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void FixedUpdate()
    {
        if(pc.isDead) {
            return;
        }

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
