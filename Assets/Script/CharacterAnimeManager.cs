using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimeManager : MonoBehaviour
{
    public Animator anim;

    public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool isBool = false)
    {
        //anim.applyRootMotion = isInteracting;
        //anim.SetBool(AnimHash.INTERACTING, isInteracting);
        //anim.CrossFade(targetAnim, 0.2f);
        if(!isBool)anim.Play(targetAnim);else anim.SetBool(targetAnim, isInteracting);
    }
}
