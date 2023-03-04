using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    Animator EffectAnimator;
    // Start is called before the first frame update
    void Start()
    {
        EffectAnimator=GetComponent<Animator>();
        Destroy(gameObject,EffectAnimator.GetCurrentAnimatorClipInfo(0).Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
