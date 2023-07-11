using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIceChange : MonoBehaviour
{
    AudioSource DiceAudioController;
    Animator DiceController;
    public AudioClip DiceRoll;
    public AudioClip ClueRevealSound;

    private void Start()
    {
        DiceAudioController = GetComponent<AudioSource>();
        DiceController = GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        DiceController.SetTrigger("Change");
        DiceAudioController.PlayOneShot(DiceRoll);
    }
    public void ClueReveal()
    {
        DiceAudioController.PlayOneShot(ClueRevealSound);
    }
}
