using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Player
{
    public class MovementState : BaseState<PlayerC>
    {
        public MovementState(PlayerC controller) : base(controller) { }

        #region --- Overrides ---

        public override void EnterState()
        {
            Debug.Log("In Movement state!");
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
