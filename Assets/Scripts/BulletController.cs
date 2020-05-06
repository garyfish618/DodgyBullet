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
    private bool justSpawned;

    void Start() {
        StartCoroutine("WaitDamage");
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
        bounces = 0;
        pc = PersistenceController.Instance;
        ui =  GameObject.Find("UIController").GetComponent<UIController>();
    }
    // Update is called once per frame
    void Update()
    {


        if(pc.inGame && !pc.timeFrozen) {
            UnfreezeBullet();
        }

        else {
            FreezeBullet();
        }


    }

    private IEnumerator WaitDamage() {
        //Waits for bullet to leave enemy before it will damage an enemy
        justSpawned=true;
        yield return new WaitForSeconds(1);
        justSpawned=false;
    }

    public void FreezeBullet(){
        rb.velocity = Vector3.zero;
    }

    public void UnfreezeBullet() {
        rb.velocity = transform.forward * speed;
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

        if(col.gameObject.tag == "Enemy" && !justSpawned) {
            col.gameObject.GetComponent<EnemyController>().TakeDamage(5);
            Destroy(gameObject);
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
