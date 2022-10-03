using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerBar : MonoBehaviour
{
    [SerializeField] TMP_Text tmrT;
    Slider tmrS;
    float tAcc;
    public static int tmrt = 120;
    public static float gametime = 120;
    private bool Activee = false;
    private void Awake(){
        GameEventContainer.TimeEnd += EndAll;
        GameEventContainer.stageChange += StartTime;
        GameEventContainer.end += End;
    }
    void Start(){
        gametime = Init.Singleton.GameTime;
        tmrt = ((short)gametime);
        tmrS = GetComponent<Slider>();
        tmrS.maxValue = gametime;
        tmrS.value = gametime;
    }
    void Update()
    {
    if(Activee){
        float time = gametime - Time.time;
        tmrS.value = time;
        tAcc += Time.deltaTime;
        if(tAcc >= 1f)
        {
            tAcc -= 1f;
            tmrt --;
            tmrT.text = tmrt.ToString();
        }
    }
    }

    private void StartTime(){
        if(GameEventContainer.QuestionNumber != -1 && !Activee)
            Activee = true;
    }

    private void EndAll(){
        gameObject.SetActive(false);
    }
    private void End(){
        Activee = false;
        gameObject.SetActive(false);
    }
}
