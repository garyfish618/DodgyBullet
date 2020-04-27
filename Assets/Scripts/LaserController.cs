using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }
}
