using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FailParticle : MonoBehaviour
{
    IEnumerator Start(){
        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = Init.Singleton.effBase.FailCli;
        aud.Play();
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        Debug.Log("Destroying Fail");
    }
}
