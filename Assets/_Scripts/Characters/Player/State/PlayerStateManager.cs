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
        AddState(EState.Idle, new IdleState(_controller, EState.Idle));
        AddState(EState.Movement, new MovementState(_controller, EState.Movement));
        AddState(EState.Attack, new AttackState(_controller, EState.Attack));
    }

    #endregion
}
