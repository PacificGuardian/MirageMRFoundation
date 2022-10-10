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
    private float releaseCD = 2;
    private float time;
    private Outline outline;
    [SerializeField]
    private Vector3 Pickedup;

    [SerializeField]
    bool LogTriggers = false;
    public SteamVR_Action_Single ReleasePressed;
    private void Start(){
        Robot = this.gameObject;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "unactive"){
        Target = other.gameObject;        
        if(LogTriggers)
        Debug.Log(other.name);
        }    
    }
    private void OnTriggerExit(Collider other){
        if(Target == other.gameObject)
        Target = null;
    }

    private IEnumerator Pickup(GameObject other){
        //Debug.Log("Attempting to pickup object");
        if(Carried == null && other != null){
                Carried = other.gameObject;
                outline = Carried.AddComponent<Outline>();
                outline.OutlineWidth = 50;
                other.gameObject.transform.SetParent(this.gameObject.transform);
                Instantiate(Init.Singleton.effBase.Pickup,Carried.transform.position,Quaternion.identity);
                Destroy(other.GetComponent<Rigidbody>());
                other.tag = "active";
                Debug.Log("Attached to " + other.GetComponent<AnswersContainer>().Answer);
                Carried.transform.localEulerAngles = new Vector3 (-90, 0, -90);
                time = 0;
                //Vector3 tempLocal = other.transform.localPosition;
                while(Vector3.Distance(other.transform.localPosition ,Pickedup) >= 0.01f){
                    other.transform.localPosition = Vector3.MoveTowards(other.transform.localPosition, Pickedup, 0.02f);
                    yield return new WaitForSeconds(0.005f);
                    if(other == null)
                    break;
                }
                HandsUp = true;
                yield return null;
            }
        yield return null;
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1") || ReleasePressed.GetAxis(SteamVR_Input_Sources.Any) > 0.9f)
        StartCoroutine(Pickup(Target));
        //if(Input.GetButtonDown)
        if(ReleasePressed.GetAxis(SteamVR_Input_Sources.Any) > 0.9f || Input.GetButtonDown("Cancel"))
            if(time >= releaseCD)
                StartCoroutine(Release());
        if(time < releaseCD)
        time += Time.deltaTime;
    }

    private IEnumerator Release(){
        if(Carried != null){
            if(!Carried.GetComponent<AnswersContainer>().Correct)
            {
            Instantiate(Init.Singleton.effBase.FailPar,Carried.transform.position,Quaternion.identity);
            Destroy(Carried);
            TimerBar.tmrt -= 5;
            HandsUp = false;
            time = 0;
            }
            else
            Debug.Log("Burg");
            yield return null;
        }
        yield return null;
    }
}