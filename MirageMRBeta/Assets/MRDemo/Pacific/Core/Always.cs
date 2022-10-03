using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Always : MonoBehaviour{
    private void Update(){
        if(RobotAttacher.Robot != null)
        transform.LookAt(RobotAttacher.Robot.transform);
    }
}