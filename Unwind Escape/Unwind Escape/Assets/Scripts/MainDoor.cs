using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public bool DoorOpen;
    Animator DoorController;
    
    // Start is called before the first frame update
    void Start()
    {
        DoorOpen = false;
        DoorController = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorOpen == true)
        {
            DoorController.SetBool("DoorOpen", true);
        }
    }
    
}
