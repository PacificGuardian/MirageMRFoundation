using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_2 : MonoBehaviour
{
    [SerializeField] GameObject robot;
    [SerializeField] float robotspeed = 1;
    [SerializeField] float book_move_speed = 1;
    [SerializeField] float book_rotate_speed = 1;
    [SerializeField] float xpos = 0;
    [SerializeField] float ypos = 0;
    [SerializeField] float zpos = 0;
    GameObject aa;
    bool move_to_robot = false;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {            
            robot.transform.Translate(Vector3.down * robotspeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {            
            robot.transform.Translate(Vector3.up * robotspeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {            
            robot.transform.Translate(Vector3.left * robotspeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {            
            robot.transform.Translate(Vector3.right * robotspeed * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            this.GetComponent<Collider>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.GetComponent<Collider>().enabled = false;
        }
        if(move_to_robot == true)
        {
            Vector3 robot_pos = new Vector3(this.transform.position.x+xpos, this.transform.position.y + ypos, this.transform.position.z + zpos);            
            aa.transform.rotation = Quaternion.Lerp(aa.transform.rotation,robot.transform.rotation,book_rotate_speed);
            aa.transform.position = Vector3.MoveTowards(aa.transform.position, robot_pos, book_move_speed * Time.deltaTime);
            if (aa.transform.position == robot_pos)
            {
                move_to_robot = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("touch");  
        aa = other.gameObject;
        move_to_robot = true;
        other.transform.SetParent(this.transform);
        this.transform.GetComponent<Collider>().enabled = false;
    }
}
