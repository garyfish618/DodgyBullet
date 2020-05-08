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
    public GameObject nextLevel;
    private PersistenceController pc;

    public bool dontDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = PersistenceController.Instance;
        ammo.text = pc.ammoInClip.ToString() + "/" + pc.ammoLeft.ToString();
        
    }

    public void UpdateUI() {
        UnityEngine.Debug.Log("Called");
        pc = PersistenceController.Instance;
       
        if(pc.enemiesLeft == 0 && pc.moneyLeft == 0) {
            destroyEnemies.color = Color.green;
            objectiveText.color = Color.green;
            //Level complete!
            nextLevel.SetActive(true);
            pc.isDead = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(pc.enemiesLeft == 0) {
            destroyEnemies.color = Color.green;
        }

        else {
            destroyEnemies.color = Color.red;
        }

        if(pc.moneyLeft == 0) {
            objectiveText.color = Color.green;
        }

        else {
            objectiveText.color = Color.red;
        }
        
        if(pc.timeFrozen) {
            timeFreezeText.SetActive(true);
        }

        else {
            timeFreezeText.SetActive(false);
        }

        ammo.text = pc.ammoInClip.ToString() + "/" + pc.ammoLeft.ToString();

        CheckDoorOpen();
    }

    //Open big door for level 2
    private void CheckDoorOpen() {

        if(pc.currentLevel == 2 && pc.enemiesLeft <= 6 && pc.inGame) {
            UnityEngine.Debug.Log("inside");
            GameObject.Find("Level/HiddenDoor").GetComponent<Animator>().SetTrigger("OpenDoor");  

        }
    }

    public void KillPlayer() {
        UpdateUI();
        pc.isDead = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);


    }

    public void NextLevel() {
        nextLevel.SetActive(false);
        pc.isDead = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pc.LoadNextLevel();
    }

    public void RespawnPlayer() {
        pc.isDead = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameOver.SetActive(false);

        
        pc.RespawnPlayer();
      
    }
}
