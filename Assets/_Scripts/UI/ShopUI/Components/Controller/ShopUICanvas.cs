using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICanvas : UICanvas, IObserver<object>
{
    #region --- Overrides ---

    protected override void OnInit()
    {
        base.OnInit();

        _subject.AddObserver(EUIKey.Item, this);
        _subject.AddObserver(EUIKey.ItemEffect, this);

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

            if((EItemState)d == EItemState.Purchased)
                ChangeCoins();
        }
        else if(data is KeyValuePair<EItemEffect, float> effect)
        {
            string nameEffect = "";
            switch (effect.Key)
            {
                case EItemEffect.None:
                    nameEffect = StringCollection.BONUS_NONE;
                    _txtEffect.text = nameEffect;
                    return;
                case EItemEffect.BaseRange:
                    nameEffect = StringCollection.BONUS_RANGE;
                    break;
                case EItemEffect.RangeAfterEliminating:
                    nameEffect = StringCollection.BONUS_RANGE_AFTER_ELIMINATING;
                    break;
                case EItemEffect.MovementSpeed:
                    nameEffect = StringCollection.BONUS_MAX_SPEED;
                    break;
                case EItemEffect.MovementSpeedAfterEliminating:
                    nameEffect = StringCollection.BONUS_MAX_SPEED_AFTER_ELIMINATING;
                    break;
            }

            _txtEffect.text = $"+ {effect.Value} " + nameEffect;
        }
    }

    #endregion

    #region --- Unity methods ---

    private void OnEnable()
    {
        for (int i = 1; i < _tabItems.Count; i++)
            _tabItems[i].OnInit(false);

        ChangeCoins();
    }

    #endregion

    #region --- Methods ---

    private void ChangeCoins()
    {
        _txtCoins.text = LevelManager.Instance.Coins.ToString();
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _txtCoins;
    [SerializeField] private TextMeshProUGUI _txtEffect;

    [SerializeField] private List<TabColorC> _tabItems = new List<TabColorC>();
    private Dictionary<EItemState, ShopButtonC> _buttonItems = new Dictionary<EItemState, ShopButtonC>();

    [SerializeField] private ShopSubject _subject;

    #endregion
}
