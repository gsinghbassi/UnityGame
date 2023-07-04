using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchRoom3 : MonoBehaviour
{
    public Light[] Lights;
    bool LightsOn;
    Animator LightsAnimator;    
    public Material LampShade;
    

    // Start is called before the first frame update
    void Start()
    {
        LightsOn = true;
        LightSwitchControls(1);
        LightsAnimator = GetComponent<Animator>();
        LightsAnimator.SetBool("On", true);       
        LampShade.SetColor("_EmissionColor", Color.white);
        


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
            
        }
        else if(!LightsOn)
        {
            LightsOn = true;
            LightsAnimator.SetBool("On", true);
            LampShade.SetColor("_EmissionColor", Color.white);
            LightSwitchControls(1);
            
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
