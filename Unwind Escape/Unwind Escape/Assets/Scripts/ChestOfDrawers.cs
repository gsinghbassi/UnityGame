using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOfDrawers : MonoBehaviour
{
    public bool DrawersOpen;
    Animator    ChestController;
    // Start is called before the first frame update
    void Start()
    {
        DrawersOpen = false;
        ChestController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DrawersOpen == false)
        {
            ChestController.SetBool("DrawerOpen", false);
        }
        else if (DrawersOpen == true)
        {
            ChestController.SetBool("DrawerOpen", true);
        }
    }
}
