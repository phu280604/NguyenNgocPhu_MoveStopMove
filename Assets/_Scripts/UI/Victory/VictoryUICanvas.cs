using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUICanvas : UICanvas
{
    #region --- Overrides ---

    public override void Open()
    {
        base.Open();

        OnOpenScreen();
    }

    #endregion

    #region --- Methods ---

    public void OnOpenScreen()
    {
        PoolManager.Instance.CollectAll();
    }

    public void OnNextLevel()
    {
        GameManager.Instance.ChangeState(EGameStates.GamePlay);

        UIManager.Instance.OpenUI<GamePlayUICanvas>(); 
    }

    public void OnBacktoHone()
    {
        GameManager.Instance.ChangeState(EGameStates.Menu);

        UIManager.Instance.OpenUI<MenuUICanvas>();
    }

    #endregion
}
