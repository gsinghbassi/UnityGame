using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockCheck : MonoBehaviour
{
    public string CheckCode;
    public bool CodeTrue;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if(other.name== CheckCode)
        {
            CodeTrue = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == CheckCode)
        {
            CodeTrue = false;
        }
    }

}
