using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody rb;

    [SerializeField]
    public UIController ui = null;

    void Start() {
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            Destroy(gameObject);
            ui.KillPlayer();
            
        }

        else {
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(transform.forward, col.GetContact(0).normal));

        }
    }
}
