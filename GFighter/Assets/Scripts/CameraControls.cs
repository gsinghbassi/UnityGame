using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject PlayerCharacter;
    public  GameObject CPUCharacter;
    float PlayerpositionZ;
    float CPUpositionZ;
    public float CameraCenterPosition;
    public float CameraCenterPositionPrevious;
    public float CameraCenterPositionDifference;
    float CameraXmin;
    float CameraXmax;
    public float CameraX;

    // Start is called before the first frame update
    void Start()
    {
        CameraXmin = -3.5f;
        CameraXmax = CameraXmin - 2f;
        CameraX = CameraXmin;
        CameraCenterPosition = (PlayerpositionZ + CPUpositionZ) / 2;
        CameraCenterPositionPrevious = CameraCenterPosition;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerpositionZ = PlayerCharacter.transform.position.z;
        CPUpositionZ = CPUCharacter.transform.position.z;
        CameraCenterPosition=(PlayerpositionZ+CPUpositionZ)/ 2;
        if (CameraCenterPositionPrevious != CameraCenterPosition)
        {
            CameraCenterPositionDifference = CameraCenterPosition - CameraCenterPositionPrevious;
            if (CameraX >= CameraXmax)
            {
                CameraX = CameraX-CameraCenterPositionDifference;
            }
            /* This needs work
            else if (CameraX < CameraXmin)
            {
                CameraX = CameraX + CameraCenterPositionDifference;
            }*/
            CameraCenterPositionPrevious = CameraCenterPosition;
        }
        transform.position = new Vector3(CameraX, transform.position.y, CameraCenterPosition);
    }
}
