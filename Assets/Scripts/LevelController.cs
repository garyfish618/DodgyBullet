using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private PersistenceController pc;
    public int ammoAtStart;
    void Awake() {
        pc = PersistenceController.Instance;
        pc.startingAmmo=ammoAtStart;
        pc.StartLevel();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("UIController").GetComponent<UIController>().UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
