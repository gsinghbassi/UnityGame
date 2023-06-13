using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public bool DoorOpen;
    Animator DoorController;
    Camera G_Camera;
    // Start is called before the first frame update
    void Start()
    {
        DoorOpen = false;
        DoorController = GetComponent<Animator>();
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorOpen == true)
        {
            DoorController.SetBool("DoorOpen", true);
        }
    }
    public void DoorZoom()
    {
        G_Camera.GetComponent<CameraController>().CameraZoomObject("DoorZoom",this.transform);
    }
}
