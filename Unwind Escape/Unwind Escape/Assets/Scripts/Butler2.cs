using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butler2 : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public float playerspeed;
    float startTime;
    public string scenename;
    AudioSource ButlerAudioController;
    public AudioClip[] Sound_Footsteps;
    int SelectedSoundFootstep;

    // Start is called before the first frame update
    void Start()
    {
        ButlerAudioController = GetComponent<AudioSource>();
        transform.position = Point1.position;
        startTime = Time.time+0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            transform.position = Point1.position;
        }
        if (Time.time > startTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point2.position, playerspeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="NextScene")
        {
            SceneManager.LoadScene(scenename);
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
