using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServoTesting : MonoBehaviour{
    bool tempHandState;
    SerialController wired;
    private void Start() {
        wired = gameObject.AddComponent<SerialController>();
    }
    private void Update() {
        bool tempBool = RobotAttacher.HandsUp;
        if(tempHandState != tempBool){
            if(tempBool){
            wired.SendSerialMessage("u");
            Debug.Log("Up");
            }
            else if(!tempBool){
            wired.SendSerialMessage("d");
            Debug.Log("Down");
            }
            tempHandState = RobotAttacher.HandsUp;
        }
    }
    
}