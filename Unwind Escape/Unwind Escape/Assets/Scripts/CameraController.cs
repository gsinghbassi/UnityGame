using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 Pos1 = new Vector3(-5f,9f,5f);
    Quaternion Rot1 = Quaternion.Euler(45f,135f,0f);
    Vector3 Pos2 = new Vector3(5f,8.5f,5f);
    Quaternion Rot2 = Quaternion.Euler(45f,225f,0f);
    Vector3 Pos3 = new Vector3(10f, 16f, -10f);
    Quaternion Rot3 = Quaternion.Euler(45f, 315f, 0f);
    Vector3 Pos4 = new Vector3(-10f, 16f, -10f);
    Quaternion Rot4 = Quaternion.Euler(45f, 405f, 0f);
    public GameObject Set1;
    public GameObject Set2;
    public GameObject Set3;
    public GameObject Set4;
    int updatecamera;
    public float cameraspeed;
    Vector3 CameraTargetPosition;
    Quaternion CameraTargetRotation;
    
    


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
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != CameraTargetPosition) { transform.LookAt(GameObject.Find("CameraTarget-Center").transform); }
        transform.position=Vector3.MoveTowards(transform.position,CameraTargetPosition,cameraspeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, CameraTargetRotation, cameraspeed* 5f);
        

        
    }

    
    


    public void CameraControls(string G_Value)
    {
        if (G_Value == "right") {
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
            CameraTargetPosition = Pos1;
            CameraTargetRotation = Rot1;
            Set1.SetActive(true);
            Set2.SetActive(true);
            Set3.SetActive(false);
            Set4.SetActive(false);

        }
        if (updatecamera == 2)
        {
            
            CameraTargetPosition = Pos2;
            CameraTargetRotation = Rot2;
            Set1.SetActive(false);
            Set2.SetActive(true);
            Set3.SetActive(true);
            Set4.SetActive(false);
        }
        if (updatecamera == 3)
        {
            CameraTargetPosition = Pos3;
            CameraTargetRotation = Rot3;
            Set1.SetActive(false);
            Set2.SetActive(false);
            Set3.SetActive(true);
            Set4.SetActive(true);
        }
        if (updatecamera == 4)
        {
            CameraTargetPosition = Pos4;
            CameraTargetRotation = Rot4;
            Set1.SetActive(true);
            Set2.SetActive(false);
            Set3.SetActive(false);
            Set4.SetActive(true);
        }

    }

}
