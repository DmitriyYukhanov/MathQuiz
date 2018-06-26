using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum enumAniState
{
    Idle,
    Attack1,
    Attack2,
    Attack3,
}

public abstract class AbstractAniState
{
    protected float updateTime;
    protected enumAniState _reserveState = enumAniState.Idle;

    protected enumAniState _currentAniState = enumAniState.Idle;
    public enumAniState currentAniState { get { return _currentAniState; } }

    public abstract void Update(float time, AniStateController controller);
    protected abstract AbstractAniState SetState(enumAniState state);
}
