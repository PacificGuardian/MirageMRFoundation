using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForestAnim : MonoBehaviour {
    public GameObject MainCamera;
    private void Awake() {
        GameEventContainer.start += StartEffects;
        GameEventContainer.stageChange += StageChange;
        GameEventContainer.end += Finale;
        GameEventContainer.TimeEnd += BadFin;
    }

    private void StartEffects(){
        MainCamera.GetComponentInChildren<Camera>().enabled = false;
        GameObject SceCamGO = new GameObject();
        Camera CutCam = SceCamGO.AddComponent<Camera>();
    }

    private void StageChange(){

    }

    private void Finale(){

    }

    private void BadFin(){

    }

}