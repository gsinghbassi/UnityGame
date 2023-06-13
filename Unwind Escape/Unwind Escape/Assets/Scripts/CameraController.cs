using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 PrevCameraPosition;
    public Quaternion PrevCameraRotation;
    Vector3 Pos1 = new Vector3(-5f,9f,5f);
    Quaternion Rot1 = Quaternion.Euler(45f,135f,0f);
    Vector3 Pos2 = new Vector3(5f,8.5f,5f);
    Quaternion Rot2 = Quaternion.Euler(45f,225f,0f);
    Vector3 Pos3 = new Vector3(10f, 16f, -10f);
    Quaternion Rot3 = Quaternion.Euler(45f, 315f, 0f);
    Vector3 Pos4 = new Vector3(-10f, 16f, -10f);
    Quaternion Rot4 = Quaternion.Euler(45f, 405f, 0f);
    Vector3 PosCodeYellow = new Vector3(-2.078956f, 1.034713f, -2.056693f);
    Quaternion RotCodeYellow = Quaternion.Euler(86.39f, 125.822f, 0f);
    Vector3 PosCodeOrange = new Vector3(0.1060199f, 1.854301f, -3.629745f);    
    Quaternion RotCodeOrange = Quaternion.Euler(85.875f, 181.172f, 0f);
    Vector3 PosCodeRed = new Vector3(0.05355277f, 1.806422f, 2.460785f);
    Quaternion RotCodeRed = Quaternion.Euler(2.922f, -0.136f, 0f);
    Vector3 PosChest = new Vector3(1.69f, 1.82f, 2.730999f);
    Quaternion RotChest = Quaternion.Euler(0.688f, -0.376f, 0f);
    Vector3 PosKeyInsert = new Vector3(3.718723f, 1.420202f, 1.439942f);
    Quaternion RotKeyInsert = Quaternion.Euler(33.038f, 44.862f, 0f);
    Vector3 PosDoor = new Vector3(3.718723f, 1.420202f, 1.439942f);
    Quaternion RotDoor = Quaternion.Euler(33.038f, 44.862f, 0f);
    public GameObject Set1;
    public GameObject Set2;
    public GameObject Set3;
    public GameObject Set4;
    int updatecamera;
    public float cameraspeed;
    Vector3 CameraTargetPosition;
    Quaternion CameraTargetRotation;
    Transform CameraTargetForRoomAxo;
    Transform CameraTargetSet1;
    Transform CameraTargetSet2;
    Transform CameraTargetSet3;
    Transform CameraTargetSet4;
    Transform CameraTarget;
    public LayerMask PlayerMask;
    public LayerMask EverythingMask;
    public GameObject ChestArrows;
    public GameObject BackButton;
    
    


    // Start is called before the first frame update
    void Start()
    {
        updatecamera = 1;
        transform.position = Pos1;
        transform.rotation = Rot1;
        CameraTargetPosition = Pos1;
        CameraTargetRotation = Rot1;
        transform.rotation = Rot1;
        Set1.SetActive(true);
        Set2.SetActive(true);
        Set3.SetActive(false);
        Set4.SetActive(false);
        CameraTargetForRoomAxo = GameObject.Find("CameraTarget-Center").transform;
        CameraTargetSet1 = GameObject.Find("CameraTarget-Set1").transform;
        CameraTargetSet2 = GameObject.Find("CameraTarget-Set2").transform;
        CameraTargetSet3 = GameObject.Find("CameraTarget-Set3").transform;
        CameraTargetSet4 = GameObject.Find("CameraTarget-Set4").transform;
        CameraTarget = CameraTargetSet1;
        ChestArrows.SetActive(false);
        BackButton.SetActive(false);
        PrevCameraPosition = CameraTargetPosition;
        PrevCameraRotation = CameraTargetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != CameraTargetPosition) 
        { 
            transform.LookAt(CameraTarget); 
        } 
        transform.position=Vector3.MoveTowards(transform.position,CameraTargetPosition,cameraspeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, CameraTargetRotation, cameraspeed* 5f);
        

        
    }

    
    public void CameraZoomObject(string G_Value,Transform G_Object)
    {
       
        if (G_Value=="CodeYellowOutside")
        {
            GetComponent<Camera>().orthographicSize = 0.6f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosCodeYellow;
            CameraTargetRotation = RotCodeYellow;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "CodeOrange")
        {
            GetComponent<Camera>().orthographicSize = 0.21f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosCodeOrange;
            CameraTargetRotation = RotCodeOrange;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "CodeRed")
        {
            GetComponent<Camera>().orthographicSize = 0.8f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosCodeRed;
            CameraTargetRotation = RotCodeRed;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "Chest")
        {
            GetComponent<Camera>().orthographicSize = 0.2f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosChest;
            CameraTargetRotation = RotChest;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
            StartCoroutine(ArrowsDelay());
        }
        if (G_Value == "KeyInsert")
        {
            GetComponent<Camera>().orthographicSize = 0.2f;     
            CameraTarget = G_Object;
            CameraTargetPosition = PosKeyInsert;
            CameraTargetRotation = RotKeyInsert;
            GetComponent<Camera>().cullingMask = PlayerMask;        
            
        }
        if (G_Value == "DoorZoom")
        {
            GetComponent<Camera>().orthographicSize = 2f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosDoor;
            CameraTargetRotation = RotDoor;
            GetComponent<Camera>().cullingMask = EverythingMask;            
            Set1.SetActive(true);
            Set2.SetActive(false);
            Set3.SetActive(false);
            Set4.SetActive(true);
            StartCoroutine(DoorZoomOut());
        }

        }


    public void CameraControls(string G_Value)
    {
        if (G_Value == "right") 
        {
            Butler.clearinteractionobjects = true;
            CameraTarget = CameraTargetForRoomAxo;
            GetComponent<Camera>().orthographicSize = 5.51f;
            GetComponent<Camera>().cullingMask = EverythingMask;
            BackButton.SetActive(false);
            ChestArrows.SetActive(false);
            if (updatecamera < 4)
            {
                updatecamera++;
            }
            else if (updatecamera == 4)
            {
                updatecamera=1;
            }
        }
        if (G_Value == "left")
        {
            Butler.clearinteractionobjects = true;
            CameraTarget = CameraTargetForRoomAxo;
            GetComponent<Camera>().orthographicSize = 5.51f;
            GetComponent<Camera>().cullingMask = EverythingMask;
            BackButton.SetActive(false);
            ChestArrows.SetActive(false);
            if (updatecamera > 1)
            {
                updatecamera--;
            }
            else if (updatecamera == 1)
            {
                updatecamera = 4;
            }
        }

        if (updatecamera==1)
        {
            CameraTarget = CameraTargetSet1;
            CameraTargetPosition = Pos1;
            CameraTargetRotation = Rot1;            
            Set1.SetActive(true);
            Set2.SetActive(true);
            Set3.SetActive(false);
            Set4.SetActive(false);
            PrevCameraPosition = CameraTargetPosition;
            PrevCameraRotation = CameraTargetRotation;

        }
        if (updatecamera == 2)
        {
            CameraTarget = CameraTargetSet2;
            CameraTargetPosition = Pos2;
            CameraTargetRotation = Rot2;
            Set1.SetActive(false);
            Set2.SetActive(true);
            Set3.SetActive(true);
            Set4.SetActive(false);
            PrevCameraPosition = CameraTargetPosition;
            PrevCameraRotation = CameraTargetRotation;
        }
        if (updatecamera == 3)
        {
            CameraTarget = CameraTargetSet3;
            CameraTargetPosition = Pos3;
            CameraTargetRotation = Rot3;
            Set1.SetActive(false);
            Set2.SetActive(false);
            Set3.SetActive(true);
            Set4.SetActive(true);
            PrevCameraPosition = CameraTargetPosition;
            PrevCameraRotation = CameraTargetRotation;
        }
        if (updatecamera == 4)
        {
            CameraTarget = CameraTargetSet4;
            CameraTargetPosition = Pos4;
            CameraTargetRotation = Rot4;
            Set1.SetActive(true);
            Set2.SetActive(false);
            Set3.SetActive(false);
            Set4.SetActive(true);
            PrevCameraPosition = CameraTargetPosition;
            PrevCameraRotation = CameraTargetRotation;
        }

    }

    public void Back()
    {
        GetComponent<Camera>().orthographicSize = 5.51f;
        GetComponent<Camera>().cullingMask = EverythingMask;
        BackButton.SetActive(false);
        CameraTargetPosition = PrevCameraPosition;
        CameraTargetRotation = PrevCameraRotation;
        CameraTarget = CameraTargetForRoomAxo;
        ChestArrows.SetActive(false);
    }


    IEnumerator ArrowsDelay()
    {
        yield return new WaitForSeconds(1.5f);
        ChestArrows.SetActive(true);
    }
    IEnumerator DoorZoomOut()
    {
        yield return new WaitForSeconds(2.2f);        
        GetComponent<Camera>().orthographicSize = 5.51f;
        CameraTargetPosition = Pos4;
        CameraTargetRotation = Rot4;
        Set1.SetActive(true);
        Set2.SetActive(false);
        Set3.SetActive(false);
        Set4.SetActive(true);
        PrevCameraPosition = CameraTargetPosition;
        PrevCameraRotation = CameraTargetRotation;
    }
}
