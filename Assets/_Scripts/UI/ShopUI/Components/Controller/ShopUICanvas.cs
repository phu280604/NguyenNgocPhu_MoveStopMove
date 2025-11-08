using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICanvas : UICanvas
{
    #region --- Overrides ---

    public override void CloseDirectly()
    {
        PoolManager.Instance.Despawn(EPoolType.VisualObject);

        UIManager.Instance.OpenUI<MenuUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Menu);

        base.CloseDirectly();
    }

    #endregion

    #region --- Unity methods ---

    private void OnEnable()
    {
        for (int i = 1; i < _tabItems.Count; i++)
            _tabItems[i].OnInit(false);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private List<TabColorC> _tabItems = new List<TabColorC>();

    #endregion
}
