using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private GameObject player = null;

    public float rotationSpeed = 10f;
    public float shootSpeed = 5;
    public GameObject bullet;
    public Transform firePoint;

    private bool isShooting;

    private PersistenceController pc;
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PersistenceController").GetComponent<PersistenceController>();
        isShooting = false;
        player = pc.player;
    }

    // Update is called once per frame
    void Update()
    {
        //If player becomes null, try to reference pc's again
        if(player == null) {
            player = pc.player;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit)) {
            
            //Rotate enemy
            if(hit.transform == player.transform) {

               Vector3 lookDirection = player.transform.position - transform.position;
               lookDirection.y = 0;
               Quaternion rot = Quaternion.LookRotation(lookDirection);
               transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);

               StartCoroutine("ShootLaser");
            }          
        }       
    }

    IEnumerator ShootLaser()
    {

        if (isShooting)
        {
            yield break;
        }
        
        pc.bullets.Add(Instantiate(bullet, firePoint.position, firePoint.rotation));

        isShooting = true;
        yield return new WaitForSeconds(shootSpeed);
        isShooting = false;


    }
}
