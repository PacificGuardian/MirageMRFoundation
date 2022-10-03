using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRegistry : MonoBehaviour{
    private void Awake(){
    GameEventContainer.start += StartEvent;
    GameEventContainer.stageChange += BookSwap;
    GameEventContainer.end += EndEvent;
    GameEventContainer.TimeEnd += EndEvent;
    }
    
    private void StartEvent(){
       Debug.Log("Starting");
    }

    private void BookSwap(){
        Init.Singleton.BookSwap();
    }

    private void EndEvent(){
        Debug.Log("Ending");
        Init.Singleton.BookStop();
    }
}