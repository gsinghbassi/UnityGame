using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        // Call the function to get all children recursively
        GetAllChildrenRecursive(gameObject);
    }

    void GetAllChildrenRecursive(GameObject parent)
    {
        // Loop through all the children of the parent object
        foreach (Transform child in parent.transform)
        {
            // Do something with the child (add it to a list, for example)
            Debug.Log(child.name);
            if (child.tag == "P_Hand")
            {
                child.tag = "CPU_Hand";
            }
            else if (child.tag == "P_Foot")
            {
                child.tag = "CPU_Foot";
            }

            // Call the function recursively to get all children of the child object
            GetAllChildrenRecursive(child.gameObject);
        }
    }
}


