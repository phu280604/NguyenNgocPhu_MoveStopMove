using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Bot
{
    public class IdleState : BaseState<BotC>
    {
        public IdleState(BotC controller, EState keyState) : base(controller, keyState) 
        { 
            if(controller.StateM is BotStateM stateM)
                _stateM = stateM;

            _timer = new Timer(_controller.StatsM.WaitingTime, () =>
            {
                _stateM.IsMoving = true;
            });
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            _timer.OnReset();

            ChangeAnim();
        }

        public override void UpdateState()
        {
            _timer.OnCountDown(Time.deltaTime);
        }

        public override void ExitState()
        {

        }

        #endregion

        #region --- Methods ---

        // Change animation based on stopping state.
        private void ChangeAnim()
        {
            _controller.Animator.Play(EAnim.Idle.ToString());
        }

        #endregion

        #region --- Fields ---

        private float _acceleration;
        private float _maxSpeed;

        private Timer _timer;

        private BotStateM _stateM;

        #endregion
    }
}
