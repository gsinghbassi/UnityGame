using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;


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
    bool damageonce;
    float damagedelay;
    float KeyPressTimeCheck;
    float KeyPressDelayInterval;
    public float SendDamage;   //KeepPublic   
    bool runningkick;
    TextMeshProUGUI HitsText;
    int hits;
    float HitTimeCheck;
    GameObject Effect1; //KeepPublic   
    GameObject Effect2; //KeepPublic   
    public Material PlayerMaterial; //Keep this public
    Color DamageColor = new Color(0.3584906f, 0.0253649f, 0.0253649f);
    public bool lose; //KeepPublic   
    public bool win; //KeepPublic   



    //Audio Controls
    AudioSource PlayerAudioController;
    AudioClip GS_Punch;
    AudioClip GS_Kick;
    AudioClip GS_Damage;
    AudioClip GS_Die;
    AudioClip GS_Jump;
    AudioClip GS_Win;
    AudioClip GS_YouWin;
    AudioClip GS_Footsteps;
    bool WinSoundActivated;


    // Start is called before the first frame update
    void Start()
    {
        PlayerAudioController = GetComponent<AudioSource>();
        lose = false;
        win = false;
        HitsText = transform.Find("Hits").transform.Find("HitsText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(HitsTextReset(0f));
        damagedelay = 0f;
        damageonce = false;
        runningkick = false;
        SendDamage = 0f;
        PlayerReady = false;
        MovementAllowed = true;
        runActive = false;
        health = 1f;
        walkSpeed = 1f;
        runSpeed = 4f;
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
        KeyPressTimeCheck = Time.time;
        Effect1 = (GameObject)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Effects/Effect1.prefab"), typeof(GameObject));
        Effect2 = (GameObject)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Effects/Effect2.prefab"), typeof(GameObject));
        PlayerMaterial.color = Color.white;

        GS_Punch = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Punch.mp3"), typeof(AudioClip));
        GS_Kick = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Kick.mp3"), typeof(AudioClip));
        GS_Damage = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Damage.mp3"), typeof(AudioClip));
        GS_Jump = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Jump.mp3"), typeof(AudioClip));
        GS_Die = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Die.mp3"), typeof(AudioClip));
        GS_Win = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/Win.mp3"), typeof(AudioClip));
        GS_YouWin = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/YouWin.mp3"), typeof(AudioClip));
        GS_Footsteps = (AudioClip)AssetDatabase.LoadAssetAtPath(("Assets/Audio/FootSteps.mp3"), typeof(AudioClip));

    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (health <= 0)
        {
            Die();
        }
        if (PlayerReady&&!lose &&!win)
        {
            InputsPlayer();
        }

        if (win)
        {
            Win();
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
                runningkick = true;
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
                runningkick = false;
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

        if(Input.GetKeyDown(KeyCode.Space) && MovementAllowed)
        {
            MovementAllowed = false;
            PlayerAnimator.SetLayerWeight(1, 0f);
            PlayerAnimator.SetTrigger("Jump");
            PlayerAudioController.PlayOneShot(GS_Jump);
            StartCoroutine(MovementAllowReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
            
        }

        if (Input.GetKeyDown("z") && MovementAllowed)  //punch
        {
            MovementAllowed = false;
            PlayerAnimator.SetLayerWeight(1, 0f);
            PlayerAnimator.SetTrigger("Punch");
            PlayerAudioController.PlayOneShot(GS_Punch);
            SendDamage = 0.08f;
            StartCoroutine(MovementAllowReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
        }

        if (Input.GetKeyDown("x") && MovementAllowed)  //kick
        {

            MovementAllowed = false;
            PlayerAnimator.SetTrigger("Kick");
            PlayerAudioController.PlayOneShot(GS_Kick);
            if (!runningkick)
            {
                SendDamage = 0.15f;
            }
            if (runningkick)
            {
                SendDamage = 0.06f;
            }
            StartCoroutine(MovementAllowReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
            StartCoroutine(SendDamageReset(PlayerAnimator.GetCurrentAnimatorClipInfo(0).Length));
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
            MovementDirection = Vector3.forward * (PlayerSpeed * Time.deltaTime);

        }
        PlayerController.Move(MovementDirection + (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime));
    }

    void Gravity()
    {
        //PlayerController.Move((new Vector3(0.0f,verticalVelocity,0.0f)*Time.deltaTime));
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "P_Foot" && G_GameManager.CPUSendDamage != 0f && !damageonce)
        {
            damageonce = true;
            PlayerAnimator.SetTrigger("HitMiddle");
            health = health - G_GameManager.CPUSendDamage;
            Instantiate(Effect1, other.transform.position, Effect1.transform.rotation);
            
        }
        if (other.tag == "P_Hand" && G_GameManager.CPUSendDamage != 0f && !damageonce && Time.time > damagedelay)
        {
            damageonce = true;
            PlayerAnimator.SetTrigger("HitTop");
            health = health - G_GameManager.CPUSendDamage;
            damagedelay = Time.time + 1f;
            Instantiate(Effect2, other.transform.position, Effect2.transform.rotation);
            
        }
        if (damageonce)
        {
            PlayerAudioController.PlayOneShot(GS_Damage);
            StartCoroutine(PlayerDamageColorChange());
            hits++;
            HitsText.text = hits + " Hits";
            HitTimeCheck = Time.time + 1f;
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
        PlayerAnimator.SetLayerWeight(1, 0f);
        PlayerAnimator.SetTrigger("Die");
        PlayerAudioController.PlayOneShot(GS_Die);
        lose = true;
    }

    
    public void Win()
    {
        ClearTriggers();
        PlayVictorySound();
        PlayerAnimator.SetTrigger("Win");
        PlayerAnimator.SetLayerWeight(1, 0f);
        StartCoroutine(HitsTextReset(0f));
    }

    void PlayVictorySound()
    {
        if (!WinSoundActivated)
        {
            PlayerAudioController.PlayOneShot(GS_Win);
            PlayerAudioController.PlayOneShot(GS_YouWin);
            WinSoundActivated = true;
        }
    }

    void ClearTriggers()
    {

        PlayerAnimator.ResetTrigger("HitTop");
        PlayerAnimator.ResetTrigger("HitMiddle");
        PlayerAnimator.ResetTrigger("Kick");
        PlayerAnimator.ResetTrigger("Punch");
        PlayerAnimator.ResetTrigger("Jump");

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

    IEnumerator HitsTextReset(float G_Time)
    {
        yield return new WaitForSeconds(G_Time);
        HitsText.text = "";
        hits = 0;
    }
    IEnumerator PlayerDamageColorChange()
    {
        PlayerMaterial.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        PlayerMaterial.color = Color.white;
    }
   
    
}
