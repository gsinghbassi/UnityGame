using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLamp : MonoBehaviour
{

    public Quaternion RotationAngle;
    public float turningspeed;
    public Light SpotLightController;
    public float[] RotationAngleY;
    public float[] SpotRange;
    public float[] SpotAngle;
    public float[] SpotIntensity;
    int indexNumber;

    // Start is called before the first frame update
    void Start()
    {
        SpotLightController = transform.Find("SpotLight").GetComponent<Light>();
        indexNumber = 1;
        UpdateLight();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, RotationAngle, turningspeed*Time.deltaTime);
    }
    public void UpdateLight()
    {
        if(indexNumber<6)
        {
            indexNumber++;
        }
        else if (indexNumber>=6)
        {
            indexNumber = 0;
        }
        RotationAngle = Quaternion.Euler(0, RotationAngleY[indexNumber], 0); 
        SpotLightController.range= SpotRange[indexNumber];
        SpotLightController.spotAngle=SpotAngle[indexNumber];
        SpotLightController.intensity=SpotIntensity[indexNumber];


    }
}
