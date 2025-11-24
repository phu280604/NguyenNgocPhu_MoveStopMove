using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Bot
{
    public class AttackState : BaseState<BotC>
    {
        public AttackState(BotC controller, EState keyState) : base(controller, keyState) 
        {
            if (controller.StateM is BotStateM stateM)
            {
                _stateM = stateM;

                _delayAttack = new Timer(controller.StatsM.StatsSO.attackDelay, () =>
                {
                    ChangeAnim();
                    _spawnWeapon.OnReset();
                });

                _spawnWeapon = new Timer(_controller.StatsM.StatsSO.triggeredAnimAttack, OnSpawnWeapon);
            }
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            _stateM.NavMesh.ResetPath();

            ChangeAnim();

            _spawnWeapon.OnReset();
            _delayAttack.OnReset();
        }

        public override void UpdateState()
        {
            OnRotateTowardsTarget();

            _spawnWeapon.OnCountDown(Time.deltaTime);

            _delayAttack.OnCountDown(Time.deltaTime, true);
        }

        public override void ExitState()
        {
            _controller.StateM.IsDelayAttack = true;
            _controller.StateM.Target = null;
        }

        #endregion

        #region --- Methods ---

        private void OnSpawnWeapon()
        {
            Vector3 lookPos = _controller.StateM.Target.position - _controller.transform.position;

            WeaponC newWeapon = PoolManager.Instance.Spawn<WeaponC>(
                _controller.StateM.WeaponType,
                _controller.StateM.SpawnWeaponPos.position,
                Quaternion.LookRotation(lookPos)
            );

            newWeapon.StateM.TargetPos = _controller.StateM.Target.position;
            newWeapon.StateM.TargetTag = ETag.Bot;
            newWeapon.OnInit(_controller, () => {
                _controller.StatsM.OnUpdateStatsAfterEliminating();

                _controller.MapSubject.NotifyObservers(ELevelEventKey.Map, EMapKey.RespawnBot);

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

        // Change animation based on stopping state.
        private void ChangeAnim()
        {
            if (!_controller.gameObject.activeSelf) return;

            _controller.Animator.Play(EAnim.Attack.ToString());
        }

        #endregion

        #region --- Fields ---

        private Timer _spawnWeapon;

        private Timer _delayAttack;

        private BotStateM _stateM;

        #endregion
    }
}
