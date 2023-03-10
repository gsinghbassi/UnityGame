using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;



public class InGameMenu : MonoBehaviour
{
    GameObject MenuButtons;
    AudioSource AudioPlayer;
    public AudioClip ClickSound;
    public bool menuallowed;
    


    // Start is called before the first frame update
    void Start()
    {
        menuallowed = true;
        Time.timeScale = 1f;
        MenuButtons = transform.Find("MenuButtons").gameObject;
        AudioPlayer = GetComponent<AudioSource>();
        MenuButtons.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MenuActiveDeactive();
           
        }
    }
    public void MenuActiveDeactive()
    {
        if(MenuButtons.activeSelf &&menuallowed)
        {
            Resume();

        }
        else if (!MenuButtons.activeSelf && menuallowed)
        {
            MenuButtons.SetActive(true);
            Pause();
        }
    }
    public void QuitGame()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        Application.Quit();
    }

    public void QuitToMenu()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Resume()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        MenuButtons.SetActive(false);
        UnPause();
    }
    void Pause()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        Time.timeScale = 0f;
    }
    void UnPause()
    {
        Time.timeScale = 1f;
    }
}
