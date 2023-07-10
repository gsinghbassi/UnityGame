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
    public bool InventoryDart1;
    public bool InventoryDart2;
    public bool InventorySpray;
    Camera G_Camera;
    public static bool clearinteractionobjects;
    public GameObject InventoryBGImage;
    public List<string> InventoryItems = new List<string>();
    public GameObject InventoryController;
    public int InventoryItemNumber;
    Sprite ChosenInventoryImage;
    public Sprite Dart1Image;
    public Sprite Dart2Image;
    public Sprite HammerImage;
    public Sprite SprayImage;
    public Sprite DocumentImage;
    public GameObject Formula;
    bool formulaactivated;
    public GameObject DartTargetRing;
    public GameObject[] DartTargets;
    int x;
    public GameObject DartinBoard1;
    public GameObject DartinBoard2;
    public TextMeshProUGUI Dart1Text;
    public TextMeshProUGUI Dart2Text;    
    bool dart1Thrown;
    bool dart2Thrown;
    Vector3 HighlightedPosition;
    Vector3 DartRestPosition = new Vector3(-4,0,-3);
    public GameObject DartinPlaceCheck8;
    public GameObject DartinPlaceCheck7;
    public GameObject DocumentCupboard;
    bool CupboardActivated;
    public GameObject Document2;





    // Start is called before the first frame update
    void Start()
    {
        Document2.name = "Document2NotReady";
        CupboardActivated = false;
        x = 0;
        DartTargetRing.transform.position = DartTargets[x].transform.position;
        HighlightedPosition = DartTargets[x].transform.position;
        DartinBoard1.transform.position = DartRestPosition;
        DartinBoard2.transform.position = DartRestPosition;
        dart1Thrown=false;
        dart2Thrown=false;
        Dart1Text.text = "Throw Dart 1";
        Dart2Text.text = "Throw Dart 2";
        Formula.SetActive(false);
        formulaactivated=false;
        InventoryItemNumber = 0;
         InventoryDocument = false;
        InventoryHammer = false;
        ButlerAnimator = GetComponent<Animator>();
        G_Butler = GetComponent<NavMeshAgent>();
        G_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        InventoryBGImage.SetActive(false);
        for (int i = InventoryItemNumber; i < 4; i++)
        {
            InventoryController.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

       
       if(DartinPlaceCheck7.GetComponent<CheckDart>().DartChecker&&DartinPlaceCheck8.GetComponent<CheckDart>().DartChecker&&!CupboardActivated)
        {
            InteractionObject.name = "Dartboard Deactivated";
            DocumentCupboard.GetComponent<Cupboards>().DoorOpen = true;
            G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("DocumentCupboard", DocumentCupboard.transform);
            CupboardActivated = true;
            Document2.name = "Document2";
            InformationText.text = "I think thats the last evidence I need to collect. Let's quickly get it";
        }

        if (InventoryItemNumber==0)
        {
            InventoryBGImage.SetActive(false);
         }
        else if (InventoryItemNumber > 0)
        {
            InventoryBGImage.SetActive(true);
        }

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
                    InventoryManager("Hammer", false);
                    InventoryHammer = false;

                }
                else if(!InventoryHammer)
                {
                    InformationTextController("OH! There is a LOCK. I need to find a way to open the Lock");
                }
            }
            if (InteractionObject.name == "Chest2")
            {
                InformationTextController("");
                G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Chest2Lock", InteractionObject.transform);
                
            }
                if (InteractionObject.name == "Dart1"&&!InventoryDart1)
            {
                InteractionObject.SetActive(false);
                InformationText.text = "";
                G_Camera.GetComponent<CameraControllerRoom3>().Back();
                InventoryDart1 = true;
                InventoryManager(InteractionObject.name,true);
                G_Camera.GetComponent<CameraControllerRoom3>().Dart1Button.GetComponent<Button>().interactable = true;
            }
            if (InteractionObject.name == "Dart2"&&!InventoryDart2)
            {
                InteractionObject.SetActive(false);
                InformationText.text = "";
                G_Camera.GetComponent<CameraControllerRoom3>().Back();
                InventoryDart2 = true;
                InventoryManager(InteractionObject.name, true);
                G_Camera.GetComponent<CameraControllerRoom3>().Dart2Button.GetComponent<Button>().interactable = true;
            }
            if (InteractionObject.name == "Hammer"&&!InventoryHammer)
            {
                InteractionObject.SetActive(false);
                InformationText.text = "";
                InventoryHammer = true;
                InventoryManager(InteractionObject.name, true);
            }
            if (InteractionObject.name == "Spray"&& !InventorySpray)
            {
                InteractionObject.SetActive(false);
                InformationText.text = "";
                InventorySpray = true;
                InventoryManager(InteractionObject.name, true);
            }
            if (InteractionObject.name == "DeskLamp")
            {
                InteractionObject.GetComponent<DeskLamp>().UpdateLight();               
                
            }
            if (InteractionObject.name == "Charles")
            {
                InformationTextController("Hmm.There is a portrait of Charles Dickens with his Date of Birth");
                G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Charles", InteractionObject.transform);
                 }
            if (InteractionObject.name == "Clue")
            {
                G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Clue", InteractionObject.transform);
            }
            if (InteractionObject.name == "Document2")
            {
                Document2.SetActive(false);
                InventoryDocument = true;
                InventoryManager(InteractionObject.name, true);
                InformationText.text = "I got what I needed. Lets Leave this Building Now";
            }
            if (InteractionObject.name == "PaintingBack")
            {
                InformationTextController("");
                G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("PaintingBack", InteractionObject.transform);
            }
            if (InteractionObject.name == "Dartboard")
            {
                if (!InventoryDart1 && !InventoryDart2)
                {
                    InformationTextController("Why is there a dartboard here");
                    G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("DartboardNotReady", InteractionObject.transform);
                }
                if(InventoryDart1||InventoryDart2)
                {
                    
                    InformationTextController("");
                    G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Dartboard", InteractionObject.transform);

                }

            }


            if (InteractionObject.name == "Painting1")
            {
                if (!InventorySpray&&!formulaactivated)
                {
                    InformationTextController("I wonder what formula to use to solve this problem");
                    G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Painting1", InteractionObject.transform);
                }
                else if (!InventorySpray && formulaactivated)
                {
                    InformationTextController("I can use the formula to find the missing number");
                    G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Painting1", InteractionObject.transform);
                }

                if (InventorySpray)
                {
                    InventoryManager("Spray", false);
                    InventorySpray = false;
                    Formula.SetActive(true);
                    formulaactivated = true;
                    InformationTextController("The Spray revealed the Formula. Now i can find the missing number");
                    G_Camera.GetComponent<CameraControllerRoom3>().CameraZoomObject("Painting1", InteractionObject.transform);
                    
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
        if (other.name == "Dart1")
        {
            InformationTextController("Press E to take the Dart");
        }
        if (other.name == "Dart2")
        {
            InformationTextController("Press E to take the Dart");
        }
        if (other.name == "Hammer")
        {
            InformationTextController("Press E to take the Hammer");
        }
        if (other.name == "Spray")
        {
            InformationTextController("Press E to take the Spray");
        }
        if (other.name == "Chest2")
        {
            InformationTextController("Press E to open the Chest");
        }
        if (other.name == "DeskLamp")
        {
            InformationTextController("Press E to rotate the Lamp. I wonder what the light can reveal");
        }
        if (other.name == "Charles")
        {
            InformationTextController("Press E to Check the Painting");
        }
        if (other.name == "PaintingBack")
        {
            InformationTextController("Press E to Check the Painting");
        }
        if (other.name == "Clue")
        {
            InformationTextController("Press E to Check whats on the Sofa");
        }
        if (other.name == "Painting1")
        {
            if (!InventorySpray)
            {
                InformationTextController("Press E to Check the Painting");
            }
            else if(InventorySpray)
            {
                InformationTextController("Press E to Use the Spray on the Painting");
            }
        }
        if (other.name == "Dartboard")
        {
            InformationTextController("Press E to Check the Dartboard");
        }
        if (other.name == "Document2")
        {
            InformationTextController("Press E to take the Document");
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

        if (other.name == "Dart1")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Dart2")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Hammer")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Spray")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Chest2")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "DeskLamp")
        {
            InteractionObject = other.gameObject;
        }

        if (other.name == "Charles")
        {
            InteractionObject = other.gameObject;
        }
        
        if (other.name == "Painting1")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Clue")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "PaintingBack")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Dartboard")
        {
            InteractionObject = other.gameObject;
        }
        if (other.name == "Document2")
        {
            InteractionObject = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LightSwitchVintage" || other.name == "DoorMain" || other.name == "Document2" || other.name== "Chest1" || other.name== "Dart1" || other.name == "Dart2" || other.name == "Hammer" || other.name == "Spray" || other.name == "Chest2" || other.name == "DeskLamp" || other.name == "Charles" || other.name == "Painting1" ||other.name == "Clue" || other.name == "PaintingBack" || other.name == "Dartboard" || other.name == "Document2") 
        {
            InformationText.text = "";
            InteractionObject = null;
        }
    }

    public void InventoryManager(string G_Object, bool G_Add)
    {

        switch (G_Object)
        {
            case "Hammer":
                ChosenInventoryImage = HammerImage;
                break;
            case "Dart1":
                ChosenInventoryImage = Dart1Image;
                break;
            case "Dart2":
                ChosenInventoryImage = Dart2Image;
                break;
            case "Document2":
                ChosenInventoryImage = DocumentImage;
                break;
            case "Spray":
                ChosenInventoryImage = SprayImage;
                break;

        }
        

        if (G_Add)
        {
            
            InventoryItems.Add(G_Object);
            InventoryController.transform.GetChild(InventoryItemNumber).GetComponent<Image>().sprite=ChosenInventoryImage;
            InventoryController.transform.GetChild(InventoryItemNumber).GetComponent<Image>().enabled = true;
            InventoryItemNumber++;
        }
        else if(!G_Add)
        {

            InventoryItemNumber = InventoryItems.FindIndex(a=> a.Contains(G_Object));
            Debug.Log(InventoryItemNumber);


            InventoryItems.Remove(G_Object);
            if (InventoryItemNumber != InventoryItems.Count)
            {
                Debug.Log("This was Middle Element");
                for (int i = InventoryItemNumber; i <= InventoryItems.Count; i++)
                {
                    InventoryController.transform.GetChild(i).GetComponent<Image>().sprite = InventoryController.transform.GetChild(i+1).GetComponent<Image>().sprite;
                    Debug.Log("Replaced Element on index "+ i);
                }
                InventoryController.transform.GetChild(InventoryItems.Count).GetComponent<Image>().enabled = false;
            }

                if (InventoryItemNumber==InventoryItems.Count)
            { 
                Debug.Log("This was last element");
                for (int i = InventoryItemNumber; i < 3; i++)
                {
                    InventoryController.transform.GetChild(i).GetComponent<Image>().enabled = false;
                }
            }
            
            InventoryItemNumber =InventoryItems.Count;
        }
        }

    public void DartUpdate(int G_Input)
    {
        if(G_Input==1)
        {
            if (x < 19)
            {
                x++;
            }
            else if (x == 19)
            {
                x = 0;
            }
        }
        if(G_Input == 0)
        {
            if (x > 0)
            {
                x--;
            }
            else if (x == 0)
            {
                x = 19;
            }
        }
           
            
            DartTargetRing.transform.position = DartTargets[x].transform.position;
        HighlightedPosition= DartTargets[x].transform.position;

    }
        public void DartThrow(int G_Input)
    {
        if(G_Input==1&&!dart1Thrown)
        {
            dart1Thrown = true;
            Dart1Text.text = "Remove Dart1";
            DartinBoard1.transform.position = HighlightedPosition;
            InventoryManager("Dart1", false);                                                                                   
            InventoryDart1 = false;
        }
        else if (G_Input == 1 && dart1Thrown)
        {
            dart1Thrown = false;
            Dart1Text.text = "Throw Dart1";
            InventoryManager("Dart1", true);
            InventoryDart1 = true;
            DartinBoard1.transform.position = DartRestPosition;
        }
        if (G_Input == 2 && !dart2Thrown)
        {
            dart2Thrown = true;
            Dart2Text.text = "Remove Dart2";
            DartinBoard2.transform.position = HighlightedPosition;
            InventoryManager("Dart2", false);
            InventoryDart2 = false;
        }
        else if (G_Input == 2 && dart2Thrown)
        {
            dart2Thrown = false;
            Dart2Text.text = "Throw Dart2";
            InventoryManager("Dart2", true);
            InventoryDart2 = true;            
            DartinBoard2.transform.position = DartRestPosition;
        }

    }
    
}

