using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float health;
    CharacterController PlayerController;
    Animator PlayerAnimator;
    public float PlayerSpeed;
    float verticalVelocity;   
    Vector3 MovementDirection;
    public bool PlayermaxDistanceReached;
    

    // Start is called before the first frame update
    void Start()
    {
        health = 1f;
           PlayerController = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name != "2-CharacterSelect")
        {
            PlayerAnimator.SetBool("GameMode", true);
        }
        else if (SceneManager.GetActiveScene().name == "2-CharacterSelect")
        {
            PlayerAnimator.SetBool("GameMode", false);
        }

        verticalVelocity = -1.98f;
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Movement("Right");
            PlayerAnimator.SetBool("WalkForward", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            
            PlayerAnimator.SetBool("WalkForward", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !PlayermaxDistanceReached)
        {
            Movement("Left");
            PlayerAnimator.SetBool("WalkBack", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || PlayermaxDistanceReached)
        {            
            PlayerAnimator.SetBool("WalkBack", false);
        }
        if (Input.GetKeyDown("z"))
        {
            PlayerAnimator.SetTrigger("Punch");
        }
        if (Input.GetKeyDown("x"))
        {
            PlayerAnimator.SetTrigger("Kick");
        }

    }

    void Movement(string G_Input)
    {
        if (G_Input == "Right")
        {
            MovementDirection = Vector3.back * (PlayerSpeed * Time.deltaTime);
           
        }
        if (G_Input == "Left")
        {
            MovementDirection = Vector3.forward* (PlayerSpeed * Time.deltaTime);
        }
        PlayerController.Move(MovementDirection + (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime));
    }

    void Gravity()
    {
       //PlayerController.Move((new Vector3(0.0f,verticalVelocity,0.0f)*Time.deltaTime));
    }


    public void Idle()
    { 
    }
    public void Block()
    { 
    }
    public void Punch() 
    {
    }
    public void Kick()
    {
    }
    public void Walk() 
    { 
    }
    public void SpecialAttack()
    {
    }
    public void Die()
    {

    }

}
