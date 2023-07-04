using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButlerRoom3 : MonoBehaviour
{
    NavMeshAgent G_Butler;
    Animator ButlerAnimator;
    public TextMeshProUGUI InformationText;
    public GameObject InteractionObject;
    public bool InventoryDocument; 
    public bool InventoryHammer; 
    Camera G_Camera;
    public static bool clearinteractionobjects;    
    public GameObject InventoryBGImage;
    public GameObject DocumentImage;    
    public GameObject Document;




    // Start is called before the first frame update
    void Start()
    {
       
        InventoryDocument = false;
        InventoryHammer = false;
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
                    SceneManager.LoadScene("Level6_Cutscene");                   
                }
            }
            if (InteractionObject.name == "LightSwitchVintage")
            {
                InteractionObject.GetComponent<LightSwitchRoom3>().LightsSwitch();
            }

            if (InteractionObject.name == "Chest1")
            {
                if (InventoryHammer)
                {
                    InteractionObject.GetComponent<ChestHammer>().ChestController.SetBool("COpen", true);
                    InteractionObject.name = "Chest1Opened";
                    InformationTextController("");
                    G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Chest1", InteractionObject.transform);


                }
                else if(!InventoryHammer)
                {
                    InformationTextController("OH! There is a LOCK. I need to find a way to open the Lock");
                }
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
        if (other.name == "Chest1")
        {
            InformationTextController("Press E to Open the Chest");
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
        if (other.name == "Document2")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Chest1")
        {
            InteractionObject = other.gameObject;
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain" || other.name == "Document2" || other.name== "Chest1") 
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }
    
        
    
}

