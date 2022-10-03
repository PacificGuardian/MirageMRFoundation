using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{    
    new Name name;
    ViveControl viveControl;
    // Start is called before the first frame update
    void Start()
    {
        viveControl = GetComponent<ViveControl>();
        name = GetComponent<Name>();
        name.RobotCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            name.RobotObj.transform.Translate(Vector3.down * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            name.RobotObj.transform.Translate(Vector3.up * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            name.RobotObj.transform.Translate(Vector3.left * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            name.RobotObj.transform.Translate(Vector3.right * 10 * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.A) && name.lastkeycode != "a")
        {
            name.RobotObj.tag = "active";
            name.lastkeycode = "a";
            name.RobotCollider.enabled = true;
            ////print("collider on");
        }
        if(Input.GetKeyDown(KeyCode.S) && name.lastkeycode != "s")
        {
            name.RobotObj.tag = "unactive";
            name.lastkeycode = "s";
            name.RobotCollider.enabled = false;
            ////print("collider off");
        }
        
        if (viveControl)
        {
            if (viveControl.HandState == true && name.lasthandstate!=true)
            {
                name.RobotObj.tag = "active";
                name.RobotCollider.enabled = true;
                name.lasthandstate = true;
                //print("Collider on");
                //print(viveControl.HandState);
            }
            if (viveControl.HandState == false && name.lasthandstate!=false)
            {
                name.RobotObj.tag = "unactive";
                name.RobotCollider.enabled = false;
                name.lasthandstate = false;
                //print("Collider off");
                //print(viveControl.HandState);
            }
        }
    }
}
