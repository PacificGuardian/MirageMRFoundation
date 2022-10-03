using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ending : MonoBehaviour {
    public GameObject Win;
    public GameObject Lose;
    public AudioClip Winn;
    public AudioClip LLose;
    private Starting storu;
    public Light night;
    private void Awake() {
        GameEventContainer.TimeEnd += BadEnd;
        GameEventContainer.end += End;
        storu = GameObject.Find("GameController").GetComponent<Starting>();
    }
    private void End() {
        for(int i = 0; i < storu.DisabledOnCutscenes.Length; i++){
        storu.DisabledOnCutscenes[i].SetActive(false);
       }
        Starting.MainCamera.SetActive(false);
        GameObject Particle = Instantiate(Win, Init.Singleton.Rabbit.transform);
        Particle.transform.position = Particle.transform.position + new Vector3(0, 2, 0);
        AudioSource asss = Particle.AddComponent<AudioSource>();
        asss.clip = Winn;
        asss.Play();
        GameObject hereCam = Starting.tempCam();
        Destroy(Init.Singleton.Rabbit.GetComponent<AudioSource>());
        Material tempMat = new Material(Shader.Find("Standard"));
        tempMat.color = Color.white;
        GameObject.Find("Sphere_Inv").GetComponent<Renderer>().material = tempMat;
    }

    private void BadEnd(){
        for(int i = 0; i < storu.DisabledOnCutscenes.Length; i++){
        storu.DisabledOnCutscenes[i].SetActive(false);
       }
        Starting.MainCamera.SetActive(false);
        GameObject Particle = Instantiate(Lose, Init.Singleton.Rabbit.transform);
        AudioSource asss = Particle.AddComponent<AudioSource>();
        asss.clip = LLose;
        asss.Play();
        GameObject hereCam = Starting.tempCam();
        StartCoroutine(Blink());
        Starting.SerCon.SetActive(false);
        Material tempMat = new Material(Shader.Find("Standard"));
        tempMat.color = new Color(0.5f, 0.3f, 0.3f, 1);
        GameObject.Find("Sphere_Inv").GetComponent<Renderer>().material = tempMat;
        Destroy(Init.Singleton.Rabbit.GetComponent<AudioSource>());
    }

    IEnumerator Blink(bool Blink = true){
        while(Blink){
        night.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        night.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        }
    }
}