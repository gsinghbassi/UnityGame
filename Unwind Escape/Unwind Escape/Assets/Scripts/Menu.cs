using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public AudioSource MenuAudioController;
    public AudioClip ClickSound;
    float clicksounddelay;
    public bool Room1Active;
    public bool Room2Active;
    public bool Room3Active;
    public Button Room1Button;
    public Button Room2Button;
    public Button Room3Button;


    //Menus
    public GameObject MenuMain;
    public GameObject MenuOptions;
    public GameObject MenuCredits;
    public GameObject MenuSelectRoom;

    //Options    
    int selectedGraphicsQuality;
    public Slider SliderGFX;
    public Slider SliderSFX;
    public Slider SliderMusic;
    public Slider SliderMasterVolume;
    public AudioMixer AudioMixerControls;



    // Start is called before the first frame update
    void Start()
    {
        
        Room1Active = false;
        Room2Active = false;
        Room3Active = false;        
        MenuMain.SetActive(true);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuSelectRoom.SetActive(false);
        selectedGraphicsQuality = 3;
        SliderSFX.value = 4f;
        SliderMusic.value = -8f;
        SliderMasterVolume.value = 9f;
        QualitySettings.SetQualityLevel(2, true);
        AudioMixerControls.SetFloat("sfxvol", SliderSFX.value);
        AudioMixerControls.SetFloat("musicvol", SliderMusic.value);
        AudioMixerControls.SetFloat("mastervol", SliderMasterVolume.value);
        RoomChecker();

    }


    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }
    public void SelectRoom()
    {
        MenuAudioController.PlayOneShot(ClickSound);
        MenuMain.SetActive(false);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuSelectRoom.SetActive(true);
    }

    public void Options()
    {
        MenuAudioController.PlayOneShot(ClickSound);
        MenuMain.SetActive(false);
        MenuOptions.SetActive(true);
        MenuCredits.SetActive(false);
        MenuSelectRoom.SetActive(false);
    }
    public void Credits()
    {
        MenuAudioController.PlayOneShot(ClickSound);
        MenuMain.SetActive(false);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(true);
        MenuSelectRoom.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Back()
    {
        MenuAudioController.PlayOneShot(ClickSound);
        MenuMain.SetActive(true);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuSelectRoom.SetActive(false);
        //Saving Prefs
        PlayerPrefs.SetFloat("sfxvol", SliderSFX.value);
        PlayerPrefs.SetFloat("musicvol", SliderMusic.value);
        PlayerPrefs.SetFloat("mastervol", SliderMasterVolume.value);
        PlayerPrefs.SetFloat("gfx", selectedGraphicsQuality);
    }
    public void SelectedRoom(int G_Input)
    {
        if (G_Input == 1)
        {
            SceneManager.LoadScene("Level1_Room1");
        }
        if (G_Input == 2)
        {
            SceneManager.LoadScene("Level3_Room2");
        }
        if (G_Input == 3)
        {
            SceneManager.LoadScene("Level5_Room3");
        }
    }
    public void SliderGFXControls()
    {
        selectedGraphicsQuality = (int)SliderGFX.value;

        switch (selectedGraphicsQuality)
        {
            case 1:
                QualitySettings.SetQualityLevel(0, true);
                break;
            case 2:
                QualitySettings.SetQualityLevel(1, true);
                break;
            case 3:
                QualitySettings.SetQualityLevel(2, true);
                break;
        }


    }
    public void SliderSoundControls(string G_Input)
    {
        if (G_Input == "sfx")
        {
            AudioMixerControls.SetFloat("sfxvol", SliderSFX.value);
            if (Time.time > clicksounddelay)
            {
                MenuAudioController.PlayOneShot(ClickSound);
                clicksounddelay = Time.time + 0.5f;
            }
        }
        if (G_Input == "music")
        {
            AudioMixerControls.SetFloat("musicvol", SliderMusic.value);
        }
        if (G_Input == "master")
        {
            AudioMixerControls.SetFloat("mastervol", SliderMasterVolume.value);
        }


    }
    public void RoomChecker()
    {
        if (PlayerPrefs.GetInt("Room1Active") == 1)
        { 
            Room1Active = true;
        }
        if (PlayerPrefs.GetInt("Room2Active") == 1)
        {
            Room2Active = true;
        }
        if (PlayerPrefs.GetInt("Room3Active") == 1)
        {
            Room3Active = true;
        }
        if (Room1Active)
        {
            Room1Button.interactable = true;
        }
        else if (!Room1Active)
        {
            Room1Button.interactable = false;
        }
        if (Room2Active)
        {
            Room2Button.interactable = true;
        }
        else if (!Room2Active)
        {
            Room2Button.interactable = false;
        }
        if (Room3Active)
        {
            Room3Button.interactable = true;
        }
        else if (!Room3Active)
        {
            Room3Button.interactable = false;
        }
    }

}
