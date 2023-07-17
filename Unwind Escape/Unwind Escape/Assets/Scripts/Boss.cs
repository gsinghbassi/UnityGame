using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Transform Point2;
    public Transform Point3;
    public Vector3 nextPoint;
    public float playerspeed;
    float startTime;
    AudioSource ButlerAudioController;
    public AudioClip[] Sound_Footsteps;
    int SelectedSoundFootstep;
    Animator BossAnimator;
    public bool conversation;
    public Quaternion point2rotation;
    public Quaternion point3rotation;
    Quaternion nextrotation;
    bool firstrotationupdate;
    


    // Start is called before the first frame update
    void Start()
    {
        
        firstrotationupdate = false;
        nextrotation = transform.rotation;
        nextPoint = Point2.position;
        Time.timeScale = 1f;
        conversation = true;
        BossAnimator = GetComponent<Animator>();
        ButlerAudioController = GetComponent<AudioSource>();        
        startTime = Time.time+0.5f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

        
        if (!conversation)
        {
            if(!firstrotationupdate)
            {
                nextrotation = point2rotation;
                firstrotationupdate = true;

            }
            transform.rotation = nextrotation;
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, playerspeed*Time.deltaTime);
            BossAnimator.SetBool("Walk", true);
        }
        
            
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name== "BossP2")
        { 
            nextPoint = Point3.position;
            nextrotation = point3rotation;
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
