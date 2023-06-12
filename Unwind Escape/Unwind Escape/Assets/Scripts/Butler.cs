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
    public GameObject ChestwithKey;
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
                InformationText.text = "";
            }
            if (InteractionObject.name == "CodeOrange")
            {
                G_Camera.GetComponent<CameraController>().CameraZoomObject("CodeOrange", InteractionObject.transform);
                InformationText.text = "";
            }
            if (InteractionObject.name == "Painting")
            {
                GameObject.Find("PaintingPivot").GetComponent<Animator>().SetTrigger("Rotate");                
                G_Camera.GetComponent<CameraController>().CameraZoomObject("CodeRed", InteractionObject.transform);
                InformationText.text = "";
                InteractionObject.name = "CodeRed";
            }
            if (InteractionObject.name == "CodeRed")
            {
                G_Camera.GetComponent<CameraController>().CameraZoomObject("CodeRed", InteractionObject.transform);
                InformationText.text = "";
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
            if (InteractionObject.name == "Cupboard1" || InteractionObject.name == "Cupboard2"  || InteractionObject.name == "Cupboard4")
            {
                
                if (InteractionObject.GetComponent<Animator>().GetBool("COpen")==true)
                {
                    InteractionObject.GetComponent<Animator>().SetBool("COpen", false);
                    InteractionObject.GetComponent<Cupboards>().DoorOpen = false;
                    InformationTextController("Press E to open the cupboard door.");
                }
                else if (InteractionObject.GetComponent<Animator>().GetBool("COpen") == false)
                {
                    InteractionObject.GetComponent<Animator>().SetBool("COpen", true);
                    InteractionObject.GetComponent<Cupboards>().DoorOpen = true;
                    InformationTextController("Press E to close the cupboard door.");
                }

            }
            if(InteractionObject.name == "CupboardWithChest")
            {
                InformationText.text = "";
                InteractionObject.GetComponent<Animator>().SetBool("COpen", true);
                InteractionObject.GetComponent<BoxCollider>().enabled = false;
                InteractionObject.GetComponent<Cupboards>().DoorOpen = true;
                ChestwithKey.GetComponent<BoxCollider>().enabled = true;
                G_Camera.GetComponent<CameraController>().CameraZoomObject("Chest", ChestwithKey.transform);

            }

            if (InteractionObject.name == "Chest")
            {
                InformationText.text = "";
                G_Camera.GetComponent<CameraController>().CameraZoomObject("Chest", InteractionObject.transform);

            }
            if (InteractionObject.name == "Key")
            {
                InformationText.text = "";
                Destroy(InteractionObject);
                G_Camera.GetComponent<CameraController>().Back();
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
            InformationTextController("Press E to Check the Code on the Floor.");
        }
        if (other.name == "CodeOrange")
        {
            InformationTextController("Press E to Check whats on top of the table.");
        }
        if (other.name == "WallHint")
        {
            InformationTextController("This wall has no furniture. Looks mysterious!");
        }
        if (other.name == "Painting")
        {
            InformationTextController("Press E to inspect the Painting");
        }
        if (other.name == "CodeRed")
        {
            InformationTextController("Press E to check the Code");
        }
        if (other.name == "Cupboard1"|| other.name == "Cupboard2"||other.name == "CupboardWithChest" || other.name == "Cupboard4")
        {
            if (other.GetComponent<Animator>().GetBool("COpen") == false)
            {
                InformationTextController("Press E to open the cupboard door.");
            }
            else if (other.GetComponent<Animator>().GetBool("COpen") == true)
            {
                InformationTextController("Press E to close the cupboard door.");
            }
        }
        if (other.name == "Chest")
        {
            InformationTextController("Press E to Open the Chest.");
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
        if (other.name == "CodeYellow")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "CodeYellowOutside")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "CodeOrange")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Painting")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "CodeRed")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Cupboard1" || other.name == "Cupboard2" || other.name == "CupboardWithChest" || other.name == "Cupboard4")
        {
            InteractionObject = other.gameObject;
        }
        if(other.name=="Chest")
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
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain"|| other.name == "CodeYellow"|| other.name == "CodeYellowOutside" || other.name == "CodeOrange"|| other.name == "WallHint" || other.name == "Painting" || other.name == "CodeRed")
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }

}

