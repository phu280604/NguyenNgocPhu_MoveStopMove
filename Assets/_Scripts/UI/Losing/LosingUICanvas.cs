using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingUICanvas : UICanvas
{
    #region --- Overrides ---

    protected override void OnInit()
    {
        base.OnInit();

        PoolManager.Instance.CollectAll();
    }

    #endregion
}
