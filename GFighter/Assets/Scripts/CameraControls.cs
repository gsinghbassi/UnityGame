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

    //Shake Effect
    public static bool CameraShakeActivate;
    Vector3 CurrentCameraPosition;
    float NormalCameraPositionX;
    float NormalCameraPositionY;
    float NormalCameraPositionZ;
    bool CameraShake;
    float CameraShakeDuration;
    Vector3 shakeposition;
    float shakefrequency = 20;
    public float maxshake;
    public float finishshake;


    

    // Start is called before the first frame update
    void Start()
    {
        CameraShakeActivate = false;
        CameraShakeDuration = 5f;
        NormalCameraPositionY=transform.position.y;
        CameraXmin = -3.5f;
        CameraXmax = CameraXmin - 2.5f;
        CameraX = CameraXmin;
        CameraCenterPosition = (PlayerpositionZ + CPUpositionZ) / 2;
        CameraCenterPositionPrevious = CameraCenterPosition;
        StartCoroutine(FirstCheckDelay());
       
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
        transform.position = new Vector3(CameraX, NormalCameraPositionY, CameraCenterPosition);

        if(Input.GetKeyDown("m")|| CameraShakeActivate)
        {
            CurrentCameraPosition = transform.position;
            CameraShake = true;
            finishshake = 2;
        }
        
        if (CameraShake)
        {
            CameraShakeActivate = false;
            CamerShakeEfffect(CameraShakeDuration);
        }

    }


    void CamerShakeEfffect(float G_Duration)
    {

        
        shakeposition = new Vector3(Mathf.PerlinNoise(Random.value, Time.time * shakefrequency) * 2 - 1, Mathf.PerlinNoise(Random.value+1, Time.time * shakefrequency) * 2 - 1, Mathf.PerlinNoise(Random.value+2, Time.time * shakefrequency) * 2 - 1) * maxshake*finishshake;
        transform.localPosition = CurrentCameraPosition+shakeposition;
        finishshake = Mathf.Clamp01(finishshake-CameraShakeDuration * Time.deltaTime);
        StartCoroutine(ResetCamera());

    }

    IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(CameraShakeDuration);
        CameraShake = false;

    }

        IEnumerator FirstCheckDelay()
    {
        yield return new WaitForSeconds(1f);
        DistanceBetweenPlayerandCPU = Vector3.Distance(PlayerCharacter.transform.position, CPUCharacter.transform.position);
        DistanceBetweenPlayerandCPUPrevious = DistanceBetweenPlayerandCPU;
    }
}
