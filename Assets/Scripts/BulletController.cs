using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody rb;
    private UIController ui = null;

    void Start() {
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
        ui = GameObject.Find("UIController").GetComponent<UIController>();
    }
    // Update is called once per frame
    void Update()
    {
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
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(transform.forward, col.GetContact(0).normal));

        }
    }
}
