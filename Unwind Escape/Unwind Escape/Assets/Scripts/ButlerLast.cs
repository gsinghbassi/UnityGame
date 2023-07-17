using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButlerLast : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public float playerspeed;
    float startTime;
    AudioSource ButlerAudioController;
    public AudioClip[] Sound_Footsteps;
    int SelectedSoundFootstep;
    Animator ButlerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        ButlerAnimator = GetComponent<Animator>();
        ButlerAudioController = GetComponent<AudioSource>();
        transform.position = Point1.position;
        startTime = Time.time+0.5f;
        ButlerAnimator.SetBool("Walk", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Time.time > startTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point2.position, playerspeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "Point2")
        {
            LastSceneController.Conversation1 = true;
            StartCoroutine(Delay());
            other.name = "point2reached";
            
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
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        ButlerAnimator.SetBool("Walk", false);
    }
}
