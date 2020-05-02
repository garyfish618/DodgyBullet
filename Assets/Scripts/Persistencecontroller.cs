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

    public Transform[] spawnLocationsLevelOne;

    public int currentLevel;

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
            //DontDestroyOnLoad(GameObject.Find("UIController"));
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
        SpawnPlayer();
    }

    public void ResetEnemies() {
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }

        SpawnEnemies(currentLevel);
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
            DontDestroyOnLoad(enemies[i]);
        }

    }

}
