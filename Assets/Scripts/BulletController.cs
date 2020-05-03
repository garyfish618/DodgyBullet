using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody rb;
    private UIController ui = null;

    private int bounces;

    void Start() {
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
        bounces = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(PersistenceController.Instance.inGame && ui == null) {
            ui = GameObject.Find("UIController").GetComponent<UIController>();
        }

        rb.velocity = transform.forward * speed;

        if(rb.velocity == Vector3.zero) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {

        
        if(col.gameObject.tag == "Bullet") {
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
            if(bounces == 3) {
                Destroy(gameObject);
            }

            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(transform.forward, col.GetContact(0).normal));
            bounces++;

        }
    }
}
