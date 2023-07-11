using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FollowPath : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;
    public float playerspeed;
    float startTime;
    public string scenename;
    public Vector3 NextPoint;
    AudioSource ButlerAudioController;
    public AudioClip[] Sound_Footsteps;
    int SelectedSoundFootstep;

    // Start is called before the first frame update
    void Start()
    {
        ButlerAudioController = GetComponent<AudioSource>();
        transform.position = Point1.position;
        startTime = Time.time + 0.5f;
        NextPoint = Point2.position;
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
            transform.position = Vector3.MoveTowards(transform.position, NextPoint, playerspeed);
       
        
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Point2")
        {
            NextPoint = Point3.position;
        }
        if(other.name=="Point3")
        {
            NextPoint = Point4.position;
        }
        if (other.name == "NextScene")
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
