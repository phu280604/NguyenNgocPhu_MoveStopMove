using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<TCtrl>
{
    public BaseState(TCtrl controller, EState keyState)
    {
        _controller = controller;
        KeyState = keyState;
    }

    #region --- Methods ---

    public abstract void EnterState(); 
    public abstract void UpdateState();
    public abstract void ExitState();

    #endregion

    #region --- Properties ---

    public EState KeyState { get; private set; }

    #endregion

    #region --- Fields ---

    protected TCtrl _controller;

    #endregion
}
