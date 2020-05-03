using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private PersistenceController pc;
    
    [SerializeField]
    private int AmmoInBox = 0;

    void Start() {
        pc = PersistenceController.Instance;
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Player") {
            if(gameObject.tag == "Ammo") {
                pc.ammoLeft += AmmoInBox;
                GameObject.Find("UIController").GetComponent<UIController>().UpdateUI();
                Destroy(gameObject);
            }

            if(gameObject.tag == "Money") {
                pc.removeMoney();
                Destroy(gameObject);
            }
        }
    }
}
