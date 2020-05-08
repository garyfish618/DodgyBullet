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
    private float verticalSensitivity = 80f;

    [SerializeField]
    private float horizontalSensitivity = 3f;

    public float jumpForce = 7;
    private float distToGround;
    private PersistenceController pc;

    public bool godMode = false;

    public bool dontDestroy = false;

    public AudioSource backgroundMusic;

    void Start()
    {
        pc = GameObject.Find("PersistenceController").GetComponent<PersistenceController>();;
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

        //Get movement vector
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        Vector3 horizontalMovement = transform.right * x;
        Vector3 verticalMovement = transform.forward * z;

        velocity = (horizontalMovement + verticalMovement).normalized * speed;



        if(velocity != Vector3.zero){
            //Stops rigidbody from moving if it collides
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);     

        }



        

    }

    void LateUpdate() {

        
        //Rotation -- Only the horizontal rotation (turning), verticle handled by camera
        float yRotation = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3 (0,yRotation,0) * horizontalSensitivity;

        if(rotation != Vector3.zero) {
            transform.Rotate(rotation);
        }



        //Vertical Rotation
        float xRotation = Input.GetAxisRaw("Mouse Y");
        cameraRotation = new Vector3 (xRotation, 0, 0) * verticalSensitivity;

        if(cameraRotation != Vector3.zero) {

            cam.transform.Rotate(-cameraRotation);

            float angle = (cam.transform.rotation.eulerAngles.x > 180) ? cam.transform.rotation.eulerAngles.x - 360 : cam.transform.rotation.eulerAngles.x;


            if(angle < -60) {
                cam.transform.localEulerAngles = new Vector3 (-60, 0, 0);             
            }

            else {
                if(angle > 60) {
                    cam.transform.localEulerAngles = new Vector3(60, 0, 0);
                }
            }
        }
    }


}
