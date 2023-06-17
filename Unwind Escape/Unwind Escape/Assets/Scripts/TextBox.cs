using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{

    TextMeshProUGUI TextController;
    public GameObject ButlerPicture;
    public GameObject DialoguePicture;
    
    // Start is called before the first frame update
    void Start()
    {
        TextController = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TextController.text=="")
        {
            ButlerPicture.SetActive(false);
            DialoguePicture.SetActive(false);
        }
        else if (TextController.text != "")
        {
            ButlerPicture.SetActive(true);
            DialoguePicture.SetActive(true);
        }

    }
}
