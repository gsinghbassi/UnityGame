using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class InGameMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseOptionsMenu;
    public static bool menuActive;
    public GameObject Canvas1;

    


    //Options    
    int selectedGraphicsQuality;
    public Slider SliderGFX;
    public Slider SliderSFX;
    public Slider SliderMusic;
    public Slider SliderMasterVolume;
    public AudioMixer AudioMixerControls;
    AudioSource MenuAudioController;
    public AudioClip ClickSound;
    bool clicksounddelay;
    public string RoomName;

    // Start is called before the first frame update
    void Start()
    {
        RoomStateUpdater(RoomName);
        clicksounddelay = false;
        Canvas1.SetActive(true);
        MenuControls(0);
        menuActive = false;
        MenuAudioController = GetComponent<AudioSource>();
        //Loading Prefs        
        SliderGFX.value= PlayerPrefs.GetFloat("gfx");
        SliderGFXControls();
        SliderSFX.value = PlayerPrefs.GetFloat("sfxvol");
        SliderMusic.value = PlayerPrefs.GetFloat("musicvol");
        SliderMasterVolume.value = PlayerPrefs.GetFloat("mastervol");
        AudioMixerControls.SetFloat("sfxvol", SliderSFX.value);
        AudioMixerControls.SetFloat("musicvol", SliderMusic.value);
        AudioMixerControls.SetFloat("mastervol", SliderMasterVolume.value);
        
    }

    // Update is called once per frame
    void Update()
    {

      

        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if(!menuActive)
            {
                MenuControls(1);
            }
           else if(menuActive)
            {
                MenuControls(0);
            }

        }
    }

    public void MenuControls(int G_Input)
    {
        if(G_Input==0)
        {
            Canvas1.SetActive(true);
            menuActive = false;
            PauseMenu.SetActive(false);
            PauseOptionsMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (G_Input == 1)
        {
            Canvas1.SetActive(false);
            menuActive = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    public void Resume()
    {
        MenuControls(0);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Options()
    {
        PauseOptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void Back()
    {
        PauseOptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
        //Saving Prefs
        PlayerPrefs.SetFloat("sfxvol", SliderSFX.value);
        PlayerPrefs.SetFloat("musicvol", SliderMusic.value);
        PlayerPrefs.SetFloat("mastervol", SliderMasterVolume.value);
        PlayerPrefs.SetFloat("gfx", selectedGraphicsQuality);
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
            if (!clicksounddelay)
            {
                MenuAudioController.PlayOneShot(ClickSound);
                clicksounddelay = true;
                StartCoroutine(ClickDelay());
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
    IEnumerator ClickDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        clicksounddelay = false;

    }

    public void RoomStateUpdater(string G_Input)
    {
        if(G_Input=="Room1")
        {
            PlayerPrefs.SetInt("Room1Active", 1);
        }
        if (G_Input == "Room2")
        {
            PlayerPrefs.SetInt("Room2Active", 1);
        }
        if (G_Input == "Room3")
        {
            PlayerPrefs.SetInt("Room3Active", 1);
        }
    }

}
