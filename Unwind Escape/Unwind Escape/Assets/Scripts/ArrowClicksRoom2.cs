using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClicksRoom2 : MonoBehaviour
{
    public CameraControllerRoom2 CameraConnector;
    // Start is called before the first frame update
    void Start()
    {
        CameraConnector = GameObject.Find("Main Camera").GetComponent<CameraControllerRoom2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RightClick()
    {
        CameraConnector.CameraControls("right");
    }
    public void LeftClick()
    {
        CameraConnector.CameraControls("left");
    }
}
