using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Player
{
    public class IdleState : BaseState<PlayerC>
    {
        public IdleState(PlayerC controller) : base(controller) { }

        #region --- Overrides ---

        public override void EnterState()
        {
            Debug.Log("In Idle state!");
        }

        public override void UpdateState()
        {

        }

        public override void ExitState()
        {

        }

        #endregion
    }
}
