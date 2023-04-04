using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Menu : MonoBehaviour
{

    GameObject MenuMain;
    GameObject MenuOptions;
    GameObject MenuCredits;
    GameObject MenuHowTo;
    AudioSource AudioPlayer;    
    Toggle MusicToggle;


    public AudioClip ClickSound;
    public AudioMixer AudioMixerController;


    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer = GetComponent<AudioSource>();
        MenuMain = transform.Find("MenuMain").gameObject;
        MenuOptions = transform.Find("MenuOptions").gameObject;
        MenuCredits = transform.Find("MenuCredits").gameObject;
        MenuHowTo = transform.Find("MenuHowTo").gameObject;
        MenuMain.SetActive(true);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuHowTo.SetActive(false);
        MusicToggle = MenuOptions.transform.Find("MusicToggle").GetComponent<Toggle>();        
        LoadSettings();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        SceneManager.LoadScene("2-CharacterSelect");
    }

    public void Options()
    {

        AudioPlayer.PlayOneShot(ClickSound, 1f);
        MenuMain.SetActive(false);
        MenuOptions.SetActive(true);
        MenuCredits.SetActive(false);
        MenuHowTo.SetActive(false);
    }
    public void Credits()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        MenuMain.SetActive(false);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(true);
        MenuHowTo.SetActive(false);
    }
    public void HOWTO()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        MenuMain.SetActive(false);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuHowTo.SetActive(true);

    }


        public void Back()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        MenuMain.SetActive(true);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuHowTo.SetActive(false);
    }
    public void BackOptions()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);      
        MenuMain.SetActive(true);
        MenuOptions.SetActive(false);
        MenuCredits.SetActive(false);
        MenuHowTo.SetActive(false);

    }

    public void CheckUncheckOptions()
    {
        AudioPlayer.PlayOneShot(ClickSound, 0.7f);
        if (MusicToggle.isOn == true)
        {
            PlayerPrefs.SetInt("Music", 0);
            AudioMixerController.SetFloat("G_MusicVolume", 0f);
        }
        else if (MusicToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Music", 1);
            AudioMixerController.SetFloat("G_MusicVolume", -80.0f);
        }

    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
        
    }

    
    void LoadSettings()
    {
       
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            MusicToggle.isOn = false;
            AudioMixerController.SetFloat("G_MusicVolume", -80.0f);

        }
        else if (PlayerPrefs.GetInt("Music") == 0)
        {
            MusicToggle.isOn = true;
            AudioMixerController.SetFloat("G_MusicVolume", 0f);

        }
    }

}
