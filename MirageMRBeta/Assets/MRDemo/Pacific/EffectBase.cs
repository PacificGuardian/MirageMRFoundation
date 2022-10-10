using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "EffectBase", menuName = "JungleCfg/AudioCfg")]
public class EffectBase : ScriptableObject{
    public GameObject SucessPar;
    public GameObject FailPar;
    public GameObject Pickup;
    public AudioClip SuccessCli;
    public AudioClip FailCli;
    public AudioClip PickAu;
    public AudioClip Lauf;
}