using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BotC : CharacterC
{
    #region --- Overrides ---

    public override void OnInit()
    {
        _stateManager = new BotStateManager(this);

        _curState = _stateManager.GetState(EState.Idle);
        _curState.EnterState();

        _keyState = EState.Idle;
    }
    protected override void CheckState()
    {
        if (!_stateM.IsMoving)
            _keyState = EState.Idle;
        else if (_stateM.IsMoving)
            _keyState = EState.Movement;

        if (_curState.KeyState != _keyState)
            _stateManager.SwitchState(_keyState, ref _curState);
    }

    public override ICharacterStateM StateM => _stateM;

    #endregion

    #region --- Unity methods ---

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        OnActionState();
    }

    private void OnEnable()
    {
        _stateM.NavMesh.avoidancePriority = Random.Range(50, 100);
    }

    private void OnDisable()
    {
        _stateM.NavMesh.ResetPath();
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

    #endregion

    #region --- Properties ---

    public BotStatsM StatsM => _statsM;

    #endregion

    #region -- Fields ---

    #region -- Components --
    [SerializeField] private BotStateM _stateM;
    [SerializeField] private BotStatsM _statsM;
    #endregion

    #region -- State machine --
    private BaseState<BotC> _curState;
    private EState _keyState;
    private BotStateManager _stateManager;
    #endregion

    #endregion
}
