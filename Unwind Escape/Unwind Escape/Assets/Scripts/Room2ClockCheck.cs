using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2ClockCheck : MonoBehaviour
{
    Animator ClockController;
    public static bool HourTrue;
    public static bool MinsTrue;
    bool dooropened;
    public GameObject NeedleHour;
    public GameObject NeedleMins;
    public GameObject Key;
    
    // Start is called before the first frame update
    void Start()
    {
        Key.SetActive(false);
        ClockController = GetComponent<Animator>();
        dooropened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HourTrue && MinsTrue)
        {
            if (Key!=null)
            { Key.SetActive(true); }
            ClockController.SetBool("ClockDoor", true);
            NeedleHour.GetComponent<BoxCollider>().enabled = false;
            NeedleMins.GetComponent<BoxCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            dooropened = true;
        }
        if (dooropened == true)
        {
            ClockController.SetBool("ClockDoor", true);
        }
    }
}
