using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    //HMD
    public static GameObject MainCamera = null;
    public GameObject[] DisabledOnCutscenes = null;
    [SerializeField]
    private Vector3 TargetPos;
    private GameObject HereCam;
    public GameObject initialFade;
    public GameObject SliderBar;
    public GameObject EntryPoint;
    [SerializeField]
    bool ActivateCamera;
    public static GameObject SerCon;
    private void Awake(){
       GameEventContainer.start += StartingScene;
       MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
       if(MainCamera != null)
       MainCamera.SetActive(false);
       SerCon = GameObject.Find("SerialController");
       if(DisabledOnCutscenes.Length == 0)
             DisabledOnCutscenes = GameObject.FindGameObjectsWithTag("Particle");
       for(int i = 0; i < DisabledOnCutscenes.Length; i++){
            DisabledOnCutscenes[i].SetActive(false);
       }
       SliderBar.SetActive(false);
       SerCon.SetActive(false);
    }
    private void StartingScene(){
        Vector3 target = EntryPoint.transform.position;
        //print(target);
        HereCam = tempCam();
        StartCoroutine(PointWarp(HereCam, target));
        if(ActivateCamera)
        GameObject.Find("Sphere_Inv").GetComponent<WebCam>().StartCamera();
//        Init.Singleton.Rabbit.GetComponent<Animator>().Play("Scene");
    }

    private void ReActivate(){
        for(int i = 0; i < DisabledOnCutscenes.Length; i++){
        DisabledOnCutscenes[i].SetActive(true);
       }
        MainCamera.SetActive(true);
        Destroy(HereCam);
        GameEventContainer.AdvanceStage();
        SliderBar.SetActive(true);
        AudioSource audio;
        if(Init.Singleton.Rabbit.GetComponent<AudioSource>() == null)
        audio = Init.Singleton.Rabbit.AddComponent<AudioSource>();
        else
        audio = Init.Singleton.Rabbit.GetComponent<AudioSource>();
        audio.clip = Init.Singleton.BGM;
        audio.Play();
        audio.volume = 0.2f;
        Debug.Log("Starting BGM");
        SerCon.SetActive(true);
    }

    IEnumerator PointWarp(GameObject Original, Vector3 Target){
        float Magnitude = 0.05f;
        //Original.transform.LookAt(Target);
        while(Vector3.Distance(Original.transform.position, Target) >= 0.001f){
            Original.transform.position = Vector3.MoveTowards(Original.transform.position, Target, Magnitude);
            Magnitude *= 1.01f;
            yield return new WaitForSeconds(0.01f);
        }
        if(Vector3.Distance(Original.transform.position, Target) <= 0.001f){
            if(initialFade == null){
                ReActivate();
                StopAllCoroutines();
                Animations.Wave();
            }
            else{
            Animations.Wave();
            yield return new WaitForSeconds(2.4f);
            StartCoroutine(FadeOut(initialFade, 0.02f));
        }}
        yield return null;
    }

    IEnumerator FadeOut(GameObject Fading, float FadeSpeed){
        float mag = 0.1f;
        Vector3 center = new Vector3(0,0,0);
        while(Vector3.Distance(HereCam.transform.position, center) > 0.001){
            HereCam.transform.position = Vector3.MoveTowards(HereCam.transform.position, center, mag);
            yield return new WaitForSeconds(0.01f);
        }
        //while(Quaternion.Angle(HereCam.transform.rotation,MainCamera.transform.rotation) > 0.1){
        Color colour = Fading.GetComponent<Renderer>().material.color;
        while(colour.a >= 0){
        float fAmount = colour.a - 0.01f;
        colour = new Color(colour.r, colour.g, colour.b, fAmount);
        Fading.GetComponent<Renderer>().material.color = colour;
        yield return new WaitForSeconds(FadeSpeed);
        }
        if(colour.a <= 0){
            Debug.Log("Faded");
        Animations.BookSpawn();
        //yield return new WaitForSeconds(2.4f);
        ReActivate();
        yield return null;
        }
    }
    public static GameObject tempCam(){
        GameObject tempCam = new GameObject("Camera");
        tempCam.AddComponent<Camera>().farClipPlane = 10000;
        tempCam.transform.SetParent(Init.Singleton.CamParent.transform);
        tempCam.transform.localPosition = new Vector3(0, 2, 0);
        tempCam.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        return tempCam;
    }
}
