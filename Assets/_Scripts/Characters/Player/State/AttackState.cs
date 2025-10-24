using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Player
{
    public class AttackState : BaseState<PlayerC>
    {
        public AttackState(PlayerC controller) : base(controller) 
        {
            if (controller.StateM is PlayerStateM stateM)
                _stateM = stateM;
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            _isStopping = _controller.StatsM.CurrentSpeed <= 0f;

            ChangeAnim();

            _acceleration = _controller.StatsM.StatsSO.acceleration;
            _maxSpeed = _controller.StatsM.StatsSO.maxMovementSpeed;
        }

        public override void UpdateState()
        {
            OnStopMove();
            OnRotateTowardsTarget();
        }

        public override void ExitState()
        {

        }

        #endregion

        #region --- Methods ---

        // Decelerate to stop the player smoothly.
        private void OnStopMove()
        {
            if (_isStopping)
                return;


            if (_controller.StatsM.CurrentSpeed > 0f)
            {
                _controller.StatsM.CurrentSpeed -= _acceleration * Time.deltaTime;
                _controller.Animator.speed = Mathf.Abs(_controller.StatsM.CurrentSpeed / _maxSpeed);

                Vector3 direction = _stateM.LastestDirection.normalized;

                _controller.transform.position = Vector3.MoveTowards(
                    _controller.transform.position,
                    _controller.transform.position + direction,
                    _controller.StatsM.CurrentSpeed * Time.deltaTime
                );
            }
            else
            {
                _controller.StatsM.CurrentSpeed = 0f;

                _isStopping = true;

                ChangeAnim();
            }
        }

        private void OnAttack()
        {

        }

        private void OnRotateTowardsTarget()
        {
            Vector3 direction = (_stateM.TransTarget.position - _controller.transform.position).normalized;
            direction.y = 0f;

            if (direction == Vector3.zero)
                direction = _controller.transform.forward;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            _controller.transform.rotation = Quaternion.Slerp(
                _controller.transform.rotation, 
                lookRotation, 
                Time.deltaTime * _controller.StatsM.StatsSO.rotationSpeed
            );
        }

        // Change animation based on stopping state.
        private void ChangeAnim()
        {
            if (!_isStopping)
                _controller.Animator.Play(EAnim.Run.ToString());
            else if(_isStopping)
            {
                _controller.Animator.Play(EAnim.Attack.ToString());
                _controller.Animator.speed = _controller.StatsM.StatsSO.maxAnimSpeed;
            }
        }

        #endregion

        #region --- Fields ---

        private float _acceleration;
        private float _maxSpeed;

        private bool _isStopping;

        private PlayerStateM _stateM;

        #endregion
    }
}
