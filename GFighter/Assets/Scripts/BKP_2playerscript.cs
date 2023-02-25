using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BKP_2playerscript: MonoBehaviour
{
    public float health;
    CharacterController CPUController;
    Animator CPUAnimator;
    float CPUSpeed;
    float walkSpeed;
    float runSpeed;
    float verticalVelocity;
    bool runActive;
    bool MovementAllowed;
    Vector3 MovementDirection;
    public bool CPUmaxDistanceReached;    
    public bool CPUReady;
    bool damageonce;
    float damagedelay;

    float KeyPressTimeCheck;
    float KeyPressDelayInterval;
    public float SendDamage;
    bool runningkick;


    // Start is called before the first frame update
    void Start()
    {
        damagedelay = 0f;
        damageonce = false;
        runningkick = false;
        SendDamage = 0f;
        CPUReady = false;
        MovementAllowed = true;
        runActive = false;
        health = 1f;
        walkSpeed = 1f;
        runSpeed = 6f;
        CPUSpeed = walkSpeed;
        CPUController = GetComponent<CharacterController>();
        CPUAnimator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name != "2-CharacterSelect")
        {
            CPUAnimator.SetBool("GameMode", true);
        }
        else if (SceneManager.GetActiveScene().name == "2-CharacterSelect")
        {
            CPUAnimator.SetBool("GameMode", false);
        }


        verticalVelocity = -1.98f;
        KeyPressDelayInterval = 1f;
        KeyPressTimeCheck = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (health<=0)
        { 
            Die();
        }
        if (CPUReady)
        {
            InputsCPU();
        }


        
       
    }
    void InputsCPU()
    {
        if (Input.GetKey(KeyCode.J) && MovementAllowed)
        {

            if (Time.time > KeyPressTimeCheck && !runActive)
            {
                Movement("Left");
                CPUAnimator.SetBool("WalkForward", true);
            }
            else if (Time.time < KeyPressTimeCheck)
            {
                runActive = true;
            }
            if (runActive)
            {
                runningkick = true;
                Movement("Run");
                CPUAnimator.SetBool("Run", true);
            }

        }


        if (Input.GetKeyUp(KeyCode.J))
        {
            KeyPressTimeCheck = Time.time + KeyPressDelayInterval;
            CPUAnimator.SetBool("WalkForward", false);
            if (runActive)
            {
                runActive = false;
                runningkick = false;
                CPUAnimator.SetBool("Run", false);
            }
        }



        if (Input.GetKey(KeyCode.L) && !CPUmaxDistanceReached && MovementAllowed)
        {
            Movement("Right");
            CPUAnimator.SetBool("WalkBack", true);
            CPUAnimator.SetLayerWeight(1, 1f);

        }
        if (Input.GetKeyUp(KeyCode.L) || CPUmaxDistanceReached)
        {
            CPUAnimator.SetBool("WalkBack", false);
            CPUAnimator.SetLayerWeight(1, 0f);
        }

        if (Input.GetKeyDown("n") && MovementAllowed)  //punch
        {
            MovementAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Punch");
            SendDamage = 0.08f;
            StartCoroutine(MovementAllowReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }

        if (Input.GetKeyDown("m") && MovementAllowed)  //kick
        {

            MovementAllowed = false;
            CPUAnimator.SetTrigger("Kick");
            if (!runningkick)
            {
                SendDamage = 0.15f;
            }
            if (runningkick)
            {
                SendDamage = 0.06f;
            }
            StartCoroutine(MovementAllowReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }
    }


        void Movement(string G_Input)
    {
        if (G_Input == "Right")
        {
            CPUSpeed = walkSpeed;
            MovementDirection = Vector3.back * (CPUSpeed * Time.deltaTime);
        }

        if (G_Input == "Run")
        {
            CPUSpeed = runSpeed;
            MovementDirection = Vector3.forward * (CPUSpeed * Time.deltaTime);
        }

        if (G_Input == "Left")
        {
            CPUSpeed = walkSpeed;
            MovementDirection = Vector3.forward * (CPUSpeed * Time.deltaTime);
        }
        CPUController.Move(MovementDirection + (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime));
    }


    void Gravity()
    {
        //PlayerController.Move((new Vector3(0.0f,verticalVelocity,0.0f)*Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "P_Foot" && G_GameManager.PlayerSendDamage!=0f && !damageonce)
        {
            damageonce = true;
            CPUAnimator.SetTrigger("HitMiddle");
            Debug.Log("GotHit by" + other.name);
            health = health - G_GameManager.PlayerSendDamage;
            
        }
        if (other.tag == "P_Hand" && G_GameManager.PlayerSendDamage!=0f && !damageonce &&Time.time>damagedelay)
        {
            damageonce = true;
            CPUAnimator.SetTrigger("HitTop");
            Debug.Log("GotHit by" + other.name);
            health = health - G_GameManager.PlayerSendDamage;
            damagedelay = Time.time + 1f;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "P_Foot" || other.tag == "P_Hand")
        {
            damageonce = false;
        }
    }

        public void Die()
    {
        CPUAnimator.SetTrigger("Die");
    }
    IEnumerator MovementAllowReset(float G_Time)
    {
        yield return new WaitForSeconds(G_Time);
        MovementAllowed = true;
    }

    IEnumerator SendDamageReset(float G_Time)
    {
        yield return new WaitForSeconds(G_Time);
        SendDamage = 0f;
    }

}
