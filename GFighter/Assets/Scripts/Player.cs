using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float health;   //KeepPublic
    CharacterController PlayerController;
    Animator PlayerAnimator;
    float PlayerSpeed;
    float walkSpeed;
    float runSpeed;
    float verticalVelocity;
    bool runActive;
    bool MovementAllowed;
    Vector3 MovementDirection;
    public bool PlayermaxDistanceReached;   //KeepPublic
    public bool PlayerReady;   //KeepPublic


    float KeyPressTimeCheck;
    float KeyPressDelayInterval;
    

    // Start is called before the first frame update
    void Start()
    {
        
        PlayerReady = false;
        MovementAllowed = true;
        runActive = false;
        health = 1f;
        walkSpeed =1f;
        runSpeed =6f;
        PlayerSpeed = walkSpeed;
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
        KeyPressDelayInterval = 1f;
        KeyPressTimeCheck=Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (health <= 0)
        {
            Die();
        }
        if (PlayerReady)
        {
            InputsPlayer();
        }
    }

   

    void InputsPlayer()
    {
        if (Input.GetKey(KeyCode.RightArrow) && MovementAllowed)
        {

            if (Time.time > KeyPressTimeCheck && !runActive)
            {
                Movement("Right");
                PlayerAnimator.SetBool("WalkForward", true);
            }
            else if (Time.time < KeyPressTimeCheck)
            {
                runActive = true;
            }
            if (runActive)
            {
                Movement("Run");
                PlayerAnimator.SetBool("Run", true);
            }

        }


        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            KeyPressTimeCheck = Time.time + KeyPressDelayInterval;
            PlayerAnimator.SetBool("WalkForward", false);
            if (runActive)
            {
                runActive = false;
                PlayerAnimator.SetBool("Run", false);
            }
        }



        if (Input.GetKey(KeyCode.LeftArrow) && !PlayermaxDistanceReached && MovementAllowed)
        {
            Movement("Left");
            PlayerAnimator.SetBool("WalkBack", true);            
            PlayerAnimator.SetLayerWeight(1, 1f);
            
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || PlayermaxDistanceReached)
        {
            PlayerAnimator.SetBool("WalkBack", false);
            PlayerAnimator.SetLayerWeight(1, 0f);
        }

        if (Input.GetKeyDown("z") && MovementAllowed)
        {
            MovementAllowed = false;
            PlayerAnimator.SetLayerWeight(1, 0f);
            PlayerAnimator.SetTrigger("Punch");
            StartCoroutine(MovementAllowReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }
        if (Input.GetKeyDown("x") && MovementAllowed)
        {

            MovementAllowed = false;            
            PlayerAnimator.SetTrigger("Kick");
            StartCoroutine(MovementAllowReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }
    }




    void Movement(string G_Input)
    {
        if (G_Input == "Right")
        {
            PlayerSpeed = walkSpeed;
            MovementDirection = Vector3.back * (PlayerSpeed * Time.deltaTime);           
           
        }
        if (G_Input == "Run")
        {
            PlayerSpeed = runSpeed;
            MovementDirection = Vector3.back * (PlayerSpeed * Time.deltaTime);
        }
        if (G_Input == "Left")
        {
            PlayerSpeed = walkSpeed;
            MovementDirection = Vector3.forward* (PlayerSpeed * Time.deltaTime);
            
        }
        PlayerController.Move(MovementDirection + (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime));
    }

    void Gravity()
    {
       //PlayerController.Move((new Vector3(0.0f,verticalVelocity,0.0f)*Time.deltaTime));
    }

    
    
    
    public void Die()
    {
        PlayerAnimator.SetTrigger("Die");
    }


    IEnumerator MovementAllowReset(float G_Time)
    {
        Debug.Log(G_Time);
        yield return new WaitForSeconds(G_Time);
        MovementAllowed = true;
    }

}
