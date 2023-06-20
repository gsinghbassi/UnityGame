using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Clock : MonoBehaviour
{
    public Quaternion[] Times;
    Quaternion One    = Quaternion.Euler(0f, 180f, 30f);
    Quaternion Two    = Quaternion.Euler(0f, 180f, 60f);
    Quaternion Three  = Quaternion.Euler(0f, 180f, 90f);
    Quaternion Four   = Quaternion.Euler(0f, 180f, 120f);
    Quaternion Five   = Quaternion.Euler(0f, 180f, 150f);
    Quaternion Six    = Quaternion.Euler(0f, 180f, 180f);
    Quaternion Seven  = Quaternion.Euler(0f, 180f, 210f);
    Quaternion Eight  = Quaternion.Euler(0f, 180f, 240f);
    Quaternion Nine   = Quaternion.Euler(0f, 180f, 270f);
    Quaternion Ten    = Quaternion.Euler(0f, 180f, 300f);
    Quaternion Eleven = Quaternion.Euler(0f, 180f, 330f);
    Quaternion Twelve = Quaternion.Euler(0f, 180f, 360f);
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
