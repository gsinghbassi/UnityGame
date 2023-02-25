using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject PlayerCharacter;
    public GameObject CPUCharacter;
    float PlayerpositionZ;
    float CPUpositionZ;
    float CameraCenterPosition;
    float CameraCenterPositionPrevious;
    float CameraCenterPositionDifference;
    float CameraXmin;
    float CameraXmax;
    float CameraX;

    float DistanceBetweenPlayerandCPU;
    float DistanceBetweenPlayerandCPUPrevious;

    // Start is called before the first frame update
    void Start()
    {
        CameraXmin = -3.5f;
        CameraXmax = CameraXmin - 2.5f;
        CameraX = CameraXmin;
        CameraCenterPosition = (PlayerpositionZ + CPUpositionZ) / 2;
        CameraCenterPositionPrevious = CameraCenterPosition;
        DistanceBetweenPlayerandCPU = Vector3.Distance(PlayerCharacter.transform.position, CPUCharacter.transform.position);
        DistanceBetweenPlayerandCPUPrevious = DistanceBetweenPlayerandCPU;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerpositionZ = PlayerCharacter.transform.position.z;
        CPUpositionZ = CPUCharacter.transform.position.z;
        CameraCenterPosition=(PlayerpositionZ+CPUpositionZ)/ 2;
        DistanceBetweenPlayerandCPU = Vector3.Distance(PlayerCharacter.transform.position, CPUCharacter.transform.position);
        

        if (CameraCenterPositionPrevious != CameraCenterPosition)
        {
            CameraCenterPositionDifference = CameraCenterPosition - CameraCenterPositionPrevious;
           
            
            if(CameraX >= CameraXmax &&  DistanceBetweenPlayerandCPU >DistanceBetweenPlayerandCPUPrevious)
            {
                CameraX = CameraX - Mathf.Abs(CameraCenterPositionDifference);
            }
            if(CameraX <= CameraXmin &&  DistanceBetweenPlayerandCPU < DistanceBetweenPlayerandCPUPrevious)
            {
                CameraX = CameraX + Mathf.Abs(CameraCenterPositionDifference);
            }  
            CameraCenterPositionPrevious = CameraCenterPosition;
            DistanceBetweenPlayerandCPUPrevious = DistanceBetweenPlayerandCPU;
        }
        transform.position = new Vector3(CameraX, transform.position.y, CameraCenterPosition);
    }
}
