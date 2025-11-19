using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerC : CharacterC
{
    #region --- Overrides ---

    public override void OnInit()
    {
        _stateManager = new PlayerStateManager(this);

        _curState = _stateManager.GetState(EState.Idle);
        _curState?.EnterState();

        _keyState = EState.Idle;
    }
    protected override void CheckState()
    {
        if (_stateM.Direction == Vector3.zero)
            _keyState = EState.Idle;
        else if (_stateM.Direction != Vector3.zero)
            _keyState = EState.Movement;

        if (_stateM.Target != null && _keyState == EState.Idle)
            _keyState = EState.Attack;

        if (_curState.KeyState != _keyState)
            _stateManager.SwitchState(_keyState, ref _curState);
    }

    public override ICharacterStateM StateM => _stateM;

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
        if (_isChangeRange)
        {
            _atkRangePos.localScale = Vector3.one * (_statsM.CurrentRangeAttack - 0.5f);
            _isChangeRange = false;
        }
        _physicH.BuildAttackRange(_statsM.CurrentRangeAttack, _atkRangePos.position , (target) => {
            _stateM.Target = target?.gameObject.transform;
        });
    }

    #endregion

    #endregion

    #region --- Properties ---

    public PlayerStatsM StatsM => _statsM;

    #endregion

    #region --- Fields ---

    #region -- Components --
    [Header("Handler")]
    [SerializeField] private PlayerInputH _controlH;
    [SerializeField] private PlayerPhysicH _physicH;

    [Header("Model")]
    [SerializeField] private PlayerStatsM _statsM;
    [SerializeField] private PlayerStateM _stateM;
    [SerializeField] private Transform _atkRangePos;
    private bool _isChangeRange = true;
    #endregion

    #region -- State machine --
    private PlayerStateManager _stateManager;
    private BaseState<PlayerC> _curState;
    private EState _keyState;
    #endregion

    #endregion
}
