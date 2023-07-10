using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDart : MonoBehaviour
{
    
    public bool DartChecker;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Darts")
        {
            
            DartChecker = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        DartChecker = false;
    }
}
