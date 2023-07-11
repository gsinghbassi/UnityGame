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
    public GameObject CupboardClue;
    public GameObject Document;
    //Sound
    AudioSource ButlerAudioController;
    public AudioClip[] Sound_Footsteps;
    int SelectedSoundFootstep;
    public AudioClip Sound_CupboardDoorOpen;
    public AudioClip Sound_LigtSwitch;
    public AudioClip Sound_ClueReveal;
    public AudioClip Sound_ClueSlide;
    public AudioClip Sound_ClueSlide2;
    public AudioClip Sound_Collection;



    // Start is called before the first frame update
    void Start()
    {
        ButlerAudioController = GetComponent<AudioSource>();
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
                    SceneManager.LoadScene("Level4_Cutscene");                   
                }
            }
            if (InteractionObject.name == "LightSwitchVintage")
            {
                InteractionObject.GetComponent<LightSwitchRoom2>().LightsSwitch();
                ButlerAudioController.PlayOneShot(Sound_LigtSwitch);
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
                ButlerAudioController.PlayOneShot(Sound_Collection);
            }
            if (InteractionObject.name == "Rug Clue")
            {
                InteractionObject.GetComponent<RugCodeMaths>().RugOut = true;
                InteractionObject.name = "Rug Clue Out";
                ButlerAudioController.PlayOneShot(Sound_ClueSlide);
            }
            if (InteractionObject.name == "Rug Clue Out")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("RugClue", InteractionObject.transform);
                InformationText.text = "";
                ButlerAudioController.PlayOneShot(Sound_ClueReveal);
            }
            if (InteractionObject.name == "TV Collider Off")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("TVClue", InteractionObject.transform);
                InformationText.text = "";
                ButlerAudioController.PlayOneShot(Sound_ClueReveal);
            }
            
                if (InteractionObject.name == "DiceControl")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("Dice", InteractionObject.transform);
                InformationText.text = "";
            }


            if (InteractionObject.name == "CupboardLocked")
            {

                if (InventoryKey)
                {
                    InteractionObject.GetComponent<Cupboards>().DoorOpen = true;
                    InventoryBGImage.SetActive(false);
                    KeyImage.SetActive(false);
                    G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("CupboardClue", CupboardClue.transform);
                    InformationText.text = "";
                    InteractionObject.name = "CupboardUnlocked";
                }
                
            }
            if (InteractionObject.name == "CupboardUnlocked")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("CupboardClue", CupboardClue.transform);
              InformationText.text = "";

            }
            if (InteractionObject.name == "ChestDrawers")
            {
                G_Camera.GetComponent<CameraControllerRoom2>().CameraZoomObject("ChestLock", InteractionObject.transform);
                InformationText.text = "";
            }

            if (InteractionObject.name == "ChestDrawersOpened")
            {
                Document.SetActive(false);
                InformationText.text = "I have collected Evidence. Now I can go to the other Room";
                InteractionObject.name = "ChestDrawerEmpty";
                KeyImage.SetActive(false);
                DocumentImage.SetActive(true);
                InventoryBGImage.SetActive(true);
                InventoryDocument = true;
                G_Camera.GetComponent<CameraControllerRoom2>().Back();

            }

                if (InteractionObject.name == "CupboardRight")
            {
                if (InteractionObject.GetComponent<Animator>().GetBool("COpen") == true)
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
        
            if (other.name == "Rug Clue")
        {
            InformationTextController("Press E to Check Whats under the Rug");
        }
        if (other.name == "Rug Clue Out")
        {
            InformationTextController("Press E to Check the Clue");
        }
        if (other.name == "TV Collider On")
        {
            InformationTextController("I need to find a way to turn ON the TV");
        }
        if (other.name == "TV Collider On2")
        {
            InformationTextController("I need to Switch the Lights OFF to Turn the TV ON");
        }
        if (other.name == "TV Collider Off")
        {
            InformationTextController("Press E to Check the Clue");
        }        
            if (other.name == "DiceControl")
        {
            InformationTextController("Press E to Check Dice");
        }

        
            if (other.name == "CupboardUnlocked")
        {
            InformationTextController("Press E to check the Clue inside the Cupboard");
        }

            if (other.name == "CupboardLocked")
        {

            if (InventoryKey)
            {
                InformationTextController("Press E to use Key and Open Door Lock");
            }
            else if (!InventoryKey)
            {
                InformationTextController("I need to find a key to Open the Lock");
            }
        }


            if (other.name == "CupboardRight")
        {
            if (other.GetComponent<Animator>().GetBool("COpen") == false)
            {
                InformationTextController("Press E to open the cupboard door");
            }
            else if (other.GetComponent<Animator>().GetBool("COpen") == true)
            {
                InformationTextController("Press E to close the cupboard door");
            }
        }
        if (other.name == "ChestDrawers")
        {
            InformationTextController("Press E to open the Drawer");
        }

        if (other.name == "ChestDrawersOpened")
        {
            InformationTextController("Press E to Take the Document");
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
        
            if (other.name == "Rug Clue")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "TV Collider On")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "TV Collider On2")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "TV Collider Off")
        {
            InteractionObject = other.gameObject;
        }        
            if (other.name == "DiceControl")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "CupboardRight")
        {
            InteractionObject = other.gameObject;
        }

        
        if (other.name == "CupboardLocked")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "CupboardUnlocked")
        {
            InteractionObject = other.gameObject;
        }

        if (other.name == "ChestDrawers")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "ChestDrawersOpened")
        {
            InteractionObject = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain" || other.name == "Document1" || other.name == "PaintingClock" || other.name == "Clock" || other.name == "Key-Cupboard" || other.name == "Rug Clue" || other.name == "TV Collider Off" || other.name == "TV Collider On" || other.name == "TV Collider On2" || other.name == "DiceControl" || other.name == "CupboardRight" || other.name == "CupboardLocked" || other.name == "CupboardUnlocked" || other.name == "ChestDrawers" || other.name == "ChestDrawersOpened") 
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }

    public void Step()
    {
        if (SelectedSoundFootstep < Sound_Footsteps.Length - 1)
        {
            SelectedSoundFootstep++;
        }
        else if (SelectedSoundFootstep == Sound_Footsteps.Length - 1)
        {
            SelectedSoundFootstep = 0;
        }
        ButlerAudioController.PlayOneShot(Sound_Footsteps[SelectedSoundFootstep]);
    }

}

