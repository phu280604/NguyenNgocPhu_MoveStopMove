using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Player
{
    public class AttackState : BaseState<PlayerC>
    {
        public AttackState(PlayerC controller, EState keyState) : base(controller, keyState) 
        {
            if (controller.StateM is PlayerStateM stateM)
                _stateM = stateM;

            if (_spawnWeapon == null)
                _spawnWeapon = new Timer(_controller.StatsM.StatsSO.triggeredAnimAttack, OnSpawnWeapon);
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            _isStopping = _controller.StatsM.CurrentSpeed <= 0f;

            ChangeAnim();

            _acceleration = _controller.StatsM.StatsSO.acceleration;
            _maxSpeed = _controller.StatsM.MaxSpeed;

            _spawnWeapon.OnReset();
        }

        public override void UpdateState()
        {
            OnStopMove();
            OnRotateTowardsTarget();

            _spawnWeapon.OnCountDown(Time.deltaTime);

            SetDelayAttack();
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

        private void OnSpawnWeapon()
        {
            Vector3 lookPos = _controller.StateM.Target.position - _controller.transform.position;

            WeaponC newWeapon = PoolManager.Instance.Spawn<WeaponC>(
                _controller.StateM.WeaponType,
                _controller.StateM.SpawnWeaponPos.position,
                Quaternion.LookRotation(lookPos)
            );

            if (newWeapon == null) return;

            newWeapon.StateM.TargetPos = _controller.StateM.Target.position;
            newWeapon.StateM.TargetTag = ETag.Bot;
            newWeapon.OnInit(_controller, (coins) => {

                if (_controller.StateM.IsDead || coins == -1) return;

                _controller.GetCoinDrop(coins);

                _controller.StatsM.OnUpdateStatsAfterEliminating();

                _controller.Subject.NotifyObservers(EEventKey.Map, EMapKey.RespawnBot);

                _controller.Subject.NotifyObservers(EEventKey.Map, EMapKey.NextLevel);

                _stateM.IsChangeRange = true;
            });
        }

        private void OnRotateTowardsTarget()
        {
            Vector3 direction = (_stateM.Target.position - _controller.transform.position).normalized;
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

        private void SetDelayAttack()
        {
            if (_controller.Animator.GetBool(EAnimParams.IsDelayAttack.ToString())) {
                ChangeAnim();

                _spawnWeapon.OnReset();
            }
        }

        // Change animation based on stopping state.
        private void ChangeAnim()
        {
            if (!_controller.gameObject.activeSelf) return;

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

        private Timer _spawnWeapon;

        private PlayerStateM _stateM;

        #endregion
    }
}
