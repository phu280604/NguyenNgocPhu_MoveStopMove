using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUICanvas : UICanvas
{
    #region --- Overrides ---

    public override void CloseDirectly()
    {
        PoolManager.Instance.CollectAll();

        UIManager.Instance.OpenUI<MenuUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Menu);

        base.CloseDirectly();
    }

    #endregion

    #region --- Fields ---



    #endregion
}
