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


    // Start is called before the first frame update
    void Start()
    {
        SelectedPlayerCharacter = PlayerPrefs.GetInt("SelectedPlayerCharacter");
        SelectedCPUCharacter = PlayerPrefs.GetInt("SelectedCpuCharacter");
        PlayerCharacter = Characters[SelectedPlayerCharacter];
        CPUCharacter = Characters[SelectedCPUCharacter];
        PlayerPicture.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(("Assets/CharacterPhotos/"+PlayerCharacter.name+".png"), typeof(Sprite));
        CPUPicture.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(("Assets/CharacterPhotos/"+ CPUCharacter.name+".png"), typeof(Sprite));
        InstancePlayerCharacter=Instantiate(PlayerCharacter, PlayerStartPosition.position, PlayerStartPosition.rotation);
        InstanceCPUCharacter=Instantiate(CPUCharacter, CPUStartPosition.position, CPUStartPosition.rotation);
        InstancePlayerCharacter.GetComponent<CPU>().enabled = false;
        InstancePlayerCharacter.GetComponent<Player>().enabled = true;
        InstanceCPUCharacter.GetComponent<Player>().enabled = false;
        InstanceCPUCharacter.GetComponent<CPU>().enabled = true;
        InstancePlayerCharacter.GetComponent<Animator>().SetBool("GameMode", true);
        InstanceCPUCharacter.GetComponent<Animator>().SetBool("GameMode", true);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth.fillAmount = InstancePlayerCharacter.GetComponent<Player>().health;
        CPUHealth.fillAmount = InstanceCPUCharacter.GetComponent<CPU>().health;

    }
}
