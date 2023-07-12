using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    bool Rotate = false;
    Animator PCOntroller;
    private void Update()
    {
        PCOntroller = GetComponent<Animator>();
        if(Rotate)
        {
            PCOntroller.SetBool("Rotate 0",true);
        }    
    }
    public void Paintingrot()
    {
        Rotate = true; 
    }
}
