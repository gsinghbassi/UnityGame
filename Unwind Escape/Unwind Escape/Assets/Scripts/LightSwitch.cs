using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] Lights;
    bool LightsOn;
    Animator LightsAnimator;
    public GameObject HiddenObj1;
    public GameObject HiddenObj2;
    // Start is called before the first frame update
    void Start()
    {
        LightsOn = true;
        LightSwitchControls(1);
        LightsAnimator = GetComponent<Animator>();
        LightsAnimator.SetBool("On", true);
        HiddenObj1.SetActive(false);
        HiddenObj2.SetActive(false);
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
            LightSwitchControls(0);
            HiddenObj1.SetActive(true);
            HiddenObj2.SetActive(true);
        }
        else if(!LightsOn)
        {
            LightsOn = true;
            LightsAnimator.SetBool("On", true);
            LightSwitchControls(1);
            HiddenObj1.SetActive(false);
            HiddenObj2.SetActive(false);
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
