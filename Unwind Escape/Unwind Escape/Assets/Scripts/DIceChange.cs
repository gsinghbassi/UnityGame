using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIceChange : MonoBehaviour
{
    Animator DiceController;
    private void Start()
    {
        DiceController = GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        DiceController.SetTrigger("Change");
    }
}
