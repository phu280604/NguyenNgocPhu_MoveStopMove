using FSM.Bot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStateManager : StateManager<BotC>
{
    public BotStateManager(BotC controller) : base(controller) { }

    #region --- Overrides ---

    protected override void OnInit()
    {
        AddState(EState.Idle, new IdleState(_controller, EState.Idle));
        AddState(EState.Movement, new MovementState(_controller, EState.Movement));
        //AddState(EState.Attack, new AttackState(_controller));
    }

    #endregion
}
