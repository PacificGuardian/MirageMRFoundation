using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTrig : MonoBehaviour{

    public AudioClip WinClip;
    public AudioClip LoseClip;
    public GameObject ParticleWin;
    private ParticleSystem parte;
    public float ColliderScale = 1;

    private void Start() {
        gameObject.AddComponent<Outline>().OutlineWidth = 10;
        BoxCollider BoxC = gameObject.AddComponent<BoxCollider>();
        BoxC.isTrigger = true;
        BoxC.size = new Vector3(ColliderScale, ColliderScale, ColliderScale);
    }
    private void OnTriggerEnter(Collider other){
        if(other.transform.childCount > 0){
        GameObject booK = other.transform.GetChild(0).gameObject;
        if(booK.gameObject.tag == "active"){
            if(booK.gameObject.GetComponent<AnswersContainer>().Correct)
            CorrectAnswer(booK.transform.position);
            else
            WrongAnswer(booK.transform.position);
            Destroy(booK);
            RobotAttacher.HandsUp = false;
        }
    }
    }

        
    private void CorrectAnswer(Vector3 bookpos){
        Debug.Log("Correct Answer Detected");
        Instantiate(Init.Singleton.effBase.SucessPar, bookpos, Quaternion.Euler(Vector3.up));
        GameEventContainer.AdvanceStage();
    }

    private void WrongAnswer(Vector3 bookpos){
        TimerBar.tmrt -= 10;
        Instantiate(Init.Singleton.effBase.FailPar, bookpos, Quaternion.Euler(Vector3.up));
        Debug.Log("Incorrect Answer Detected");
    }

    private void OnDestroy() {
        Destroy(gameObject.GetComponent<Outline>());
    }
}