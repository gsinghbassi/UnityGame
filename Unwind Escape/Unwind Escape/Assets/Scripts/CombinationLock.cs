using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLock : MonoBehaviour
{
    GameObject lockyellow;
    GameObject lockpink;
    GameObject lockblue;
    GameObject lockorange;
    Quaternion nextNumberAngle;
    float turningspeed;
    // Start is called before the first frame update
    void Start()
    {
        lockyellow = transform.Find("Lock-Yellow").gameObject;
        lockpink = transform.Find("Lock-Pink").gameObject;
        lockblue = transform.Find("Lock-Blue").gameObject;
        lockorange = transform.Find("Lock-Orange").gameObject;
        turningspeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        nextNumberAngle = Quaternion.Euler(36f, 0, 0);
        if (Input.GetKeyDown("c"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, nextNumberAngle, Time.deltaTime * turningspeed);
        }

    }
}
