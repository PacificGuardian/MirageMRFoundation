using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : MonoBehaviour
{
     public Collider RobotCollider;
     public Collider BookCollider;
     public Collider AltarCollider;

     public GameObject RobotObj;
     public GameObject BookObj;
     public GameObject AltarObj;

     public Transform RobotTran;
     public Transform BookTran;
     public Transform AltarTran;

    public bool  lasthandstate = false;
    public bool  movetorobot = false;
    public bool  movetoaltar = false;

    public string lastkeycode = null;
    // Start is called before the first frame update
    void Start()
    {
        RobotCollider = GetComponent<Collider>();
        BookCollider = GetComponent<Collider>();
        AltarCollider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
