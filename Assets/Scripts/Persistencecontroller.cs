using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PersistenceController : MonoBehaviour
{
    public static PersistenceController Instance { get; private set; } // Set instance from ONLY within this class

    //Values below tied to instance not class

    public int ammoLeft;
    public int ammoInClip;

    private int startingAmmo;
    private int startingClip;

    public bool isDead;
    public GameObject player;

    public ArrayList bullets;
    
    public int currentLevel;

    private UIController ui;
    public GameObject gameCanvas;

    private ArrayList powerUps;
    public int moneyLeft;

    public int enemiesLeft;

    public bool soundAudible;
    public bool timeFrozen;
    public bool inGame;
    public bool elevatorMoving;

    public int timeFreezes;

    public bool initialLoadDone;

    private bool levelFreshStart;


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
            isDead = false;
            soundAudible = true;
            timeFreezes = 0;
            currentLevel = 0;
            levelFreshStart = true;
            powerUps = new ArrayList();          
            
            DontDestroyOnLoad(gameObject); // gameObject = the game object this script lives on
        }

        else //Gives singleton property. stops unity from trying to duplicate and create more instances
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKey("escape") && !isDead && !elevatorMoving && inGame) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inGame = false;

            //Hide in game canvas
            gameCanvas.SetActive(false);
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            SceneManager.LoadScene("MainMenu");
            player.GetComponent<PlayerController>().backgroundMusic.Pause();
        }

        //Use time freeze
        if(Input.GetKeyDown("x") && inGame && !isDead) {
            if(timeFreezes > 0) {
                timeFreezes--;
                
                StartCoroutine("FreezeTime");
            }
        }
    }

    private IEnumerator FreezeTime() {
        if(timeFrozen) {
            yield break;
        }
        timeFrozen = true;
        ui.UpdateUI();
        yield return new WaitForSeconds(5);
        timeFrozen = false;
        ui.UpdateUI();
    }


    public void StartLevel() {
        
        if(currentLevel == 0) {
            ui = GameObject.Find("UIController").GetComponent<UIController>();
            ui.dontDestroy = true;
            gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
            gameCanvas.GetComponent<CanvasController>().dontDestroy = true;
            DontDestroyOnLoad(ui.gameObject);
            DontDestroyOnLoad(gameCanvas);
            
            currentLevel++;
            levelFreshStart = false;
        }

        if(initialLoadDone) {
            RemoveDuplicates();
        }

        inGame = true;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController play = player.GetComponent<PlayerController>();
        play.dontDestroy = true;

 
        if(!play.backgroundMusic.isPlaying) {
            play.backgroundMusic.Play();
        }
        
        
        DontDestroyOnLoad(player);

         //Dont destroy powerups
        foreach(GameObject power in GameObject.FindGameObjectsWithTag("Powerup")) {
            DontDestroyOnLoad(power);
            powerUps.Add(power);
            //Mark as an original
            power.GetComponent<PowerupController>().dontDestroy = true;
        }

        //Dont destory enemies
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            DontDestroyOnLoad(enemy);
            enemy.GetComponent<EnemyController>().dontDestroy = true;
        }
        initialLoadDone = true;
    }

    //This method removes duplicate canvas', enemies, players, and power-ups that unity attempts to create when re-loading a scene
    public void RemoveDuplicates() {
        GameObject[] canvases = GameObject.FindGameObjectsWithTag("GameCanvas");
        GameObject[] uiControllers = GameObject.FindGameObjectsWithTag("uiController");

        for(int i = 0; i < canvases.Length; i++) {
            GameObject canv = canvases[i];
            if(!canv.GetComponent<CanvasController>().dontDestroy) {
                Destroy(canv);
            }
        }

        for(int i = 0; i < uiControllers.Length; i++) {
            GameObject uiCont = uiControllers[i];
            if(!uiCont.GetComponent<UIController>().dontDestroy) {
                Destroy(uiCont);
            }
        }

        if(levelFreshStart) {
            levelFreshStart = false;
            return;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        
        for(int i = 0; i < players.Length; i++) {
            GameObject player = players[i];
            if(!player.GetComponent<PlayerController>().dontDestroy) {
                Destroy(player);
            }
        }

        for(int i = 0; i < enemies.Length; i++) {
            GameObject enemy = enemies[i];
            if(!enemy.GetComponent<EnemyController>().dontDestroy) {
                Destroy(enemy);
            }
        }

        for(int i = 0; i < powerups.Length; i++) {
            GameObject power = powerups[i];
            if(!power.GetComponent<PowerupController>().dontDestroy) {
                Destroy(power);
            }
        }


     
    }

    public void LoadCurrentLevel() {
          switch(currentLevel) {
            case 1:
                SceneManager.LoadScene("LevelOne");
                break;
            
            case 2:
                SceneManager.LoadScene("LevelTwo");
                break;

            case 3:
                SceneManager.LoadScene("LevelThree");
                break;

            default:
                UnityEngine.Debug.LogError("Invalid level!");
                break;
        }
    }

    public void LoadNextLevel() {
        CleanUp();
        levelFreshStart = true;
        currentLevel++;

        switch(currentLevel) {

            case 1:
                SceneManager.LoadScene("LevelOne");
                break;
            
            case 2:
                SceneManager.LoadScene("LevelTwo");
                break;

            case 3:
                SceneManager.LoadScene("LevelThree");
                break;

            default:
                UnityEngine.Debug.LogError("Invalid level!");
                break;
        }
    }

    public void PlayButtonClicked() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inGame = true;
        if(currentLevel == 0) {
            SceneManager.LoadScene("LevelOne");   
            return;     
        }
        player.GetComponent<PlayerController>().backgroundMusic.UnPause();
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        LoadCurrentLevel();
        gameCanvas.SetActive(true);
    }

    public void ShowHelp()
    {
        SceneManager.LoadScene("HowtoPlay");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            Application.Quit();

        #endif
    }

    public void RespawnPlayer() {
        CleanUp();
        ammoInClip = startingClip;
        ammoLeft = startingAmmo;
        levelFreshStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CleanUp() {
        Destroy(player);
        //Destroy all bullets
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for(int i = 0; i < bullets.Length; i++) {
            Destroy(bullets[i]);
        } 

        //Destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++) {
            Destroy(enemies[i]);
        }

        //Destroy all powerups
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("Powerup");
        for(int i = 0; i < powerUps.Length; i++) {
            Destroy(powerUps[i]);
        }

        moneyLeft = 0;
        enemiesLeft = 0;
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void RemoveMoney() {
        moneyLeft -= 1;
        UnityEngine.Debug.Log("Money:" + moneyLeft);
        if(moneyLeft == 0) {

           ui.UpdateUI();
        }

    }
}
