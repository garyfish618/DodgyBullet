using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private bool elevatorMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartElevator");

    }

    private IEnumerator StartElevator() {
        if(elevatorMoving) {
            yield break;
        }
        elevatorMoving = true;
        GetComponent<Animator>().SetTrigger("ElevatorUp");
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetTrigger("ElevatorDown");
        yield return new WaitForSeconds(2);
        elevatorMoving = false;
        

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {
            col.transform.SetParent(transform);
            StartCoroutine("StartElevator");

        }
    }

    void OnCollisionExit(Collision col) {
        col.transform.SetParent(null);
    }

}
