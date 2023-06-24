using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Clock : MonoBehaviour
{
    public Quaternion[] Times;    
    float needlespeed = 50f;
    Quaternion TargetRotation;
    int nextTime;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.name== "NeedleHour") 
        { 
        nextTime = 4;
        TargetRotation = Times[nextTime];
        }
        if (transform.name == "NeedleMins")
        {
            nextTime = 9;
            TargetRotation = Times[nextTime];
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.name == "NeedleHour"  )
        {
            if(nextTime == 10) { 
            Room2ClockCheck.HourTrue = true;
            }
            else if (nextTime != 10)
            {
                Room2ClockCheck.HourTrue = false;
            }
        }
        
        if (transform.name == "NeedleMins")
        {
            if (nextTime == 2)
            {
                Room2ClockCheck.MinsTrue = true;
            }
            else if (nextTime != 2)
            {
                Room2ClockCheck.MinsTrue = false;
            }
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, needlespeed * Time.deltaTime);

    }
    
    void OnMouseDown()
    {
        if (nextTime < Times.Length - 1)
        {
            nextTime++;
            TargetRotation = Times[nextTime];
        }
        else if (nextTime == Times.Length - 1)
        {
            nextTime = 0;
            TargetRotation = Times[nextTime];
        }
    }
}
