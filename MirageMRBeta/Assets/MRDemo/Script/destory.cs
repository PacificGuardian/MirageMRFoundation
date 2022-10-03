using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destory : MonoBehaviour
{
    [SerializeField] bool destory_option = false;
    [SerializeField] float destory_time = 5;
    [SerializeField] float stoppos = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destory_option == true)
        {
            destory_time -= Time.deltaTime;
        }
        if (destory_time <= 0)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.SetParent(null);
        }
        if(this.transform.position.y <= stoppos)
        {
           this.GetComponent<Rigidbody>().useGravity = false;
           this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
