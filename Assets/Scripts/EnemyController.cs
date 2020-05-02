using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    private GameObject player = null;

    public float rotationSpeed = 10f;
    public float shootSpeed = 5;
    public GameObject bullet;
    public Transform firePoint;

    private AudioSource enemyDeathSound;

    private NavMeshAgent navMeshAgent;
    private bool isShooting;

    private PersistenceController pc;

    public Slider healthBar;
    private bool snappedToPlayer;
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
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Dont do anything if out of game
        if(!pc.inGame) {
            return;
        }

        //If player becomes null, try to reference pc's again
        if(player == null) {
            player = pc.player;
        }

        navMeshAgent.SetDestination(player.transform.position);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit)) {
            
            //Rotate enemy
            if(hit.transform == player.transform) {
                Vector3 lookDirection = player.transform.position - transform.position;
                lookDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDirection);
                navMeshAgent.updatePosition = true;


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
        Animator anim = gameObject.GetComponentInChildren(typeof(Animator)) as Animator;
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
