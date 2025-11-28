using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerC : CharacterC
{
    #region --- Overrides ---

    #region -- Methods --
    public override void OnInit()
    {
        // Configure state model.
        _stateM.LayerTargets = new string[] {
            ELayer.Bot.ToString()
        };

        // Configure state machine.
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

        if(_stateM.IsDead)
            _keyState = EState.Dead;

        if (_curState.KeyState != _keyState)
            _stateManager.SwitchState(_keyState, ref _curState);
    }

    protected override void OnBuildRangeAttack()
    {
        // Change scale range attack.
        if (_stateM.IsChangeRange)
        {
            _stateM.AtkRangePos.localScale = Vector3.one * (_statsM.CurrentRangeAttack - 0.5f);
            _stateM.IsChangeRange = false;
        }

        // Build range attack.
        _physicH.BuildAttackRange(
            _statsM.CurrentRangeAttack,
            _stateM.AtkRangePos.position,
            _stateM.LayerTargets,
            this
        );
    }

    public override void OnDead()
    {
        _stateM.OnHideCollider();

        base.OnDead(); 
    }
    #endregion

    #region -- Properties --
    public override ICharacterStateM StateM => _stateM;
    #endregion

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
    }

    private void FixedUpdate()
    {
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

    #region -- Handle physics --
    private void OnActionHandler()
    {
        _controlH.GetInput((newDir) =>
        {
            _stateM.Direction = newDir;

            if(newDir != Vector3.zero)
                _stateM.LastestDirection = newDir;
        });
    }

    public void GetCoinDrop(int coins)
    {
        GameplayManager.Instance.SetCoin(coins);
        if(UIManager.Instance.BackTopUI is GamePlayUICanvas ui)
        {
            ui.Subject.NotifyObservers(EUIGamePlayKey.TextCoins, GameplayManager.Instance.Coins);
        }
    }
    #endregion

    #region -- Handle reset --

    public void OnHandleAfterDead()
    {
        GameManager.Instance.ChangeState(EGameStates.Losing);

        _stateM.OnReset();
    }

    #endregion

    #endregion

    #region --- Properties ---

    public PlayerStatsM StatsM => _statsM;

    public Subject<EEventKey, object> Subject => GameplayManager.Instance.GameplaySubject;

    #endregion

    #region --- Fields ---

    #region -- Components --
    [Header("Handler")]
    [SerializeField] private PlayerInputH _controlH;

    [Header("Model")]
    [SerializeField] private PlayerStatsM _statsM;
    [SerializeField] private PlayerStateM _stateM;

    #endregion

    #region -- State machine --
    private PlayerStateManager _stateManager;
    private BaseState<PlayerC> _curState;
    private EState _keyState;
    #endregion

    #endregion
}
