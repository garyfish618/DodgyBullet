using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text ammo;
    public Text objectiveText;
    public Text destroyEnemies;
    public GameObject timeFreezeText;
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

        if(gameOver == null) {
            gameOver = GameObject.Find("Canvas/GameOver").transform.GetChild(0).gameObject;
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
            GameObject.Find("Level/HiddenDoor").GetComponent<Animator>().SetTrigger("OpenDoor");    

        }

        else {
            destroyEnemies.color = Color.red;
        }

        TimeFreezeActive(pc.timeFrozen);
    }

    private void TimeFreezeActive(bool timeActive){
        if(timeFreezeText == null){
            timeFreezeText = GameObject.Find("Canvas/TimeFreeze").transform.GetChild(0).gameObject;
        }
        
        timeFreezeText.SetActive(timeActive);
    }


    public void KillPlayer() {
        UpdateUI();
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
