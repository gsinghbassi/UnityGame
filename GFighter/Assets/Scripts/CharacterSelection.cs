using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class CharacterSelection : MonoBehaviour
{
    
    //Character Variables
    TextMeshProUGUI CharacterSelect;
    int SelectedCharacter;
    int SelectedPlayerCharacter;
    int SelectedCPUCharacter;
    int SelectedPreviousCharacter;
    bool PlayerCharacterSelected;
    bool CPUCharacterSelected;
    Transform UISelectedCharacter;
    public int maxCharacters;
    GameObject[] Characters;
    GameObject[] CharactersPlayers3DModels;
    GameObject[] CharactersCPU3DModels;
    Transform G_3Dmodels;

   

    //Audio Variables
    AudioSource AudioPlayer;
    public AudioClip ClickSound;
    public AudioClip CharacterMoveSound;
    public AudioClip CharacterSelectSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacterSelected = false;
        CPUCharacterSelected = false;
           G_3Dmodels = GameObject.Find("CharacterModels").transform;
        AudioPlayer = GetComponent<AudioSource>();
        UISelectedCharacter = transform.Find("UISelected");
        CharacterSelect = transform.Find("ChooseCharacterText").GetComponent<TextMeshProUGUI>();
        CharacterSelect.text = "Choose Your Character";
        maxCharacters = transform.Find("Characters").transform.childCount;
        Characters = new GameObject[maxCharacters];
        CharactersPlayers3DModels = new GameObject[maxCharacters];
        CharactersCPU3DModels = new GameObject[maxCharacters];
        for (int i = 0; i < maxCharacters; i++)
         {             
            Characters[i] = transform.Find("Characters").transform.GetChild(i).gameObject;
            CharactersPlayers3DModels[i] = G_3Dmodels.Find("Player").transform.GetChild(i).gameObject;
            CharactersCPU3DModels[i] = G_3Dmodels.Find("Cpu").transform.GetChild(i).gameObject;
         }
        SelectedCharacter = 0;
        SelectedPreviousCharacter = SelectedCharacter;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (!PlayerCharacterSelected)
            {
                SelectedPlayerCharacter = SelectedCharacter;
                ChangeText(1);
                PlayerCharacterSelected = true;
                InitializeOtherPlayerforCpu(SelectedCharacter);
                CharactersPlayers3DModels[SelectedPlayerCharacter].GetComponent<Animator>().SetTrigger("Menu_IdleToReady");
                AudioPlayer.PlayOneShot(CharacterSelectSound);

            }
            else if(PlayerCharacterSelected)
            {
                SelectedCPUCharacter = SelectedCharacter;
                ChangeText(2);
                CPUCharacterSelected = true;
                CharactersCPU3DModels[SelectedCPUCharacter].GetComponent<Animator>().SetTrigger("Menu_IdleToReady");
                UISelectedCharacter.GetComponent<Animator>().enabled=false;
                AudioPlayer.PlayOneShot(CharacterSelectSound);
                transform.Find("Back").gameObject.SetActive(false);
                transform.Find("Instructions").gameObject.SetActive(false);
                StartCoroutine(LoadScene());
            }

        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Left");    //Remove Later
            CharacterSelecter(0);
            AudioPlayer.PlayOneShot(CharacterMoveSound);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");    //Remove Later
            CharacterSelecter(1);
            AudioPlayer.PlayOneShot(CharacterMoveSound);
        }
    }

    void InitializeOtherPlayerforCpu(int G_Input)
    {

        if (G_Input < maxCharacters && G_Input != 0)
        {
            CharacterSelecter(0);
        }
        if (G_Input == 0)
        {
            CharacterSelecter(1);
        }
       
    }

   
    void CharacterSelecter(int G_Input)
    {
        if (!CPUCharacterSelected)
        {
            if (G_Input == 0 && SelectedCharacter > 0)
            {
                SelectedCharacter--;
            }

            if (G_Input == 1 && SelectedCharacter < maxCharacters - 1)
            {
                SelectedCharacter++;
            }

            if (SelectedCharacter != SelectedPreviousCharacter)
            {
                UISelectedCharacter.position = Characters[SelectedCharacter].transform.position;
                SelectedPreviousCharacter = SelectedCharacter;
                Upated3Dmodel(SelectedCharacter);
            }

        }
    }

    void Upated3Dmodel(int G_Input)
    {
        if (!PlayerCharacterSelected)
        {
            for (int i = 0; i < maxCharacters; i++)
            {
                CharactersPlayers3DModels[i].SetActive(false);
            }
            CharactersPlayers3DModels[G_Input].SetActive(true);
        }
        if (PlayerCharacterSelected)
        {
            for (int i = 0; i < maxCharacters; i++)
            {
                CharactersCPU3DModels[i].SetActive(false);
            }
            CharactersCPU3DModels[G_Input].SetActive(true);
        }

    }


    void ChangeText(int G_Input)
    {
        
        if(G_Input == 0)
        {
            CharacterSelect.text = "Choose Your Character";
        }
        if (G_Input == 1)
        {
            CharacterSelect.text = "Choose CPU Character";
        }
        if (G_Input == 2)
        {
            CharacterSelect.text = "Prepare for Battle";
        }
    }
    public void Back()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        SceneManager.LoadScene("1-Start");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("3-FightingArena");
    }

}
 