using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    new Name name;
    float time = 0;
    public int TimeForAltar;
    bool timecount = false;
    ViveControl viveControl;

    // Start is called before the first frame update
    void Start()
    {
        viveControl = GetComponent<ViveControl>();
        name = GetComponent<Name>();


    }

    // Update is called once per frame
    void Update()
    {

        Vector3 RobotPosition = new Vector3(name.RobotTran.position.x-8, name.RobotTran.position.y +2, name.RobotTran.position.z  );
        Vector3 AltarPosition = new Vector3(name.AltarTran.position.x, name.AltarTran.position.y-0.04f , name.AltarTran.position.z+0.08f);
        //print(name.movetoaltar);

        if(timecount == true)
        {
            time += Time.deltaTime;
        }
        if (viveControl && viveControl.HandState == false && name.lasthandstate == true)
        {
            name.BookObj.transform.SetParent(name.AltarTran);
            name.lasthandstate = false;
            name.BookObj.tag = "unactive";
            //print(name.lasthandstate);
            print("put down");
        }

        if (Input.GetKeyDown(KeyCode.S) && name.lasthandstate == true)
        {
            name.BookObj.transform.SetParent(name.AltarTran);
            name.lasthandstate = false;
            //print("disable parent");
            name.BookObj.tag = "unactive";
        }
        
        if(name.movetorobot == true && name.BookObj.CompareTag("active"))
        {                        
            transform.position = Vector3.MoveTowards(transform.position, RobotPosition, 5 * Time.deltaTime);
            if (transform.position == RobotPosition)
            {
                name.movetorobot = false;
            }
        }

        if (name.movetoaltar == true && name.BookObj.CompareTag("unactive"))
        {
            print("movetoaltar");
            transform.position = Vector3.MoveTowards(transform.position, AltarPosition, 5 * Time.deltaTime);
            if (transform.position == AltarPosition)
            {
                name.movetoaltar = false;
            }
        }
    }

        
    public void OnTriggerEnter()
    {        
        if (name.RobotObj.CompareTag("active"))
        {
            //print("name.RobotTran");
            name.BookObj.tag = "active";
            name.BookObj.transform.SetParent(name.RobotTran);
            name.movetorobot = true;
            name.lasthandstate = true;
        }

        if (name.AltarObj.CompareTag("active"))
        {
            timecount = true;
            name.BookObj.transform.SetParent(name.AltarTran);
            name.movetoaltar = true;
            name.lasthandstate = true;
            //print("detect");
            /*
            if (time >= TimeForAltar)
            {
                name.BookObj.transform.SetParent(name.RobotTran);
                name.movetoaltar = false;
                name.movetorobot = true;
                name.lasthandstate = true;
                //print("overtime");
                time = 0;
                timecount = false;
            }
            */
        }
    }
}
