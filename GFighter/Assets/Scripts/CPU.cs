using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CPU : MonoBehaviour
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
    G_GameManager GameManagerReference;

    //CPU AI
    float PlayerCPUDistance;
    bool CPULeft;
    bool CPURight;
    bool CPUKick;
    bool CPUPunch;
    bool CPURun;
    int runORwalkCheck;
    bool keepwalking;
    bool runORwalk;
    float CPUWait;
    bool gotoPlayer;
    bool CPUWaitUpdated;
    float PlayerRange;
    bool isPlayerinRange;




    // Start is called before the first frame update
    void Start()
    {
        gotoPlayer = false;        
        CPUWaitUpdated = false;
        CPUWait = Time.time + Random.Range(7f, 9f);
        PlayerRange = 0.4f;
        if (GameObject.Find("GameManager")!=null)
        {
            GameManagerReference = GameObject.Find("GameManager").GetComponent<G_GameManager>();
        }

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
        if (CPUReady&&health>0)
        {
            InputsCPU();
            CPUAI();
        }   
        
       
    }

    void CPUAI()
    {
        
        PlayerCPUDistance = GameManagerReference.PlayerCPUpositionDifference;

        if (Time.time > CPUWait &&gotoPlayer)
        {
            if (PlayerCPUDistance > PlayerRange)
            {
                CPULeft = true;
            }
        }
        if (PlayerCPUDistance <= PlayerRange)
        {
            CPULeft = false;
            gotoPlayer = false;
            //CPUWait = Time.time;
            CPUWaitUpdated = false;
         }

        if(PlayerCPUDistance>Random.Range((PlayerRange+2f),7f) && !CPUWaitUpdated)
        {
            gotoPlayer = true;
            CPUWait=Time.time + Random.Range(1f, 3f);
            CPUWaitUpdated=true;
        }

        if(!gotoPlayer&& PlayerCPUDistance > PlayerRange && Time.time>CPUWait+4f)
        {
            gotoPlayer = true;
            CPUWait = Time.time + Random.Range(1f, 3f);
            CPUWaitUpdated = true;
        }

        if (health < 0.5f)
        {
            gotoPlayer = false;
            CPURight = true;
        }
        
    }

    

    void InputsCPU()
    {
        

        if (CPULeft && MovementAllowed)
        {
            if ((!runActive&& PlayerCPUDistance<=4f) ||keepwalking)
            {
                Movement("Left");
                CPUAnimator.SetBool("WalkForward", true);
            }

            if(PlayerCPUDistance > 4f && !runORwalk)
            {
                runORwalkCheck = Random.Range(0, 2);   
                runORwalk = true;
            }


            if (runORwalkCheck == 0)
            {
                keepwalking = true;
            }
            if (runORwalkCheck == 1)
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


        if (!CPULeft)
        {
            CPUAnimator.SetBool("WalkForward", false);
            runORwalkCheck = 0;
            runORwalk = false;
            keepwalking = false;
            if (runActive)
            {
                runActive = false;
                runningkick = false;
                CPUAnimator.SetBool("Run", false);
                
            }
            

        }



        if (CPURight && !CPUmaxDistanceReached && MovementAllowed)
        {
            Movement("Right");
            CPUAnimator.SetBool("WalkBack", true);
            CPUAnimator.SetLayerWeight(1, 1f);

        }
        if (!CPURight || CPUmaxDistanceReached)
        {
            CPUAnimator.SetBool("WalkBack", false);
            CPUAnimator.SetLayerWeight(1, 0f);
        }

        if (CPUPunch && MovementAllowed)  //punch
        {
            MovementAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Punch");
            SendDamage = 0.08f;
            StartCoroutine(MovementAllowReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }

        if (CPUKick && MovementAllowed)  //kick
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
        CPUAnimator.SetLayerWeight(1, 0f);
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
