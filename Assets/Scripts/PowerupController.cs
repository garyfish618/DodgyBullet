using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private PersistenceController pc;
    private UIController ui;
    public bool isTimeFreeze;
    public bool isAmmo;
    public bool isMoney;

    //Marked as not destroyable
    public bool dontDestroy = false;

    
    [SerializeField]
    private int AmmoInBox = 0;

    void Start() {
        ui = GameObject.Find("UIController").GetComponent<UIController>();
        pc = GameObject.Find("PersistenceController").GetComponent<PersistenceController>();;

        if(isMoney) {
            pc.moneyLeft++;
        }
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Player") {
            if(isAmmo) {
                pc.ammoLeft += AmmoInBox;
                ui.UpdateUI();
            }

            if(isTimeFreeze) {
                pc.timeFreezes++;
                
            }

            if(isMoney) {
                pc.RemoveMoney();
            }
            gameObject.SetActive(false);
        }
    }
}
