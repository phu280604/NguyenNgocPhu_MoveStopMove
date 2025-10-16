using FSM.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StateManager<PlayerC>
{
    public PlayerStateManager(PlayerC controller) : base(controller) { }

    #region --- Overrides ---

    protected override void OnInit()
    {
        AddState(EState.Idle, new IdleState(_controller));
        AddState(EState.Movement, new MovementState(_controller));
    }

    #endregion
}
