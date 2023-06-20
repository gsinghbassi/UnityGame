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
    public bool InventoryDocument;    
    public bool InventoryKey;    
    Camera G_Camera;
    public static bool clearinteractionobjects;    
    public GameObject InventoryBGImage;
    public GameObject DocumentImage;
    public GameObject KeyImage;




    // Start is called before the first frame update
    void Start()
    {
       
        InventoryDocument = false;
        ButlerAnimator = GetComponent<Animator>();
        G_Butler = GetComponent<NavMeshAgent>();
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        InventoryBGImage.SetActive(false);
        DocumentImage.SetActive(false);
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
                if (InventoryDocument && !InteractionObject.GetComponent<MainDoor>().DoorOpen)
                {
                    InteractionObject.GetComponent<MainDoor>().DoorOpen=true;
                    InventoryBGImage.SetActive(false);
                    DocumentImage.SetActive(false);                    
                    InformationTextController("Press E to Leave the Room.");
                    
                    
                }
                if (!InventoryDocument)
                {
                    InformationTextController("I need to find the Document first.");
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

            if (InteractionObject.name == "Document1")
            {
                InventoryDocument = true;
                InformationText.text = "";
                Destroy(InteractionObject);
                G_Camera.GetComponent<CameraControllerRoom2>().Back();
                InventoryBGImage.SetActive(true);
                DocumentImage.SetActive(true);
                
            }
            if (InteractionObject.name == "PaintingClock")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("PaintingClock", InteractionObject.transform);
                InformationText.text = "";
            }
            
            if (InteractionObject.name == "Clock")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("Clock", InteractionObject.transform);
                InformationText.text = "";
            }
            if (InteractionObject.name == "Key-Cupboard")
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
        if (other.name == "PaintingClock")
        {
            InformationTextController("Press E to Check the Painting");

        }
        if (other.name == "Clock")
        {
            InformationTextController("Press E to Check the Clock");
        }
        if (other.name == "Key-Cupboard")
        {
            InformationTextController("Press E to take the Key");
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
        if (other.name == "Document1")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "PaintingClock")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Clock")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Key-Cupboard")
        {
            InteractionObject = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain" || other.name == "Document1" || other.name == "PaintingClock" || other.name == "Clock" || other.name == "Key-Cupboard")
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }
    
        
    
}

