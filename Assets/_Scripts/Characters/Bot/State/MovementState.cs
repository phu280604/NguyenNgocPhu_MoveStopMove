using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace FSM.Bot
{
    public class MovementState : BaseState<BotC>
    {
        public MovementState(BotC controller, EState keyState) : base(controller, keyState) 
        {
            if (controller.StateM is BotStateM stateM)
                _stateM = stateM;
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            ChangeAnim();

            OnMove();
        }

        public override void UpdateState()
        {
            OnOutMove();
        }

        public override void ExitState()
        {
            _canMoving = false;
        }

        #endregion

        #region --- Methods ---

        // Accelerate to move the player smoothly.
        private void OnMove()
        {
            

            NavMeshHit hit;

            for(int i = 0; i < 10; i++)
            {
                Vector2 randomDirInCircle = Random.insideUnitCircle * _controller.StatsM.RangeMoving;

                Vector3 dir = new Vector3(randomDirInCircle.x, 0f, randomDirInCircle.y);
                _targetPos = _controller.transform.position + dir;

                if (!_canMoving && NavMesh.SamplePosition(_targetPos, out hit, 1f, NavMesh.AllAreas))
                {
                    _stateM.NavMesh.SetDestination(hit.position);

                    _canMoving = true;

                    break;
                }
            }

            _canMoving = true;

        }

        // Rotate the player to face the movement direction.
        private void OnOutMove()
        {
            if (!_canMoving) return;

            if(Vector3.Distance(_targetPos, _controller.transform.position) <= _controller.StatsM.DistanceStop)
                _stateM.IsMoving = false;

            DrawLineDistance();
        }

        private void DrawLineDistance()
        {
            Debug.DrawLine(_controller.transform.position, _targetPos, Color.magenta);
        }

        // Change animation based on movement state.
        private void ChangeAnim()
        {
            _controller.Animator.Play(EAnim.Run.ToString());

            //if (_isMoving)
            //    _controller.Animator.speed = _controller.StatsM.StatsSO.maxAnimSpeed;
        }

        #endregion

        #region --- Fields ---

        private float _acceleration;
        private float _maxSpeed;

        private Vector3 _targetPos;

        private bool _canMoving = false;

        private BotStateM _stateM;

        #endregion
    }
}
