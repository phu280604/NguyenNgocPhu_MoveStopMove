using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Player
{
    public class DeadState : BaseState<PlayerC>
    {
        public DeadState(PlayerC controller, EState keyState) : base(controller, keyState) 
        {
            if (controller.StateM is PlayerStateM stateM)
                _stateM = stateM;
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            OnChangeAnimation();
        }

        public override void UpdateState()
        {
            OnHandleAfterAnimationDone();
        }

        public override void ExitState()
        {

        }

        #endregion

        #region --- Methods ---

        private void OnChangeAnimation()
        {
            if(_stateM.IsDead)
                _controller.Animator.SetBool(EAnimParams.IsDead.ToString(), _stateM.IsDead);
        }

        private void OnHandleAfterAnimationDone()
        {
            if(_stateM.IsDead && !_controller.Animator.GetBool(EAnimParams.IsDead.ToString()))
                _controller.OnDespawn();
        }

        #endregion

        #region --- Fields ---

        private PlayerStateM _stateM;

        #endregion
    }
}
