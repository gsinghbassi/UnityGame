using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClicks : MonoBehaviour
{
    public CameraController CameraConnector;
    // Start is called before the first frame update
    void Start()
    {
        CameraConnector = GameObject.Find("Main Camera").GetComponent<CameraController>();
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
