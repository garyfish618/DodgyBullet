using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    PersistenceController pc;

    void Start() {
        pc = PersistenceController.Instance;
    }

    public void PlayButtonPressed(){
        pc.PlayButtonClicked();
    }


    public void HowButtonPressed(){
        pc.ShowHelp();
    }

    public void CreditsPressed(){
        pc.LoadCredits();
    }

    public void QuitPressed(){
        pc.QuitGame();
    }

}

