using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLock : MonoBehaviour
{
    GameObject lockyellow;
    GameObject lockpink;
    GameObject lockblue;
    GameObject lockorange;    
    int incrementangle;
    int currentanglevalueyellow;
    int currentanglevaluepink;
    int currentanglevalueblue;
    int currentanglevalueorange;
    float turningspeed=5f;
    Quaternion AngleYellow;
    Quaternion AnglePink;
    Quaternion AngleBlue;
    Quaternion AngleOrange;
    

    // Start is called before the first frame update
    void Start()
    {
        lockyellow = transform.Find("Lock-Yellow").gameObject;
        lockpink = transform.Find("Lock-Pink").gameObject;
        lockblue = transform.Find("Lock-Blue").gameObject;
        lockorange = transform.Find("Lock-Orange").gameObject;
        incrementangle = 36;
        currentanglevalueyellow = -90;
        currentanglevaluepink = -90;
        currentanglevalueblue = -90;
        currentanglevalueorange = -90;
        AngleYellow = Quaternion.Euler(0, -90, currentanglevalueyellow);
        AnglePink = Quaternion.Euler(0, -90, currentanglevaluepink);
        AngleBlue = Quaternion.Euler(0, -90, currentanglevalueblue);
        AngleOrange = Quaternion.Euler(0, -90, currentanglevalueorange);

    }

    // Update is called once per frame
    void Update()
    {
        
       lockyellow.transform.rotation= Quaternion.Slerp(lockyellow.transform.rotation, AngleYellow, Time.deltaTime * turningspeed);
       lockpink.transform.rotation= Quaternion.Slerp(lockpink.transform.rotation, AnglePink, Time.deltaTime * turningspeed);
       lockblue.transform.rotation= Quaternion.Slerp(lockblue.transform.rotation, AngleBlue, Time.deltaTime * turningspeed);
       lockorange.transform.rotation= Quaternion.Slerp(lockorange.transform.rotation, AngleOrange, Time.deltaTime * turningspeed);

    }

    public void RotateLockYellow(int G_Value)        
    {
        
        if(G_Value==-1)
        {
            currentanglevalueyellow -= incrementangle;
            AngleYellow = Quaternion.Euler(0,-90 , currentanglevalueyellow);
        }
        if (G_Value == 1)
        {
            currentanglevalueyellow += incrementangle;
            AngleYellow = Quaternion.Euler(0, -90, currentanglevalueyellow);
        }

    }
    public void RotateLockPink(int G_Value)
    {

        if (G_Value == -1)
        {
            currentanglevaluepink -= incrementangle;
            AnglePink = Quaternion.Euler(0, -90, currentanglevaluepink);
        }
        if (G_Value == 1)
        {
            currentanglevaluepink += incrementangle;
            AnglePink = Quaternion.Euler(0, -90, currentanglevaluepink);
        }

    }
    public void RotateLockBlue(int G_Value)
    {

        if (G_Value == -1)
        {
            currentanglevalueblue -= incrementangle;
            AngleBlue = Quaternion.Euler(0, -90, currentanglevalueblue);
        }
        if (G_Value == 1)
        {
            currentanglevalueblue += incrementangle;
            AngleBlue = Quaternion.Euler(0, -90, currentanglevalueblue);
        }

    }
    public void RotateLockOrange(int G_Value)
    {

        if (G_Value == -1)
        {
            currentanglevalueorange -= incrementangle;
            AngleOrange = Quaternion.Euler(0, -90, currentanglevalueorange);
        }
        if (G_Value == 1)
        {
            currentanglevalueorange += incrementangle;
            AngleOrange = Quaternion.Euler(0, -90, currentanglevalueorange);
        }

    }

}
