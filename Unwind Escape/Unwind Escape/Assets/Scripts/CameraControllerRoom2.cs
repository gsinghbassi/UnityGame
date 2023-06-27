using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerRoom2 : MonoBehaviour
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
    Vector3 PosPaintingClock = new Vector3(0.1706064f, 2.334346f, -2.132858f);
    Quaternion RotPaintingClock = Quaternion.Euler(2.681f, 180.997f, 0f);
    Vector3 PosClock = new Vector3(0.03792413f, 2.083092f, 4f);
    Quaternion RotClock = Quaternion.Euler(0.791f, 0.55f, 0f);
    Vector3 PosCodeRug = new Vector3(-3.189539f, 0.8158967f, -1.253579f);
    Quaternion RotCodeRug = Quaternion.Euler(88.866f, 271.426f, 0f);
    Vector3 PosCodeTV = new Vector3(9.2f, 1.03f, 4.2f);
    Quaternion RotCodeTV = Quaternion.Euler(0.962f, 46.238f, 0f);
    Vector3 PosDice = new Vector3(-2.033061f, 0.8023469f, 0.1387399f);
    Quaternion RotDice = Quaternion.Euler(11.963f, 273.541f, 0f);
    Vector3 PosCupboardClue = new Vector3(-2.962553f, 1.027805f, -3.135758f);
    Quaternion RotCupboardClue = Quaternion.Euler(-0.241f, 271.134f, 0f);
    Vector3 PosLock2 = new Vector3(-2.908144f, 1.131081f, 3.244418f);
    Quaternion RotLock2 = Quaternion.Euler(-0.171f, -47.132f, 0f);
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
    public GameObject BackButton;
    public float NearPlane;
    public GameObject ChestArrows;



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
        BackButton.SetActive(false);
        ChestArrows.SetActive(false);
        PrevCameraPosition = CameraTargetPosition;
        PrevCameraRotation = CameraTargetRotation;
        NearPlane = -10f;
        GetComponent<Camera>().nearClipPlane = NearPlane;
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
       
        if (G_Value== "PaintingClock")
        {
            GetComponent<Camera>().orthographicSize = 0.9f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosPaintingClock;
            CameraTargetRotation = RotPaintingClock;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "Clock")
        {
            GetComponent<Camera>().orthographicSize = 0.45f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosClock;
            CameraTargetRotation = RotClock;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "RugClue")
        {
            GetComponent<Camera>().orthographicSize = 0.25f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosCodeRug;
            CameraTargetRotation = RotCodeRug;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "TVClue")
        {
            cameraspeed = 5f;
            GetComponent<Camera>().orthographicSize = 0.27f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosCodeTV;
            CameraTargetRotation = RotCodeTV;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "Dice")
        {            
            GetComponent<Camera>().orthographicSize = 0.05f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosDice;
            CameraTargetRotation = RotDice;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "CupboardClue")
        {
            GetComponent<Camera>().orthographicSize = 0.3f;
            GetComponent<Camera>().nearClipPlane = 0f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosCupboardClue;
            CameraTargetRotation = RotCupboardClue;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "ChestLock")
        {
            GetComponent<Camera>().orthographicSize = 0.1f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosLock2;
            CameraTargetRotation = RotLock2;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
            StartCoroutine(ArrowsDelay());
        }

    }


    public void CameraControls(string G_Value)
    {
        if (G_Value == "right") 
        {
            GetComponent<Camera>().nearClipPlane = NearPlane;
            cameraspeed = 0.1f;
            ButlerRoom2.clearinteractionobjects = true;
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
            GetComponent<Camera>().nearClipPlane = NearPlane;
            cameraspeed = 0.1f;
            ButlerRoom2.clearinteractionobjects = true;
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
        GetComponent<Camera>().nearClipPlane = NearPlane;
        GetComponent<Camera>().cullingMask = EverythingMask;
        BackButton.SetActive(false);
        CameraTargetPosition = PrevCameraPosition;
        CameraTargetRotation = PrevCameraRotation;
        CameraTarget = CameraTargetForRoomAxo;
        cameraspeed = 0.1f;
        ChestArrows.SetActive(false);
    }
    IEnumerator ArrowsDelay()
    {
        yield return new WaitForSeconds(1.5f);
        ChestArrows.SetActive(true);
    }



}
