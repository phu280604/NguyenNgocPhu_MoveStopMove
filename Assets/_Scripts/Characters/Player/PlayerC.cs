using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : CharacterC<PlayerStateM, PlayerStatsM>
{
    #region --- Overrides ---

    public override void OnInit()
    {
        base.OnInit();

        _stateManager = new PlayerStateManager(this);

        _curState = _stateManager.GetState(EState.Idle);
        _curState?.EnterState();

        _keyState = EState.Idle;
    }

    #endregion

    #region --- Unity Methods ---

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        OnActionState();
        OnActionHandler();
        OnBuildRangeAttack();
    }

    #endregion

    #region --- Methods ---

    #region -- State machine --
    private void CheckState()
    {
        if(_stateM.Direction == Vector3.zero)
            _keyState = EState.Idle;
        else if (_stateM.Direction != Vector3.zero)
            _keyState = EState.Movement;

        if(_stateM.Target != null && _keyState == EState.Idle)
            _keyState = EState.Attack;

        if (_curState != _stateManager.GetState(_keyState))
            _stateManager.SwitchState(_keyState, ref _curState);
    }

    private void OnActionState()
    {
        CheckState();
        _curState?.UpdateState();
    }
    #endregion

    #region -- Handler --
    private void OnActionHandler()
    {
        _controlH.GetInput((newDir) =>
        {
            _stateM.Direction = newDir;

            if(newDir != Vector3.zero)
                _stateM.LastestDirection = newDir;
        });
    }

    private void OnBuildRangeAttack()
    {
        _physicH.BuildAttackRange(_statsM.CurrentRangeAttack, (target) => { 
            _stateM.Target = target?.gameObject;
        });
    }
    #endregion

    #endregion

    #region --- Fields ---

    [SerializeField] private PlayerInputH _controlH;
    [SerializeField] private PlayerPhysicH _physicH;

    private PlayerStateManager _stateManager;
    private BaseState<PlayerC> _curState;
    private EState _keyState;

    #endregion
}
