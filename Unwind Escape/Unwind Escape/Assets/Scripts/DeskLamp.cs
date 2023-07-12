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
    public GameObject PaintingBack;
    AudioSource LampAudioController;
    public AudioClip[] Sound_Lamp;
    int SelectedSoundLamp;
    bool SoundActive;

    // Start is called before the first frame update
    void Start()
    {
        LampAudioController = GetComponent<AudioSource>();
        SelectedSoundLamp = 0;
        PaintingBack.SetActive( false);        
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
        
        if (SoundActive)
        {
            LampSound();
        }
        SoundActive = true;
        if (indexNumber<6)
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
        if(RotationAngleY[indexNumber]==343)
        {
            PaintingBack.SetActive(true);
           
        }
        else if (RotationAngleY[indexNumber] != 343)
        {
            PaintingBack.SetActive(false);
            
        }


    }

    public void LampSound()
    {
        if (SelectedSoundLamp < Sound_Lamp.Length - 1)
        {
            SelectedSoundLamp++;
        }
        else if (SelectedSoundLamp == Sound_Lamp.Length - 1)
        {
            SelectedSoundLamp = 0;
        }
        LampAudioController.PlayOneShot(Sound_Lamp[SelectedSoundLamp]);
    }
}
