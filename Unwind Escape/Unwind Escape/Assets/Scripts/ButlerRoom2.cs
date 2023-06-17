using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButlerRoom2 : MonoBehaviour
{
    NavMeshAgent G_Butler;
    Animator ButlerAnimator;
    public TextMeshProUGUI InformationText;
    public GameObject InteractionObject;
    public bool InventoryKey;    
    Camera G_Camera;
    public static bool clearinteractionobjects;    
    public GameObject InventoryBGImage;
    public GameObject KeyImage;




    // Start is called before the first frame update
    void Start()
    {
       
        InventoryKey = false;
        ButlerAnimator = GetComponent<Animator>();
        G_Butler = GetComponent<NavMeshAgent>();
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        InventoryBGImage.SetActive(false);
        KeyImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = G_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitpoint;
            if(Physics.Raycast(ray, out hitpoint))
            {
                G_Butler.SetDestination(hitpoint.point);
            }
        }

        if(G_Butler.velocity!= Vector3.zero)
        {
            ButlerAnimator.SetBool("Walk", true);
        }
        if (G_Butler.velocity == Vector3.zero)
        {
            ButlerAnimator.SetBool("Walk", false);
        }

        if(clearinteractionobjects)
        {
            clearinteractionobjects = false;
            InformationText.text = "";
            InteractionObject = null;
        }

        if(InteractionObject!=null&&Input.GetKeyDown("e"))
        {
            
            
            if (InteractionObject.name == "DoorMain")
            {
                if (InventoryKey && !InteractionObject.GetComponent<MainDoor>().DoorOpen)
                {
                    InteractionObject.GetComponent<MainDoor>().DoorOpen=true;
                    InventoryBGImage.SetActive(false);
                    KeyImage.SetActive(false);                    
                    InformationTextController("Press E to Leave the Room.");
                    
                    
                }
                if (!InventoryKey)
                {
                    InformationTextController("I need to find the key first.");
                }
                if(InteractionObject.GetComponent<MainDoor>().DoorOpen)
                {
                    InformationTextController("Press E to Leave the Room.");
                    
                }
            }
            if (InteractionObject.name == "LightSwitchVintage")
            {
                InteractionObject.GetComponent<LightSwitchRoom2>().LightsSwitch();
            }

            if (InteractionObject.name == "Key")
            {
                InventoryKey = true;
                InformationText.text = "";
                Destroy(InteractionObject);
                G_Camera.GetComponent<CameraControllerRoom2>().Back();
                InventoryBGImage.SetActive(true);
                KeyImage.SetActive(true);
                
            }

        }

    }

    public void InformationTextController(string G_Text)
    {
        InformationText.text = G_Text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LightSwitchVintage")
        {
            InformationTextController("Press E to Switch Light On or Off");
           
        }
        if (other.name == "DoorMain")
        {
            if (!other.GetComponent<MainDoor>().DoorOpen)
            {
                InformationTextController("Press E to Open the Door");
            }
            else if (other.GetComponent<MainDoor>().DoorOpen)
            {
                InformationTextController("Press E to Leave the Room");
            }
        }        
        if (other.name == "Key")
        {
            InformationTextController("Press E to Take the Key.");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name== "LightSwitchVintage")
        {
            InteractionObject = other.gameObject;           
        }
        if (other.name == "DoorMain")
        {
            InteractionObject = other.gameObject;
        }        
        if (other.name == "Key")
        {
            InteractionObject = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain"|| other.name == "CodeYellow"|| other.name == "CodeYellowOutside" || other.name == "CodeOrange"|| other.name == "WallHint" || other.name == "Painting" || other.name == "CodeRed" || other.name == "Chest" || other.name == "Key")
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }
    
        
    
}

