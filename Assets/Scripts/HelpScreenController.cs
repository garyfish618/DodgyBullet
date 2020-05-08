using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HelpScreenController : MonoBehaviour
{

    public void ShowMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
