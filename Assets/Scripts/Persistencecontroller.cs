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

    [SerializeField]
    private GameObject moneyObj = null;

    [SerializeField]
    private GameObject ammoObj = null;

    public GameObject player;

    public ArrayList bullets;
    
    [HideInInspector]
    public GameObject[] enemies;

    [HideInInspector]
    public GameObject[] ammoBoxes;

    [HideInInspector]
    public GameObject[] money;

    public Transform[] spawnLocationsLevelOne;

    public Transform[] ammoLocationsLevelOne;

    public Transform[] moneyLocationsLevelOne;
    public int currentLevel;

    private UIController ui;
    public int moneyLeft;

    public int enemiesLeft;

    public bool soundAudible;
    public bool inGame;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            startingClip = ammoInClip;
            startingAmmo = ammoLeft;
            soundAudible = true;
            inGame = true;
            bullets = new ArrayList();
            SpawnPlayer();
            isDead = false;
            soundAudible = true;
            SpawnEnemies(currentLevel);
            SpawnPowerUps(currentLevel);
            ui = GameObject.Find("UIController").GetComponent<UIController>();
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
        GameObject.Find("UIController").GetComponent<UIController>().UpdateUI();
        ResetEnemies();
        ResetPowerups();
        SpawnPlayer();
        ui.UpdateUI();
    }

    public void ResetEnemies() {
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }
        enemiesLeft = 0;

        SpawnEnemies(currentLevel);
    }

    public void ResetPowerups() {
        moneyLeft = 0;

        foreach(GameObject money in money) {
            Destroy(money);
        }

        foreach(GameObject ammo in ammoBoxes) {
            Destroy(ammo);
        }

        SpawnPowerUps(currentLevel);
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

    public void SpawnPowerUps(int level) {
        Transform[] currentAmmoBoxes;
        Transform[] currentMoney;
        switch (level) {
            
            case 1:
                currentAmmoBoxes = ammoLocationsLevelOne;
                currentMoney = moneyLocationsLevelOne;
                break;

            default:
                currentAmmoBoxes = null;
                currentMoney = null;
                UnityEngine.Debug.LogError("Invalid level provided to SpawnPowerups");
                break;
        }

        ammoBoxes = new GameObject[currentAmmoBoxes.Length];
        money = new GameObject[currentMoney.Length];
        
        for(int i = 0; i < currentAmmoBoxes.Length; i++) {
            ammoBoxes[i] = ((GameObject)Instantiate(ammoObj, currentAmmoBoxes[i].transform.position, currentAmmoBoxes[i].transform.rotation));
            DontDestroyOnLoad(ammoBoxes[i]);
        }

         for(int i = 0; i < currentMoney.Length; i++) {
            moneyLeft++;
            money[i] = ((GameObject)Instantiate(moneyObj, currentMoney[i].transform.position, currentMoney[i].transform.rotation));
            DontDestroyOnLoad(money[i]);
        }
    }

    public void RemoveMoney() {
        UnityEngine.Debug.Log(moneyLeft);
        moneyLeft -= 1;

        if(moneyLeft == 0) {

           ui.UpdateUI();
        }

    }
}
