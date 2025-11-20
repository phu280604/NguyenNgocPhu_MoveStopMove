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
                _stateM = stateM;
        }

        #region --- Overrides ---

        public override void EnterState()
        {
            _stateM.NavMesh.ResetPath();

            ChangeAnim();

            if (_spawnWeapon == null)
                _spawnWeapon = new Timer(_controller.StatsM.StatsSO.triggeredAnimAttack, OnSpawnWeapon);
            else
                _spawnWeapon.OnReset();
        }

        public override void UpdateState()
        {
            OnRotateTowardsTarget();

            _spawnWeapon.OnCountDown(Time.deltaTime);
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
           // BotStateM stateM = _stateM;

            Vector3 lookPos = _controller.StateM.Target.position - _controller.transform.position;

            WeaponC newWeapon = PoolManager.Instance.Spawn<WeaponC>(
                _controller.StateM.WeaponType,
                _controller.StateM.SpawnWeaponPos.position,
                Quaternion.LookRotation(lookPos)
            );

            newWeapon.StateM.TargetPos = _controller.StateM.Target.position;
            newWeapon.StateM.TargetTag = ETag.Bot;
            newWeapon.OnInit(_controller);
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
            _controller.Animator.Play(EAnim.Attack.ToString());
        }

        #endregion

        #region --- Fields ---

        private Timer _spawnWeapon;

        private BotStateM _stateM;

        #endregion
    }
}
