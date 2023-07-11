using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchRoom2 : MonoBehaviour
{
    public Light[] Lights;
    bool LightsOn;
    Animator LightsAnimator;    
    public Material LampShade;
    public GameObject TVLight;
    public GameObject TVScreen;
    public Material TVScreenOn;
    public Material TVScreenOff;
    public GameObject TVCollider;
    AudioSource Light_AudioController;




    // Start is called before the first frame update
    void Start()
    {
        Light_AudioController = GetComponent<AudioSource>();
        LightsOn = true;
        LightSwitchControls(1);
        LightsAnimator = GetComponent<Animator>();
        LightsAnimator.SetBool("On", true);       
        LampShade.SetColor("_EmissionColor", Color.white);
        TVLight.SetActive(false);        
        TVCollider.name = "TV Collider On";
        TVScreen.GetComponent<MeshRenderer>().material = TVScreenOff;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void LightsSwitch()
    {
        if (LightsOn)
        {
            LightsOn = false;
            LightsAnimator.SetBool("On", false);
            LampShade.SetColor("_EmissionColor", Color.grey);
            LightSwitchControls(0);
            TVLight.SetActive(true);           
            TVCollider.name = "TV Collider Off";
            TVScreen.GetComponent<MeshRenderer>().material = TVScreenOn;
            Light_AudioController.Play();
        }
        else if(!LightsOn)
        {
            LightsOn = true;
            LightsAnimator.SetBool("On", true);
            LampShade.SetColor("_EmissionColor", Color.white);
            LightSwitchControls(1);
            TVLight.SetActive(false);            
            TVCollider.name = "TV Collider On2";
            TVScreen.GetComponent<MeshRenderer>().material = TVScreenOff;
            Light_AudioController.Stop();
        }
    }

    void LightSwitchControls(int G_Input)
    { 
        if(G_Input==1)
        {

            for (int i=0;i<Lights.Length;i++)
            { 
                Lights[i].enabled = true; 
            }
        }
        else if (G_Input==0)
        {
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i].enabled = false;
            }
        }
    }
}
