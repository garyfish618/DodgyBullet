using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistenceController : MonoBehaviour
{
    public static PersistenceController Instance { get; private set; } // Set instance from ONLY within this class

    //Values below tied to instance not class

    public int ammoLeft;
    public int ammoInClip;

    private int startingAmmo;
    private int startingClip;

    public bool isDead;
    
    [SerializeField]
    private GameObject playerObj = null;

    [SerializeField]
    private GameObject enemyObj = null;

    public GameObject player;

    public ArrayList bullets;
    
    [HideInInspector]
    public GameObject[] enemies;

    [HideInInspector]
    public GameObject[] ammoBoxes;

    [HideInInspector]
    public GameObject[] money;

    public Transform[] spawnLocationsLevelOne;
    public int currentLevel;

    private UIController ui;

    private ArrayList powerUps;
    public int moneyLeft;

    public int enemiesLeft;

    public bool soundAudible;
    public bool timeFrozen;
    public bool inGame;
    public bool elevatorMoving;

    public int timeFreezes;

    public int moneyAtStart;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            startingClip = ammoInClip;
            startingAmmo = ammoLeft;
            soundAudible = true;
            timeFrozen = false;
            elevatorMoving = false;
            inGame = true;
            bullets = new ArrayList();
            SpawnPlayer();
            isDead = false;
            soundAudible = true;
            SpawnEnemies(currentLevel);
            ui = GameObject.Find("UIController").GetComponent<UIController>();
            timeFreezes = 0;
            moneyAtStart = 0;
            moneyLeft = 0;
            powerUps = new ArrayList();

            //Dont destroy powerups
            foreach(GameObject power in GameObject.FindGameObjectsWithTag("Powerup")) {
                DontDestroyOnLoad(power);
                powerUps.Add(power);
                //Mark as an original
                power.GetComponent<PowerupController>().dontDestroy = true;
            }


            //Dont destory spawnpoints
            foreach(GameObject spawn in GameObject.FindGameObjectsWithTag("Spawn")) {
                DontDestroyOnLoad(spawn);
            }

            DontDestroyOnLoad(ui.gameObject);
            DontDestroyOnLoad(gameObject); // gameObject = the game object this script lives on
        }

        else //Gives singleton property. stops unity from trying to duplicate and create more instances
        {
            Destroy(gameObject);
        }
    }


    public void SpawnPlayer() {
        if(player != null) {
            DestroyPlayer();
        }

        //Spawn player
        player = (GameObject)Instantiate(playerObj, playerObj.transform.position, playerObj.transform.rotation);
        player.GetComponent<PlayerController>().backgroundMusic.Play();
        DontDestroyOnLoad(player);
    }

    public void ResetAmmo() {
        ammoLeft = startingAmmo;
        ammoInClip = startingClip;
    }

    public void DestroyPlayer() {
        Destroy(player);
    }

    public void RespawnPlayer() {
        DestroyPlayer();
        ResetAmmo();
        ResetBullets();
        GameObject.Find("UIController").GetComponent<UIController>().UpdateUI();
        ResetEnemies();
        ResetPowerups();
        SpawnPlayer();
        ui.UpdateUI();
        timeFreezes = 0;
    }

    public void ResetBullets() {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for(int i = 0; i < bullets.Length; i++) {
            Destroy(bullets[i]);
        } 
    }

    public void ResetEnemies() {
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }
        enemiesLeft = 0;

        SpawnEnemies(currentLevel);
    }

    public void ResetPowerups() {
        moneyLeft = moneyAtStart;
        timeFreezes = 0;
        timeFrozen = false;

        foreach(GameObject power in powerUps) {
            power.SetActive(true);
        }
    }

    public void SpawnEnemies(int level) {
        Transform[] currentEnemies;
        switch (level) {
            
            case 1:
                currentEnemies = spawnLocationsLevelOne;
                break;

            default:
                currentEnemies = null;
                UnityEngine.Debug.LogError("Invalid level provided to SpawnEnemies");
                break;
        }

        enemies = new GameObject[currentEnemies.Length];
        
        for(int i = 0; i < currentEnemies.Length; i++) {
            enemies[i] = ((GameObject)Instantiate(enemyObj, currentEnemies[i].transform.position, currentEnemies[i].transform.rotation));
            
            if(currentEnemies[i].tag == "FreezeEnemy") {
                enemies[i].GetComponent<EnemyController>().freezeEnemy = true;
            }
            enemiesLeft++;
            DontDestroyOnLoad(enemies[i]);
        }
    }

    public void RemoveMoney() {
        moneyLeft -= 1;

        if(moneyLeft == 0) {

           ui.UpdateUI();
        }

    }
}
