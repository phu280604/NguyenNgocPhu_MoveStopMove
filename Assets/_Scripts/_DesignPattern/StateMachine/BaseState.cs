using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<TCtrl>
{
    public BaseState(TCtrl controller)
    {
        _controller = controller;
    }

    #region --- Methods ---

    public abstract void EnterState(); 
    public abstract void UpdateState();
    public abstract void ExitState();

    #endregion

    #region --- Fields ---

    protected TCtrl _controller;

    #endregion
}
