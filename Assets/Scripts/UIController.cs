using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text ammo;
    public Text objectiveText;
    public Text destroyEnemies;
    public GameObject gameOver;
    
    private PersistenceController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = PersistenceController.Instance;
        ammo.text = pc.ammoInClip.ToString() + "/" + pc.ammoLeft.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateUI() {
        if(ammo == null)
        {
            ammo = GameObject.Find("Canvas/Ammo").GetComponent<Text>();
        }

        if(objectiveText == null) {
            objectiveText = GameObject.Find("Canvas/ObjectiveText").GetComponent<Text>();
        }

         if(destroyEnemies == null) {
            destroyEnemies = GameObject.Find("Canvas/DestroyEnemies").GetComponent<Text>();
        }

        ammo.text = pc.ammoInClip.ToString() + "/" + pc.ammoLeft.ToString();

        if(pc.moneyLeft == 0) {
            objectiveText.color = Color.green;
        }

        else {
            objectiveText.color = Color.red;
        }

        if(pc.enemiesLeft == 0) {
            destroyEnemies.color = Color.green;
        }

        else {
            destroyEnemies.color = Color.red;
        }
    }


    public void KillPlayer() {

        pc.isDead = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);


    }

    public void RespawnPlayer() {
        //Destroy existing bullet objects
        foreach(GameObject bullet in pc.bullets) {
            Destroy(bullet);
        }

        pc.isDead = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameOver.SetActive(false);

        
        pc.RespawnPlayer();
      
    }
}
