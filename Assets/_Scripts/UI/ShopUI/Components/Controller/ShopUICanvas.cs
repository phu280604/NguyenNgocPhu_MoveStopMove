using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICanvas : UICanvas, IObserver<object>
{
    #region --- Overrides ---

    protected override void OnInit()
    {
        base.OnInit();

        _subject.AddObserver(EUIKey.Item, this);

        List<ShopButtonC> buttonItems = GameObject.FindObjectsByType<ShopButtonC>(FindObjectsSortMode.None).ToList();

        foreach(ShopButtonC buttonItem in buttonItems)
            _buttonItems.Add(buttonItem.ItemState, buttonItem);
    }

    public override void CloseDirectly()
    {
        PoolManager.Instance.Despawn(EPoolType.VisualObject);

        UIManager.Instance.OpenUI<MenuUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Menu);

        base.CloseDirectly();
    }
    public void OnNotify(object data)
    {
        if(data is int d)
        {
            foreach (ShopButtonC item in _buttonItems.Values)
            {
                if (item.gameObject.activeSelf)
                    item.gameObject.SetActive(false);
            }

            _buttonItems[(EItemState)d].gameObject.SetActive(true);
        }
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
    private Dictionary<EItemState, ShopButtonC> _buttonItems = new Dictionary<EItemState, ShopButtonC>();

    [SerializeField] private ShopSubject _subject;

    #endregion
}
