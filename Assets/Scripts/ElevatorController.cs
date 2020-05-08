using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private PersistenceController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PersistenceController").GetComponent<PersistenceController>();;

    }

    private IEnumerator StartElevator() {
        if(pc.elevatorMoving) {
            yield break;
        }

        pc.elevatorMoving = true;
        GetComponent<Animator>().SetTrigger("ElevatorUp");
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetTrigger("ElevatorDown");
        yield return new WaitForSeconds(2);
        pc.elevatorMoving = false;
        

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {
            col.transform.SetParent(transform);
            StartCoroutine("StartElevator");
            

            if(col.contacts.Length > 0) {
                ContactPoint contact = col.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5) {
                    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col.collider);
                }
            }

        }
    }

    void OnCollisionExit(Collision col) {
        col.transform.SetParent(null);
        DontDestroyOnLoad(col.gameObject);

        if(col.gameObject.tag == "Player") {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col.collider, false);

        }
    }

}
