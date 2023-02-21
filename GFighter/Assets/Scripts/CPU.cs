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
    float CPUSpeed=1f;
    public bool CPUReady;
    public float damage;



    // Start is called before the first frame update
    void Start()
    {
        CPUReady = false;
        health = 1f;
        CPUController = GetComponent<CharacterController>();
        CPUAnimator = GetComponent<Animator>();
        verticalVelocity = -1.98f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health<=0)
        { 
            Die();
        }


        if (Input.GetKey(KeyCode.D) && !CPUmaxDistanceReached)
        {
            Movement("Right");
            CPUAnimator.SetBool("WalkBack", true);
        }
        if (Input.GetKeyUp(KeyCode.D) || CPUmaxDistanceReached)
        {

            CPUAnimator.SetBool("WalkBack", false);
        }
        if (Input.GetKey(KeyCode.A) )
        {
            Movement("Left");
            CPUAnimator.SetBool("WalkForward", true);
        }
        if (Input.GetKeyUp(KeyCode.A) )
        {
            CPUAnimator.SetBool("WalkForward", false);
        }
       
    }
    void Movement(string G_Input)
    {
        if (G_Input == "Right")
        {
            MovementDirection = Vector3.back * (CPUSpeed * Time.deltaTime);

        }
        if (G_Input == "Left")
        {
            MovementDirection = Vector3.forward * (CPUSpeed * Time.deltaTime);
        }
        CPUController.Move(MovementDirection + (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime));
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "P_Foot")
        {
            CPUAnimator.SetTrigger("HitMiddle");
            Debug.Log("GotHit");
            health = health - damage;
        }
        if (other.tag == "P_Hand")
        {
            CPUAnimator.SetTrigger("HitTop");
            Debug.Log("GotHit");
            health = health - damage;
        }

    }
    public void Die()
    {
        CPUAnimator.SetTrigger("Die");
    }
}
