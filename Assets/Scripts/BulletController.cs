using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int bouncesAllowed = 3;
    public float speed = 30f;
    public Rigidbody rb;
    private UIController ui = null;
    private PersistenceController pc;

    private int bounces;

    void Start() {
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
        bounces = 0;
        pc = PersistenceController.Instance;
    }
    // Update is called once per frame
    void Update()
    {


        if(pc.inGame) {
            rb.velocity = transform.forward * speed;

            if(ui == null) {
                ui = GameObject.Find("UIController").GetComponent<UIController>();
            }
        }

        else {
            rb.velocity = Vector3.zero;
        }


    }

    void OnCollisionEnter(Collision col) {

        
        if(col.gameObject.tag == "Bullet") {
            UnityEngine.Debug.Log("BulletCollide");
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }

        if (col.gameObject.tag == "Player") {
            if(col.gameObject.GetComponent<PlayerController>().godMode) {
                return;
            }
            Destroy(gameObject);
            ui.KillPlayer();
            
        }

        else {
            if(bounces == bouncesAllowed) {
                Destroy(gameObject);
            }

            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(transform.forward, col.GetContact(0).normal));
            bounces++;

        }
    }
}
