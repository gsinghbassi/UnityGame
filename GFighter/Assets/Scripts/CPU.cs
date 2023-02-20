using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    public float health;
    CharacterController CPUController;
    Animator CPUAnimator;
    float verticalVelocity;
    Vector3 MovementDirection;
    public bool CPUmaxDistanceReached;
    // Start is called before the first frame update
    void Start()
    {
        health = 1f;
        CPUController = GetComponent<CharacterController>();
        CPUAnimator = GetComponent<Animator>();
        verticalVelocity = -1.98f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()   {
       

        CPUController.Move(MovementDirection + (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime));
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "P_Foot")
        {
            CPUAnimator.SetTrigger("HitMiddle");
            Debug.Log("GotHit");
        }
        if (other.tag == "P_Hand")
        {
            CPUAnimator.SetTrigger("HitMiddle");
            Debug.Log("GotHit");
        }

    }

}
