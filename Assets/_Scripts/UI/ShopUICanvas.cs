using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICanvas : UICanvas
{
    #region --- Overrides ---

    protected override void OnInit()
    {
        for(int i = 1; i < _tabItems.Count; i++)
        {
            _tabItems[i].OnInit(false);
        }

        _tabItems[0].OnInit(true);
    }

    public override void CloseDirectly()
    {
        PoolManager.Instance.Despawn(EPoolType.VisualObject);

        UIManager.Instance.OpenUI<MenuUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Menu);

        base.CloseDirectly();  
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private List<TabItem> _tabItems = new List<TabItem>();

    #endregion
}
