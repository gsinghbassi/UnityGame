using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CharacterSelection : MonoBehaviour
{
    AudioSource AudioPlayer;
    public AudioClip ClickSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Back()
    {
        AudioPlayer.PlayOneShot(ClickSound, 1f);
        SceneManager.LoadScene("1-Start");
    }
}
