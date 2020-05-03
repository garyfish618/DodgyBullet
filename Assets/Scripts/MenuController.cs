using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void PlayGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(PersistenceController.Instance != null) {
            PersistenceController.Instance.inGame = true;
            PersistenceController.Instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            PersistenceController.Instance.player.GetComponent<PlayerController>().backgroundMusic.UnPause();
        }
        SceneManager.LoadScene("MainGame");
        
    }

    public void ShowHelp()
    {
        Application.OpenURL("https://rijeka.sdsu.edu/Gfishell/cs583s2020_fishell_gary_proj_02/-/wikis/How-to-Play");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            Application.Quit();

        #endif
    }


}
