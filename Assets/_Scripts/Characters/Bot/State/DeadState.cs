using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Bot
{
    public class DeadState : BaseState<BotC>
    {
        public DeadState(BotC controller, EState keyState) : base(controller, keyState) 
        {
            if (controller.StateM is BotStateM stateM)
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
            _controller.Animator.speed = _baseAnimSpeed;

            if(_stateM.IsDead)
                _controller.Animator.SetBool(EAnimParams.IsDead.ToString(), _stateM.IsDead);
        }

        private void OnHandleAfterAnimationDone()
        {
            if(_stateM.IsDead && !_controller.Animator.GetBool(EAnimParams.IsDead.ToString()))
            {
                _controller.OnHandleAfterDead();

                _controller.OnDespawn();
            }
        }

        #endregion

        #region --- Fields ---

        private float _baseAnimSpeed = 0.7f;

        private BotStateM _stateM;

        #endregion
    }
}
