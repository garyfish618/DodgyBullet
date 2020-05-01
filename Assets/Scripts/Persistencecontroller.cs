﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistenceController : MonoBehaviour
{
    public static PersistenceController Instance { get; private set; } // Set instance from ONLY within this class

    //Values below tied to instance not class

    public int ammoLeft;
    public int ammoInClip;

    public bool isDead;

    [SerializeField]
    private float SpawnX = 3.03f;

    [SerializeField]
    private float SpawnY = 1.0f;

    [SerializeField]
    private float SpawnZ = 3.36f;

    
    [SerializeField]
    private GameObject playerObj = null;

    public GameObject player;

    public ArrayList bullets;
    public ArrayList Enemies;

    public bool soundAudible;
    public bool inGame;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            soundAudible = true;
            inGame = true;
            bullets = new ArrayList();
            Enemies = new ArrayList();
            SpawnPlayer();
            isDead = false;
            soundAudible = true;
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
        player = Instantiate(playerObj, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.Euler(0,0,0));
        DontDestroyOnLoad(player);

    }

    public void DestroyPlayer() {
        Destroy(player);
    }

}
