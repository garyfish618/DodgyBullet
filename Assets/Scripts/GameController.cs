using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    private PersistenceController pc;
    private UIController ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UIController").GetComponent<UIController>();
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

            //Use time freeze
        if(Input.GetKeyDown("x")) {
            if(pc.timeFreezes > 0) {
                pc.timeFreezes--;
                
                StartCoroutine("FreezeTime");
            }
        }
    }


    private IEnumerator FreezeTime() {
        if(pc.timeFrozen) {
            yield break;
        }

        pc.timeFrozen = true;
        ui.UpdateUI();
        yield return new WaitForSeconds(5);
        pc.timeFrozen = false;
        ui.UpdateUI();

    }
}
