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
    G_GameManager GameManagerReference;


    //CPU AI
    float PlayerCPUDistance;
    public bool CPULeft;
    public bool CPURight;
    bool CPUKick;
    bool CPURunKick;
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
    int AttackSelector;
    int AttackCount;
    int KicksCount;
    int PunchCount;
    bool AttackAllowed;   
    bool stopretreating;
    


    // Start is called before the first frame update
    void Start()
    {
       
       gotoPlayer = false;
        stopretreating = false;
        AttackAllowed =true;
        CPUWaitUpdated = false;
        CPUWait = Time.time + Random.Range(7f, 9f);
        PlayerRange = 0.4f;
        if (GameObject.Find("GameManager")!=null)
        {
            GameManagerReference = GameObject.Find("GameManager").GetComponent<G_GameManager>();
        }

        damagedelay = 0f;
        damageonce = false;
        SendDamage = 0f;
        CPUReady = false;
        MovementAllowed = true;
        runActive = false;
        health = 1f;
        walkSpeed = 1f;
        runSpeed = 4f;
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

        if (health < 0.5f && !stopretreating)
        {
            gotoPlayer = false;
            CPURight = true;
            CPULeft = false;
            StartCoroutine(Stopretreat());
        }
        


        if(PlayerCPUDistance<PlayerRange+0.5f)
        {
            isPlayerinRange = true;
        }
        else if (PlayerCPUDistance > PlayerRange + 0.5f)
        {
            isPlayerinRange = false;
            
        }

        if (isPlayerinRange &&AttackAllowed)
        {
           if(AttackCount>2)
            {
                if (KicksCount>=2)
                {
                    AttackSelector = 1;
                    KicksCount = 0;
                }
                else if (PunchCount >= 2)
                {
                    AttackSelector = 2;
                    PunchCount = 0;
                }
                AttackCount = 0;
            }
            else
            {
                AttackSelector = Random.Range(1, 3);
            }
  
            
            if (AttackSelector == 1)
            {
                CPUPunch = true;
                PunchCount++;
            }
            else if (AttackSelector == 2)
            {
                CPUKick = true;
                KicksCount++;
            }
            AttackCount++;
            
        }

        else if (!isPlayerinRange)
        {
            AttackCount = 0;
            KicksCount = 0;
            PunchCount = 0;
            AttackSelector = 0;          
            CPUPunch = false;
            CPUKick = false;
            CPUAnimator.ResetTrigger("Punch");
            CPUAnimator.ResetTrigger("Kick");
        }

    }

    
    

    void InputsCPU()
    {
        

        if (CPULeft && MovementAllowed)
        {
            if ((!runActive&& PlayerCPUDistance<=4f) ||keepwalking)
            {
                Movement("Left");
                CPUAnimator.SetLayerWeight(1, 0f);
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
                Movement("Run");
                CPUAnimator.SetBool("Run", true);
                if (PlayerCPUDistance < PlayerRange + 0.8f)
                {
                    CPURunKick = true; 
                }
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

        if (CPUPunch && MovementAllowed) 
        {
            MovementAllowed = false;
            AttackAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Punch");
            SendDamage = 0.08f;
            StartCoroutine(AttackReset("CPUPunch",CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        
        }

        if (CPUKick && MovementAllowed) 
        {

            MovementAllowed = false;
            AttackAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Kick");                           
            SendDamage = 0.15f;          
            StartCoroutine(AttackReset("CPUKick", CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }
       if(CPURunKick)
        {
            AttackAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Kick");
            SendDamage = 0.06f;
            StartCoroutine(AttackReset("CPURunKick", CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
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
            health = health - G_GameManager.PlayerSendDamage;
            
        }
        if (other.tag == "P_Hand" && G_GameManager.PlayerSendDamage!=0f && !damageonce &&Time.time>damagedelay)
        {
            damageonce = true;
            CPUAnimator.SetTrigger("HitTop");
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
    
    

    IEnumerator AttackReset (string G_String,float G_Time)
    {
        yield return new WaitForSeconds(G_Time);
        if(G_String=="CPUPunch")
        {
            CPUPunch = false;
        }
        if (G_String == "CPUKick")
        {
            CPUKick = false;
        }
        if(G_String=="CPURunKick")
        {
            CPURunKick = false;
        }
        AttackAllowed = true;
        MovementAllowed = true;
    }

    IEnumerator SendDamageReset(float G_Time)
    {
        yield return new WaitForSeconds(G_Time);
        SendDamage = 0f;
    }

    IEnumerator Stopretreat()
    {
        yield return new WaitForSeconds(2.5f);
        stopretreating = true;
        CPURight = false;
     //   yield return new WaitForSeconds(5f);
      //  stopretreating = false;
    }    
}
