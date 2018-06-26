using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AniAttack : AbstractAniState
{
    /// <summary>
    /// 생성자
    /// </summary>    
    public AniAttack(enumAniState aniState) { _currentAniState = aniState; }

    /// <summary>
    /// Update
    /// </summary>    
    public override void Update(float time, AniStateController controller)
    {
        updateTime += time;

        if (updateTime > 0.8f)
        {
            controller.SetAniState(SetState(_reserveState));
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            switch (currentAniState)
            {
                case enumAniState.Attack1:
                    _reserveState = enumAniState.Attack2;
                    break;
                case enumAniState.Attack2:
                    _reserveState = enumAniState.Attack3;
                    break;
            }
        }
    }

    /// <summary>
    /// 상태변경
    /// </summary>    
    protected override AbstractAniState SetState(enumAniState state)
    {
        if (state == enumAniState.Idle)
            return new AniIdle();
        else if (state != currentAniState)
            Reset(state);

        return this;
    }

    /// <summary>
    /// 리셋 데이터
    /// </summary>    
    private void Reset(enumAniState state)
    {
        updateTime = 0f;
        _reserveState = enumAniState.Idle;
        _currentAniState = state;
    }
}
