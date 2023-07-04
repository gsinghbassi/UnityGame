using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerRoom3 : MonoBehaviour
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
    Vector3 PosDocument = new Vector3(0f, 0f, 0f);
    Quaternion RotDocument = Quaternion.Euler(0f, 0f, 0f);
    Vector3 PosChest1 = new Vector3(-2.065588f, 1.62f, -0.6337113f);
    Quaternion RotChest1 = Quaternion.Euler(31.386f, -130.188f, 0f);
    Vector3 PosDart1  = new Vector3(-2.809783f, 1.466677f, -1.110246f);
    Quaternion RotDart1 = Quaternion.Euler(67.483f, -114.545f, 0f);
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
       
        
        if (G_Value == "Document") //This is not updated for ROOM3. Update it later on
        {
            ChestArrows.SetActive(false);
            cameraspeed = 5f;
            GetComponent<Camera>().orthographicSize = 0.45f;
            GetComponent<Camera>().nearClipPlane = -3f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosDocument;
            CameraTargetRotation = RotDocument;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "Chest1")
        {
            cameraspeed = 5f;
            GetComponent<Camera>().orthographicSize = 0.45f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosChest1;
            CameraTargetRotation = RotChest1;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }
        if (G_Value == "Dart1")
        {
            cameraspeed = 0.008f;
            GetComponent<Camera>().orthographicSize = 0.45f;
            CameraTarget = G_Object;
            CameraTargetPosition = PosDart1;
            CameraTargetRotation = RotDart1;
            GetComponent<Camera>().cullingMask = PlayerMask;
            BackButton.SetActive(true);
        }


    }


    public void CameraControls(string G_Value)
    {
        if (G_Value == "right") 
        {
            GetComponent<Camera>().nearClipPlane = NearPlane;
            cameraspeed = 0.1f;
            ButlerRoom3.clearinteractionobjects = true;
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
            ButlerRoom3.clearinteractionobjects = true;
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
        yield return new WaitForSeconds(1f);
        ChestArrows.SetActive(true);
    }



}
