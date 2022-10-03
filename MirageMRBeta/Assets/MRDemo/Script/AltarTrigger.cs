using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTrigger : MonoBehaviour
{
    [SerializeField] GameObject altar;
    // Start is called before the first frame update
    void Start()
    {
        altar.tag = "unactive";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter()
    {
        altar.tag = "active";
    }

    private void OnTriggerExit()
    {
        altar.tag = "unactive";
    }
}
