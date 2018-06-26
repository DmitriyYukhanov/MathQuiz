using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AniIdle : AbstractAniState
{
    public AniIdle() { _currentAniState = enumAniState.Idle; }
    public AniIdle(enumAniState aniState) { _currentAniState = aniState; }

    /// <summary>
    /// Update
    /// </summary>    
    public override void Update(float time, AniStateController controller)
    {        

        if (Input.GetKeyDown(KeyCode.A))
        {
            controller.SetAniState(SetState(enumAniState.Attack1));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            controller.SetAniState(SetState(enumAniState.Attack2));
        }        
    }

    /// <summary>
    /// 상태변경
    /// </summary>  
    protected override AbstractAniState SetState(enumAniState state)
    {        
        if (state != currentAniState)
            return new AniAttack(state);

        return this;
    }
}
