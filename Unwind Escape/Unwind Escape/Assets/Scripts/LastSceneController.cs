using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSceneController : MonoBehaviour
{
    public GameObject Butler1;
    public GameObject Butler2;
    public GameObject ButlerDrive;
    public GameObject Boss;
    public GameObject BossDrive;
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;
    public GameObject Cam4;
    public GameObject BossBriefcase;
    public GameObject Car;
    public static bool Conversation1;
    public static bool Conversation2;
    public static bool ConversationEnded;
    public bool DrivingScene;
    public GameObject Credits;
    bool continuegame;
    public GameObject DiaButler;
    public GameObject DiaBoss;
    public GameObject confirmationMessage;
    bool confirm1;
    bool confirm2;
    public AudioClip bgAmbientSound;
    public AudioClip CreditsMusic;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(bgAmbientSound);
        confirm1 = false;
        confirm2 = false;
        confirmationMessage.SetActive(false);
        DiaButler.SetActive(false);
        DiaBoss.SetActive(false);
        continuegame = false;
        Credits.SetActive(true);
        Credits.GetComponent<Animator>().SetBool("credits", false);
        DrivingScene = false;
        Conversation1 = false;
        Butler2.SetActive(false);
        ButlerDrive.SetActive(false);
        BossDrive.SetActive(false);
        Cam1.SetActive(true);
        Cam2.SetActive(false);
        Cam3.SetActive(false);
        Cam4.SetActive(false);
        BossBriefcase.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (continuegame)
        {
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene("Menu");
            }

        }
        if (Conversation1)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
            Cam3.SetActive(false);
            Cam4.SetActive(false);
            Conversation1 = false;
            DiaButler.SetActive(true);
            StartCoroutine(Confirm1delay());
        }
        if (Input.GetKeyDown("e")&&confirm1)
        {
            confirmationMessage.SetActive(false);
            confirm1 = false;
            Conversation2 = true;
        }
        if (Input.GetKeyDown("e") && confirm2)
        {
            confirm2 = false;
            ConversationEnded = true;
        }

        if (Conversation2)
        {
            StartCoroutine(Confirm2delay());
            DiaBoss.SetActive(true);
            DiaButler.SetActive(false);
            Cam1.SetActive(false);
            Cam2.SetActive(false);
            Cam3.SetActive(true);
            Cam4.SetActive(false);
            Conversation2 = false;
        }
        if (ConversationEnded)
        {
            confirmationMessage.SetActive(false);
            DiaButler.SetActive(false);
            DiaBoss.SetActive(false);
            Cam1.SetActive(true);
            Cam2.SetActive(false);
            Cam3.SetActive(false);
            Cam4.SetActive(false);
            Butler1.SetActive(false);
            Butler2.SetActive(true);
            Butler2.GetComponent<Animator>().SetBool("Walk", true);
            BossBriefcase.SetActive(true);
            Boss.GetComponent<Boss>().conversation = false;
            ConversationEnded = false;
        }
        if (DrivingScene)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(false);
            Cam3.SetActive(false);
            Cam4.SetActive(true);
            Boss.SetActive(false);
            Butler2.SetActive(false);
            BossDrive.SetActive(true);
            ButlerDrive.SetActive(true);
            DrivingScene = false;
            Car.GetComponent<AudioSource>().Play();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Boss Idle")
        {
            DrivingScene = true;
            StartCoroutine(CarMove());
        }
    }


    IEnumerator CarMove()
    {
        yield return new WaitForSeconds(3f);
        Cam1.SetActive(true);
        Cam2.SetActive(false);
        Cam3.SetActive(false);
        Cam4.SetActive(false);
        Car.GetComponent<Animator>().SetBool("Move", true);
        StartCoroutine(CreditsDelay());
    }

    IEnumerator CreditsDelay()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(CreditsMusic);
        yield return new WaitForSeconds(3f);        
        Credits.GetComponent<Animator>().SetBool("credits", true);
        continuegame = true;
    }
    IEnumerator Confirm1delay()
    {
        yield return new WaitForSeconds(2f);
        confirmationMessage.SetActive(true);
        confirm1 = true;
    }
    IEnumerator Confirm2delay()
    {
        yield return new WaitForSeconds(2f);
        confirmationMessage.SetActive(true);
        confirm2 = true;
    }
}
