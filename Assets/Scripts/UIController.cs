﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text ammo = null;
    
    public PersistenceController pc;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI() {
        ammo.text = pc.ammoInClip.ToString() + "/" + pc.ammoLeft.ToString();
    }
}
