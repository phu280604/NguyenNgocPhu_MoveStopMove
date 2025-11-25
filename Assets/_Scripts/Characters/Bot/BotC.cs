using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BotC : CharacterC
{
    #region --- Overrides ---

    #region -- Methods --
    public override void OnInit()
    {
        // Config state model.
        if(_stateM.LayerTargets == null)
            _stateM.LayerTargets = new string[] {
                ELayer.Player.ToString(),
                ELayer.Bot.ToString()
            };

        // Config state machine.
        if(_stateManager == null)
            _stateManager = new BotStateManager(this);

        _curState = _stateManager.GetState(EState.Idle);
        _curState.EnterState();

        _keyState = EState.Idle;

        OnDeselected();
        
    }
    protected override void CheckState()
    {
        if (!_stateM.IsMoving)
            _keyState = EState.Idle;
        else if (_stateM.IsMoving)
            _keyState = EState.Movement;

        //if (_stateM.Target != null && _statsM.VisualData != null)
        //    _keyState = EState.Attack;

        if (_curState.KeyState != _keyState)
            _stateManager.SwitchState(_keyState, ref _curState);
    }
    protected override void OnBuildRangeAttack()
    {
        if (_stateM.IsChangeRange)
        {
            _stateM.AtkRangePos.localScale = Vector3.one * (_statsM.CurrentRangeAttack - 0.5f);
            _stateM.IsChangeRange = false;
        }

        _physicH.BuildAttackRange(
            _statsM.CurrentRangeAttack,
            _stateM.AtkRangePos.position,
            _stateM.LayerTargets,
            this
        );
    }
    #endregion

    #region -- Properties --
    public override ICharacterStateM StateM => _stateM;
    #endregion

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

    private void FixedUpdate()
    {
        OnBuildRangeAttack();
    }

    private void OnEnable()
    {
        _stateM.NavMesh.avoidancePriority = Random.Range(50, 100);
    }

    private void OnDisable()
    {
        _keyState = EState.Idle;
        _stateManager.SwitchState(_keyState, ref _curState);

        OnDeselected();
    }

    #endregion

    #region --- Methods ---

    #region -- Components --
    public void OnSelected()
    {
        _stateM.SelectionRing.enabled = true;
    }

    public void OnDeselected()
    {
        _stateM.SelectionRing.enabled = false;
    }
    #endregion

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
    public Subject<ELevelEventKey, EMapKey> MapSubject { get; set; }

    #endregion

    #region -- Fields ---

    #region -- Components --
    [Header("Model components")]
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
