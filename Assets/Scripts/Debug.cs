using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{

    //https://answers.unity.com/questions/1189486/how-to-see-fps-frames-per-second.html FPS counter props

     public Text fpsText;
     private float deltaTime;
     public bool isDebug;
 
    void Start () {

        if(isDebug) {
            fpsText.gameObject.SetActive(true);
        }
        

    }

     void Update () {
         if(isDebug) {
             
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = Mathf.Ceil (fps).ToString ();
         }

     }
}
