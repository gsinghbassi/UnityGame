using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class G_GameManager : MonoBehaviour
{   
    int SelectedPlayerCharacter;    
    int SelectedCPUCharacter;
    public GameObject[] Characters;    
    GameObject PlayerCharacter;    
    GameObject CPUCharacter;
    GameObject InstancePlayerCharacter;
    GameObject InstanceCPUCharacter;

    public Transform PlayerStartPosition;
    public Transform CPUStartPosition;
    public Image PlayerPicture;    
    public Image CPUPicture;
    public Image PlayerHealth;
    public Image CPUHealth;
    public static float PlayerSendDamage;
    public static float CPUSendDamage;


    CameraControls CameraController;
    float PlayerPositionZ;
    float CPUPositionZ;
    public float PlayerCPUpositionDifference;
    float maxDistancebetweenPlayers = 8f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayerandCPUReady());
        CameraController = GameObject.Find("Camera").GetComponent<CameraControls>();
        SelectedPlayerCharacter = PlayerPrefs.GetInt("SelectedPlayerCharacter");
        SelectedCPUCharacter = PlayerPrefs.GetInt("SelectedCpuCharacter");
        PlayerCharacter = Characters[SelectedPlayerCharacter];
        CPUCharacter = Characters[SelectedCPUCharacter];
        PlayerPicture.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(("Assets/CharacterPhotos/"+PlayerCharacter.name+".png"), typeof(Sprite));
        CPUPicture.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(("Assets/CharacterPhotos/"+ CPUCharacter.name+".png"), typeof(Sprite));
        InstancePlayerCharacter=Instantiate(PlayerCharacter, PlayerStartPosition.position, PlayerStartPosition.rotation);
        InstanceCPUCharacter=Instantiate(CPUCharacter, CPUStartPosition.position, CPUStartPosition.rotation);
        InstanceCPUCharacter.transform.localScale = new Vector3(-1f, InstanceCPUCharacter.transform.localScale.y, InstanceCPUCharacter.transform.localScale.z);
        InstancePlayerCharacter.GetComponent<Player>().enabled = true;
        InstanceCPUCharacter.GetComponent<CPU>().enabled = true;
        InstancePlayerCharacter.GetComponent<Animator>().SetBool("GameMode", true);
        InstanceCPUCharacter.GetComponent<Animator>().SetBool("GameMode", true);
        //InstancePlayerCharacter.GetComponent<CPU>().enabled = false;
        Destroy(InstancePlayerCharacter.GetComponent<CPU>());        
        //InstanceCPUCharacter.GetComponent<Player>().enabled = false;
        Destroy(InstanceCPUCharacter.GetComponent<Player>());
        CameraController.PlayerCharacter = InstancePlayerCharacter;
        CameraController.CPUCharacter = InstanceCPUCharacter;
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth.fillAmount = InstancePlayerCharacter.GetComponent<Player>().health;
        CPUHealth.fillAmount = InstanceCPUCharacter.GetComponent<CPU>().health;
        PlayerDistanceCheck();
        PlayerSendDamage = InstancePlayerCharacter.GetComponent<Player>().SendDamage; 
        CPUSendDamage = InstanceCPUCharacter.GetComponent<CPU>().SendDamage;      

    }

    void PlayerDistanceCheck()
    {
        PlayerPositionZ = InstancePlayerCharacter.transform.position.z;
        CPUPositionZ = InstanceCPUCharacter.transform.position.z;
        PlayerCPUpositionDifference = PlayerPositionZ - CPUPositionZ;
        if (PlayerCPUpositionDifference < maxDistancebetweenPlayers)
        {
            InstancePlayerCharacter.GetComponent<Player>().PlayermaxDistanceReached = false;
            InstanceCPUCharacter.GetComponent<CPU>().CPUmaxDistanceReached = false;
        }
        else if (PlayerCPUpositionDifference >= maxDistancebetweenPlayers)
        {
            InstancePlayerCharacter.GetComponent<Player>().PlayermaxDistanceReached = true;
            InstanceCPUCharacter.GetComponent<CPU>().CPUmaxDistanceReached = true;
        }
    }


    IEnumerator PlayerandCPUReady()
    {
        yield return new WaitForSeconds(5.0f);
        InstancePlayerCharacter.GetComponent<Player>().PlayerReady = true;
        InstanceCPUCharacter.GetComponent<CPU>().CPUReady = true;
    }
}
