using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniStateController
{
    AbstractAniState aniState = new AniIdle(enumAniState.Idle);
    Animator anim;

    // 생성자
    public AniStateController(Animator animator)
    {
        anim = animator;
        int clipindex = 0;
        foreach(AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name.Contains("Attack"))
            {
                AnimationEvent evt = new AnimationEvent();
                evt.intParameter = clipindex++;
                evt.time = 0.2f;
                evt.functionName = "OnEvent";
                clip.AddEvent(evt);
            }
        }
    }

    public void SetAniState(AbstractAniState newAniState)
    {
        aniState = newAniState;
        Play(aniState.currentAniState.ToString());
    }

    public void Update(float time)
    {
        aniState.Update(time, this);
    }

    private void Play(string animationname)
    {
        anim.Play(animationname);
    }    

    public void OnEvent(int parameter)
    {
        //Debug.Log("show:" + parameter);
    }
}