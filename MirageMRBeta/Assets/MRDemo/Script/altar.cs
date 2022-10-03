using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altar : MonoBehaviour
{
    [SerializeField] float xpos = 0;
    [SerializeField] float ypos = 0;
    [SerializeField] float zpos = 0;
    [SerializeField] float move_speed = 5;
    bool move_to_altar = false;
    GameObject aa;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 altarpos = new Vector3(this.transform.position.x + xpos, this.transform.position.y + ypos, this.transform.position.z + zpos);

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.GetComponent<Collider>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.GetComponent<Collider>().enabled = true;
        }

        if (move_to_altar == true)
        {
            aa.transform.position = Vector3.MoveTowards(aa.transform.position, altarpos, move_speed * Time.deltaTime);
            if (aa.transform.position == altarpos)
            {
                //print("Get");
                move_to_altar = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(this.transform);
        aa = other.gameObject;
        move_to_altar = true;
        this.GetComponent<Collider>().enabled = false;       
    }
}
