using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;

    public float rotationSpeed = 10f;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit)) {
            
            //Rotate enemy
            if(hit.transform == player.transform) {

               Vector3 lookDirection = player.transform.position - transform.position;
               lookDirection.y = 0;
               Quaternion rot = Quaternion.LookRotation(lookDirection);
               transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
            }

            ShootLaser();
        }

        
    }

    void ShootLaser() {

    }
}
