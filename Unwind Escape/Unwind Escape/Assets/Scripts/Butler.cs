using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Butler : MonoBehaviour
{
    NavMeshAgent G_Butler;
    Animator ButlerAnimator;
    public TextMeshProUGUI InformationText;
    public GameObject InteractionObject;
    public bool InventoryKey;
   
    Camera G_Camera;
    // Start is called before the first frame update
    void Start()
    {
        InventoryKey = false;
        ButlerAnimator = GetComponent<Animator>();
        G_Butler = GetComponent<NavMeshAgent>();
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
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

        if(InteractionObject!=null&&Input.GetKeyDown("e"))
        {
            if(InteractionObject.name== "LightSwitchVintage")
            {
                InteractionObject.GetComponent<LightSwitch>().LightsSwitch();
            }
            if (InteractionObject.name == "CodeYellow")
            {
                InteractionObject.GetComponent<Animator>().SetTrigger("CodeMove");
                InteractionObject.name = "CodeYellowOutside";
            }
            if (InteractionObject.name == "CodeYellowOutside")
            {
                G_Camera.GetComponent<CameraController>().CameraZoomObject("CodeYellowOutside",InteractionObject.transform);              
            }
            if (InteractionObject.name == "DoorMain")
            {
                if (InventoryKey)
                {
                    InteractionObject.GetComponent<Animator>().SetBool("DoorOpen", true);
                }
                else if (!InventoryKey)
                {
                    InformationTextController("I need to find the key first.");
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
            InformationTextController("Press E to Open the Door");            
        }
        if(other.name== "CodeYellow")
        {
            InformationTextController("Press E to Check whats under the Rug.");
        }
        if (other.name == "CodeYellowOutside")
        {
            InformationTextController("Press E to Check the Code.");
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
        if (other.name == "CodeYellow")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "CodeYellowOutside")
        {
            InteractionObject = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain"|| other.name == "CodeYellow"|| other.name == "CodeYellowOutside")
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }

}

