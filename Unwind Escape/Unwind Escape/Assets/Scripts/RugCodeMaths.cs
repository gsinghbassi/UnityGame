using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugCodeMaths : MonoBehaviour
{
    public bool RugOut;
    Animator RugController;
    // Start is called before the first frame update
    void Start()
    {
        RugOut = false;
        RugController=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RugOut == false)
        {
            RugController.SetBool("RugMove", false);
        }
        else if (RugOut == true)
        {
            RugController.SetBool("RugMove", true);
        }
    }
}
