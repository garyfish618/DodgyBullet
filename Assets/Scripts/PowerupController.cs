using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private PersistenceController pc;
    private UIController ui;
    public bool isTimeFreeze;

    
    [SerializeField]
    private int AmmoInBox = 0;

    void Start() {
        ui = GameObject.Find("UIController").GetComponent<UIController>();
        pc = PersistenceController.Instance;
    }

    void OnCollisionEnter(Collision col) {
        UnityEngine.Debug.Log("Cal");
        if(col.gameObject.tag == "Player") {
            if(gameObject.tag == "Ammo") {
                pc.ammoLeft += AmmoInBox;
                ui.UpdateUI();
            }

            if(gameObject.tag == "Money") {
                pc.RemoveMoney();
            }

            if(isTimeFreeze) {
                pc.timeFreezes++;
                
            }
            Destroy(gameObject);
        }
    }
}
