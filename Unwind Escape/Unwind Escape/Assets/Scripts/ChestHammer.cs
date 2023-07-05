using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHammer : MonoBehaviour
{
    public bool Open;
    public Animator ChestController;
    Camera G_Camera;
    public GameObject Dart1;
    public ButlerRoom3 ButlerObject;
    // Start is called before the first frame update
    void Start()
    {
        ButlerObject= GameObject.Find("ButlerRoom3").GetComponent<ButlerRoom3>();
        Open = false;
        ChestController = GetComponent<Animator>();
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Dart1.name = "Dart1NotReady";
    }

    // Update is called once per frame
    void Update()
    {
        if (Open == true)
        {
            ChestController.SetBool("StayOpen", true);
        }
    }
    public void Opened()
    {

        Open = true;
        G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Dart1", this.transform);
        Dart1.name = "Dart1";
        ButlerObject.InformationText.text = "Press E to take the Dart";
    }
}
