using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSimple : MonoBehaviour
{
    public bool Open;
    public Animator ChestController;
    public GameObject Dart2;
    public ButlerRoom3 ButlerObject;

    // Start is called before the first frame update
    void Start()
    {
        ButlerObject = GameObject.Find("ButlerRoom3").GetComponent<ButlerRoom3>();
        Open = false;
        ChestController = GetComponent<Animator>();
        Dart2.name = "Dart2NotReady";
    }

    // Update is called once per frame
    void Update()
    {
        if (Open == true)
        {
            ChestController.SetBool("COpen", true);
        }
    }
    public void Opened()
    {
        Dart2.name = "Dart2";
        ButlerObject.InformationText.text = "Press E to take the Dart";
    }

}
