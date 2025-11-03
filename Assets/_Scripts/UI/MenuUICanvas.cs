using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUICanvas : UICanvas
{
    #region --- Overrides ---

    protected override void OnInit()
    {
        base.OnInit();
    }

    #endregion

    #region --- Methods ---

    public void OpenShopUI()
    {
        UIManager.Instance.BackTopUI?.CloseDirectly();
        UIManager.Instance.OpenUI<ShopUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Shop);
    }

    public void OpenGamePlayUI()
    {
        UIManager.Instance.BackTopUI?.CloseDirectly();
        UIManager.Instance.OpenUI<GamePlayUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.GamePlay);
    }

    #endregion

    #region --- Fields ---



    #endregion
}
