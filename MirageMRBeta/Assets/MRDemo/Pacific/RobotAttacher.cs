using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RobotAttacher : MonoBehaviour
{
    public static GameObject Robot;
    public GameObject Carried = null;
    private GameObject Target = null; 
    public static bool HandsUp = false;
    private Outline outline;
    [SerializeField]
    private Vector3 Pickedup;

    [SerializeField]
    bool LogTriggers = false;
    public SteamVR_Action_Single ReleasePressed;
    private void Awake(){
        Robot = this.gameObject;
    }
    private void OnTriggerEnter(Collider other) {
        if(LogTriggers)
        Debug.Log(other.name);
        if(other.tag == "unactive")
        Target = other.gameObject;            
    }
    private void OnTriggerExit(Collider other){
        if(Target == other.gameObject)
        Target = null;
    }

    private void Pickup(GameObject other){
        //Debug.Log("Attempting to pickup object");
        if(Carried == null && other != null){
                Carried = other.gameObject;
                outline = Carried.AddComponent<Outline>();
                other.gameObject.transform.SetParent(this.gameObject.transform);
                other.gameObject.transform.localPosition = Pickedup;
                Destroy(other.GetComponent<Rigidbody>());
                other.tag = "active";
                Carried.transform.LookAt(gameObject.transform);
                Debug.Log("Attached to " + other.GetComponent<AnswersContainer>().Answer);
                HandsUp = true;
            }
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1") || ReleasePressed.GetAxis(SteamVR_Input_Sources.Any) > 0.9f)
        Pickup(Target);
        //if(Input.GetButtonDown)
        if(ReleasePressed.GetAxis(SteamVR_Input_Sources.Any) > 0.9f || Input.GetButtonDown("Cancel"))
        //if(Input.GetButtonDown("Cancel"))
        Release();
    }

    public void Release(){
        if(Carried != null){
            if(!Carried.GetComponent<AnswersContainer>().Correct)
            {
            Instantiate(Init.Singleton.effBase.FailPar,Carried.transform.position,Quaternion.identity).transform.SetParent(Init.Singleton.PPar.transform);
            Destroy(Carried);
            TimerBar.tmrt -= 5;
            HandsUp = false;
            }
            else
            Debug.Log("Burg");
        }
    }
}