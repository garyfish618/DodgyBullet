using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    
    private GameObject player = null;

    public float rotationSpeed = 10f;
    public float shootSpeed = 5;
    public GameObject bullet;
    public Transform firePoint;

    private AudioSource enemyDeathSound;

    private bool isShooting;

    private PersistenceController pc;

    public Slider healthBar;
    private bool isDead;
    public int health = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyDeathSound = GetComponent<AudioSource>();
        pc = PersistenceController.Instance;
        isShooting = false;
        player = pc.player;
        healthBar.value = health;

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
               lookDirection.Normalize();
               transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);

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

    public void TakeDamage(int dmg) {
        if(health - dmg <= 0) {
            if(isDead) {
                return;
            }
            isDead = true;
            StartCoroutine("KillEnemy");
            return;
        }

        health -= dmg;
        healthBar.value = health;
    }

    private IEnumerator KillEnemy() {
        enemyDeathSound.Play();
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
