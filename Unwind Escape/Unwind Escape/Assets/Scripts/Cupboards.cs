using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboards : MonoBehaviour
{
    public bool DoorOpen;
    Animator CupboardController;
    // Start is called before the first frame update
    void Start()
    {
        DoorOpen = false;
        CupboardController=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorOpen == false)
        {
            CupboardController.SetBool("COpen", false);
        }
        else if (DoorOpen == true)
        {
            CupboardController.SetBool("COpen", true);
        }
    }
}
