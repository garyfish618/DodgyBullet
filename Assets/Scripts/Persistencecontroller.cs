using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceController : MonoBehaviour
{
    public static PersistenceController Instance { get; private set; } // Set instance from ONLY within this class

    //Values below tied to instance not class

    public int ammoLeft;
    public int ammoInClip;

    public bool isDead;

    private void Awake()
    {
        if (Instance == null)
        {
            isDead = false;
            DontDestroyOnLoad(gameObject); // gameObject = the game object this script lives on
        }

        else //Gives singleton property. stops unity from trying to duplicate and create more instances
        {
            Destroy(gameObject);
        }
    }

}
