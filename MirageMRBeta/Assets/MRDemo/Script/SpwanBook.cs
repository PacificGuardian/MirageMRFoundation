using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanBook : MonoBehaviour
{
    [SerializeField] List<int> powerelvel = new List<int>(new int[] {});
    [SerializeField] List<int> anglelevel = new List<int>(new int[] {});
    [SerializeField] GameObject parent;
    [SerializeField] int fire_point_angle = 0;
    [SerializeField] GameObject book;
    [SerializeField] Transform fire_point;
    [SerializeField] float delay = 0.2f;
    [SerializeField] int bookcount = 11;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= delay && bookcount!=0)
        {
            int power = powerelvel[Random.Range(0, powerelvel.Count)];
            int angle = anglelevel[Random.Range(0, anglelevel.Count)];
            fire_point.rotation = Quaternion.Euler(fire_point_angle, angle, 0);
            GameObject spwan = Instantiate(book, fire_point.position, fire_point.rotation);
            spwan.transform.SetParent(parent.transform);
            spwan.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse);
            //print("power: " + power + "angle: " + angle);
            anglelevel.Remove(angle);
            powerelvel.Remove(power);
            time = 0;
            bookcount--;
        }
    }
}
