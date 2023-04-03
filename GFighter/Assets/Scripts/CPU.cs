using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

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
    public bool damageonce;
    float damagedelay;
    float KeyPressTimeCheck;
    
    public float SendDamage;    
    G_GameManager GameManagerReference;
    TextMeshProUGUI HitsText;
    int hits;    
    float HitTimeCheck;
    GameObject Effect1;
    GameObject Effect2;
    GameObject Effect3;
    public Material CPUMaterial; //Keep this public
    


    //CPU AI
    float PlayerCPUDistance;
    bool CPULeft;
    bool CPURight;
    bool CPUKick;
    bool CPURunKick;
    bool CPUPunch;
    bool CPURun;
    bool CPUJump;
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
    public bool AttackPause;
    bool stopretreating;
    public bool lose; //KeepPublic   
    public bool win; //KeepPublic   
    bool JumpAttackAllowed;
    bool JumpAttackinProgress;
    int JumpRandom;

    //BlockandStaminaControls
    bool block;
    public float stamina;
    float refilldelay;
    float damagemultiplier;
    


    //Audio Controls
    AudioSource CPUAudioController;
    AudioClip GS_Punch;
    AudioClip GS_Kick;
    AudioClip GS_Damage;
    AudioClip GS_Block;
    AudioClip GS_Jump;
    AudioClip GS_Die;   
    AudioClip GS_Lose;   
    AudioClip GS_YouLose;   
    AudioClip GS_Footsteps;
    bool WinSoundActivated;

    // Start is called before the first frame update
    void Start()
    {
        refilldelay = 0;
        damagemultiplier = 3f;
        block = false;
        JumpRandom = 0;
        AttackPause = false;
        JumpAttackAllowed = false;
        CPUAudioController = GetComponent<AudioSource>();
        lose = false;
        win = false;
        HitsText = transform.Find("Hits").transform.Find("HitsText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(HitsTextReset(0f));
        transform.Find("Hits").transform.Find("HitsText").Rotate(0, 180, 0);
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
        stamina = 1f;
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
        KeyPressTimeCheck = Time.time;
        Effect1 = (GameObject)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Effects/Effect1.prefab"), typeof(GameObject));
        Effect2 = (GameObject)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Effects/Effect2.prefab"), typeof(GameObject));
        Effect3 = (GameObject)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Effects/Effect3.prefab"), typeof(GameObject));
        CPUMaterial.color = Color.white;
        GS_Punch = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Punch.mp3"), typeof(AudioClip));
        GS_Kick = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Kick.mp3"), typeof(AudioClip));
        GS_Damage = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Damage.mp3"), typeof(AudioClip));
        GS_Block = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Block.mp3"), typeof(AudioClip));
        GS_Jump = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Jump.mp3"), typeof(AudioClip));
        GS_Die = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Die.mp3"), typeof(AudioClip));
        GS_Lose = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Lose.mp3"), typeof(AudioClip));
        GS_YouLose = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/YouLose.mp3"), typeof(AudioClip));
        GS_Footsteps = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/FootSteps.mp3"), typeof(AudioClip));
        GetAllChildrenRecursive(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (health<=0)
        { 
            Die();
        }
        if (CPUReady&&!lose&&!win)
        {
            InputsCPU();
            CPUAI();
        }   
        
        if(win)
        {
            Win();
        }

        CheckPlayerHurt();
        
        if(Time.time>HitTimeCheck+1f)
        {
            StartCoroutine(HitsTextReset(0.2f));
        }
        while (stamina < 1 && Time.time > refilldelay)
        {
            stamina += 0.2f;
            refilldelay = Time.time + 3f;
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


        if (health < 0.5f && !stopretreating ||hits>=2)
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


        

        if (isPlayerinRange &&AttackAllowed && !AttackPause)
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
                JumpRandom = Random.Range(0, 2);
                if (JumpRandom == 1)
                {
                    CPUJump = true;
                }
                else if (JumpRandom != 1)
                {
                    CPUJump = false;
                }
                CPUPunch = true;
                PunchCount++;
                StartCoroutine(CPUJumpReset());
            }
            else if (AttackSelector == 2)
            {
                JumpRandom = Random.Range(0, 2);
                if (JumpRandom == 1)
                {
                    CPUJump = true;
                }
                else if (JumpRandom != 1)
                {
                    CPUJump = false;
                }
                CPUKick = true;
                KicksCount++;
                StartCoroutine(CPUJumpReset());
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


        if(CPUJump && MovementAllowed && stamina > 0)
        {
            JumpAttackAllowed = true;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Jump");
            CPUAudioController.PlayOneShot(GS_Jump);
            stamina = stamina - 0.35f;            
            StartCoroutine(CPUJumpAttackReset());


        }

        if (CPUPunch && JumpAttackAllowed)
        {
            JumpAttackAllowed = false;
            JumpAttackinProgress = true;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("JumpPunch");
            CPUAudioController.PlayOneShot(GS_Punch);
            SendDamage = 0.12f;
            StartCoroutine(CPUJumpinProgressReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }
        if (CPUKick && JumpAttackAllowed)
        {
            JumpAttackAllowed = false;
            JumpAttackinProgress = true;
            CPUAnimator.SetTrigger("JumpKick");
            CPUAudioController.PlayOneShot(GS_Kick);
            SendDamage = 0.18f;
            StartCoroutine(CPUJumpinProgressReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }


        if (CPURight && !CPUmaxDistanceReached && MovementAllowed)
        {
            Movement("Right");
            CPUAnimator.SetBool("WalkBack", true);
            if (!JumpAttackinProgress && stamina > 0)
            {
                block = true;
                CPUAnimator.SetLayerWeight(1, 1f);
            }
            else if (JumpAttackinProgress)
            {
                CPUAnimator.SetLayerWeight(1, 0f);
            }

        }
        if (!CPURight || stamina <= 0)
        {
            block = false;            
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetBool("WalkBack", false);
        }

        if (CPUmaxDistanceReached)
        {
            CPUAnimator.SetBool("WalkBack", false);           
        }

        if (CPUPunch && MovementAllowed ) 
        {
            MovementAllowed = false;
            AttackAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Punch");
            CPUAudioController.PlayOneShot(GS_Punch);
            SendDamage = 0.08f;
            StartCoroutine(AttackReset("CPUPunch",CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(CPUAnimator.GetCurrentAnimatorClipInfo(0).Length));
        
        }

        if (CPUKick && MovementAllowed ) 
        {

            MovementAllowed = false;
            AttackAllowed = false;
            CPUAnimator.SetLayerWeight(1, 0f);
            CPUAnimator.SetTrigger("Kick");
            CPUAudioController.PlayOneShot(GS_Kick);
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
        //CPUController.Move((new Vector3(0.0f,verticalVelocity,0.0f)*Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!win)
        {
            if (!block)
            {
                if (other.tag == "P_Foot" && G_GameManager.PlayerSendDamage != 0f && !damageonce)
                {
                    damageonce = true;
                    CPUAnimator.SetTrigger("HitMiddle");
                    health = health - G_GameManager.PlayerSendDamage;
                    Instantiate(Effect1, other.transform.position, Effect1.transform.rotation);
                }
                if (other.tag == "P_Hand" && G_GameManager.PlayerSendDamage != 0f && !damageonce && Time.time > damagedelay)
                {
                    damageonce = true;
                    CPUAnimator.SetTrigger("HitTop");
                    health = health - G_GameManager.PlayerSendDamage;
                    damagedelay = Time.time + 0.3f;
                    Instantiate(Effect2, other.transform.position, Effect2.transform.rotation);
                }
                if (damageonce)
                {
                    CPUAudioController.PlayOneShot(GS_Damage);
                    StartCoroutine(CPUDamageColorChange());
                    hits++;
                    HitsText.text = hits + " Hits";
                    HitTimeCheck = Time.time + 1f;
                }
            }
            else if (block)
            {
                if (other.tag == "P_Foot" && G_GameManager.PlayerSendDamage != 0f)
                {
                    stamina = stamina - G_GameManager.PlayerSendDamage * damagemultiplier;
                    Instantiate(Effect3, other.transform.position, Effect3.transform.rotation);
                    CPUAudioController.PlayOneShot(GS_Block);
                }
                if (other.tag == "P_Hand" && G_GameManager.PlayerSendDamage != 0f && Time.time > damagedelay)
                {
                    stamina = stamina - G_GameManager.PlayerSendDamage * damagemultiplier;
                    damagedelay = Time.time + 1f;
                    Instantiate(Effect3, other.transform.position, Effect3.transform.rotation);
                    CPUAudioController.PlayOneShot(GS_Block);
                }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "P_Foot" || other.tag == "P_Hand")
        {
            
            damageonce = false;
            if (Time.time > HitTimeCheck)
            {
                StartCoroutine(HitsTextReset(0.5f));
            }
        }
    }

    



    public void Die()
    {
        StartCoroutine(HitsTextReset(0f));
        CPUAnimator.SetLayerWeight(1, 0f);
        CPUAnimator.SetTrigger("Die");
        CPUAudioController.PlayOneShot(GS_Die);
        lose = true;
    }

    public void Win()
    {

        ClearTriggers();
        PlayVictorySound();      
        CPUAnimator.SetTrigger("Win");
        CPUAnimator.SetLayerWeight(1, 0f);
        StartCoroutine(HitsTextReset(0f));
    }

    void PlayVictorySound()
    {
        if (!WinSoundActivated)
        {
            CPUAudioController.PlayOneShot(GS_Lose);
            CPUAudioController.PlayOneShot(GS_YouLose);
            WinSoundActivated = true;
        }
    }

    void CheckPlayerHurt()
    {
        if (G_GameManager.playerishurt)
            AttackPause = true;
        else if (!G_GameManager.playerishurt)
            AttackPause = false;
    }



    void ClearTriggers()
    {

        CPUAnimator.ResetTrigger("HitTop");
        CPUAnimator.ResetTrigger("HitMiddle");
        CPUAnimator.ResetTrigger("Kick");
        CPUAnimator.ResetTrigger("Punch");
        CPUAnimator.ResetTrigger("Jump");

    }

    void GetAllChildrenRecursive(GameObject parent)
    {
        
        foreach (Transform child in parent.transform)
        {
                     
            if (child.tag == "P_Hand")
            {
                 child.tag = "CPU_Hand";
                
            }
            else if (child.tag == "P_Foot")
            {
                 child.tag = "CPU_Foot";
            }
                        
            GetAllChildrenRecursive(child.gameObject);
        }
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
    IEnumerator HitsTextReset(float G_Time)
    {
        yield return new WaitForSeconds(G_Time);
        HitsText.text = "";
        hits = 0;
    }

    IEnumerator CPUJumpReset()
    {
        yield return new WaitForSeconds(0.5f);
        CPUJump = false;
    }
    IEnumerator CPUJumpAttackReset()
    {
        yield return new WaitForSeconds(0.5f);
        JumpAttackAllowed = false;
    }
    IEnumerator CPUJumpinProgressReset(float G_Time)
    {

        yield return new WaitForSeconds(G_Time + 1f);
        JumpAttackinProgress = false;
    }


    IEnumerator CPUDamageColorChange()
    {
        CPUMaterial.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        CPUMaterial.color = Color.white;
    }
    
}
