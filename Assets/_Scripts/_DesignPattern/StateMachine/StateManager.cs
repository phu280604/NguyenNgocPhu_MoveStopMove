using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<TCtrl>
{
    public StateManager(TCtrl controller)
    {
        _controller = controller;
        OnInit();
    }

    #region --- Methods ---

    protected abstract void OnInit();

    protected void AddState(EState keyState, BaseState<TCtrl> state)
    {
        // check if the state pool already contains the key.
        if (_statePool.ContainsKey(keyState)) return;

        // add state to the pool.
        _statePool.Add(keyState, state);
    }

    public BaseState<TCtrl> GetState(EState keyState)
    {
        // check if the state pool not contains the key.
        if (!_statePool.ContainsKey(keyState)) return null;

        // return state from the pool.
        return _statePool[keyState];
    }

    public void SwitchState(EState keyState, ref BaseState<TCtrl> curState)
    {
        // check if the state pool not contains the key.
        if (!_statePool.ContainsKey(keyState)) return;

        // exit current state.
        curState?.ExitState();

        // enter new state.
        _statePool[keyState].EnterState();

        // return new state.
        curState = _statePool[keyState];
    }

    #endregion

    #region --- Fields ---

    protected Dictionary<EState, BaseState<TCtrl>> _statePool = new Dictionary<EState, BaseState<TCtrl>>();

    protected TCtrl _controller;

    #endregion
}
