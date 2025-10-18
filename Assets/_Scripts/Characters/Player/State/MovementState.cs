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
            _isMoving = _controller.StatsM.CurrentSpeed > 0f;

            ChangeAnim();

            _acceleration = _controller.StatsM.StatsSO.acceleration;
            _maxSpeed = _controller.StatsM.StatsSO.maxMovementSpeed;
        }

        public override void UpdateState()
        {
            OnRotate();
            OnMove();
        }

        public override void ExitState()
        {

        }

        #endregion

        #region --- Methods ---

        // Accelerate to move the player smoothly.
        private void OnMove()
        {
            if(_controller.StatsM.CurrentSpeed < _maxSpeed)
            {
                _controller.StatsM.CurrentSpeed += _acceleration * Time.deltaTime;
                _controller.Animator.speed = _controller.StatsM.CurrentSpeed / _maxSpeed;
            }
            else
            {
                _controller.StatsM.CurrentSpeed = _maxSpeed;
                _isMoving = true;
                ChangeAnim();
            }

            Vector3 direction = _controller.StateM.Direction.normalized;

            _controller.transform.position = Vector3.Lerp(
                _controller.transform.position,
                _controller.transform.position + direction,
                _controller.StatsM.CurrentSpeed * Time.deltaTime
            );
        }

        // Rotate the player to face the movement direction.
        private void OnRotate()
        {
            Vector3 direction = _controller.StateM.Direction;

            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

            _controller.transform.rotation = Quaternion.Slerp(
                _controller.transform.rotation, 
                lookRotation, 
                Time.deltaTime * _controller.StatsM.StatsSO.rotationSpeed
            );
        }

        // Change animation based on movement state.
        private void ChangeAnim()
        {
            _controller.Animator.Play(EAnim.Run.ToString());

            if (_isMoving)
                _controller.Animator.speed = _controller.StatsM.StatsSO.maxAnimSpeed;
        }

        #endregion

        #region --- Fields ---

        private float _acceleration;
        private float _maxSpeed;

        private bool _isMoving;

        #endregion
    }
}
