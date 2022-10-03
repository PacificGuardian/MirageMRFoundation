using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Animations : MonoBehaviour {
    
    [SerializeField]
    GameObject Rabbit;

    private static Animator RabAnim;
    /*
    private void Awake(){
        GameEventContainer.start += Wave;
    }    
    */
    private void Start() {
        RabAnim = Rabbit.GetComponent<Animator>();
    }
    public static void Wave(){
        RabAnim.SetTrigger("Wave");
    }
    public static void BookSpawn(){
        RabAnim.SetTrigger("BookInit");
    }
    public static void BookSwap(){
        RabAnim.SetTrigger("Book2Spawn");
    }
    
}