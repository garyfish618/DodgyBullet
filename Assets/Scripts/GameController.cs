using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    private PersistenceController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = PersistenceController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape") && !pc.isDead) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pc.inGame = false;
            pc.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            SceneManager.LoadScene("MainMenu");
            pc.player.GetComponent<PlayerController>().backgroundMusic.Pause();
        }
    }
}
