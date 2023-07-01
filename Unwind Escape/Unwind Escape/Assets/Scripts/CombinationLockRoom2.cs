using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinationLockRoom2 : MonoBehaviour
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
    bool Checklockyellow;
    bool Checklockpink;
    bool Checklockblue;
    bool Checklockorange;
    public bool CodeMatched;
    public GameObject ChestDrawers;
    public GameObject ChestArrows;
    public float testX=180;
    public float testY =43;
    public int testZ =90;
    public GameObject Shapes;
    Camera G_Camera;
    bool lockactivated;
    public TextMeshProUGUI InformationText;


    // Start is called before the first frame update
    void Start()
    {
        lockactivated = false;
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>(); 
        CodeMatched = false;
        lockyellow = transform.Find("Lock-ONE").gameObject;
        lockpink = transform.Find("Lock-TWO").gameObject;
        lockblue = transform.Find("Lock-THREE").gameObject;
        lockorange = transform.Find("Lock-FOUR").gameObject;
        incrementangle = 36;
        currentanglevalueyellow = testZ;
        currentanglevaluepink = testZ;
        currentanglevalueblue = testZ;
        currentanglevalueorange = testZ;
        AngleYellow = Quaternion.Euler(testX, testY, currentanglevalueyellow);
        AnglePink = Quaternion.Euler(testX, testY, currentanglevaluepink);
        AngleBlue = Quaternion.Euler(testX, testY, currentanglevalueblue);
        AngleOrange = Quaternion.Euler(testX, testY, currentanglevalueorange);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        AngleYellow = Quaternion.Euler(testX, testY, currentanglevalueyellow);
        AnglePink = Quaternion.Euler(testX, testY, currentanglevaluepink);
        AngleBlue = Quaternion.Euler(testX, testY, currentanglevalueblue);
        AngleOrange = Quaternion.Euler(testX, testY, currentanglevalueorange);
        lockyellow.transform.rotation= Quaternion.Slerp(lockyellow.transform.rotation, AngleYellow, Time.deltaTime * turningspeed);
       lockpink.transform.rotation= Quaternion.Slerp(lockpink.transform.rotation, AnglePink, Time.deltaTime * turningspeed);
       lockblue.transform.rotation= Quaternion.Slerp(lockblue.transform.rotation, AngleBlue, Time.deltaTime * turningspeed);
       lockorange.transform.rotation= Quaternion.Slerp(lockorange.transform.rotation, AngleOrange, Time.deltaTime * turningspeed);

        Checklockyellow = transform.Find("CheckONE").gameObject.GetComponent<CombinationLockCheck>().CodeTrue;
        Checklockpink = transform.Find("CheckTWO").gameObject.GetComponent<CombinationLockCheck>().CodeTrue;
        Checklockblue = transform.Find("CheckTHREE").gameObject.GetComponent<CombinationLockCheck>().CodeTrue;
        Checklockorange = transform.Find("CheckFOUR").gameObject.GetComponent<CombinationLockCheck>().CodeTrue;

        if (Checklockyellow&&Checklockpink&&Checklockblue&&Checklockorange&&!lockactivated)
        {
            CodeMatched = true;            
            ChestDrawers.GetComponent<ChestOfDrawers>().DrawersOpen=true;
            ChestDrawers.name = "ChestDrawersOpened";
            ChestArrows.SetActive(false);
            Shapes.SetActive(false);
            lockblue.SetActive(false);
            lockorange.SetActive(false);
            lockpink.SetActive(false);
            lockyellow.SetActive(false);
            InformationText.text = "Press E to Take the Document";
            G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("Document", lockyellow.transform);
            lockactivated = true;
            
        }
        if(!Checklockyellow || !Checklockpink ||!Checklockblue || !Checklockorange)
        {
            CodeMatched = false; 
        }
        
    }

    public void RotateLockYellow(int G_Value)        
    {
        
        if(G_Value==-1)
        {
            currentanglevalueyellow -= incrementangle;
            AngleYellow = Quaternion.Euler(testX, -90 , currentanglevalueyellow);
        }
        if (G_Value == 1)
        {
            currentanglevalueyellow += incrementangle;
            AngleYellow = Quaternion.Euler(testX, -90, currentanglevalueyellow);
        }

    }
    public void RotateLockPink(int G_Value)
    {

        if (G_Value == -1)
        {
            currentanglevaluepink -= incrementangle;
            AnglePink = Quaternion.Euler(testX, -90, currentanglevaluepink);
        }
        if (G_Value == 1)
        {
            currentanglevaluepink += incrementangle;
            AnglePink = Quaternion.Euler(testX, -90, currentanglevaluepink);
        }

    }
    public void RotateLockBlue(int G_Value)
    {

        if (G_Value == -1)
        {
            currentanglevalueblue -= incrementangle;
            AngleBlue = Quaternion.Euler(testX, -90, currentanglevalueblue);
        }
        if (G_Value == 1)
        {
            currentanglevalueblue += incrementangle;
            AngleBlue = Quaternion.Euler(testX, -90, currentanglevalueblue);
        }

    }
    public void RotateLockOrange(int G_Value)
    {

        if (G_Value == -1)
        {
            currentanglevalueorange -= incrementangle;
            AngleOrange = Quaternion.Euler(testX, -90, currentanglevalueorange);
        }
        if (G_Value == 1)
        {
            currentanglevalueorange += incrementangle;
            AngleOrange = Quaternion.Euler(testX, -90, currentanglevalueorange);
        }

    }

   
}
