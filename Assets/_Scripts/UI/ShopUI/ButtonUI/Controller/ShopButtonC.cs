using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopButtonC : MonoBehaviour, IObserver<object>
{
    #region --- Overrides ---

    public void OnNotify(object data)
    {
        if (_text != null && data is int coins)
        {
            _coins = coins;
            _text.text = coins.ToString();
        }
    }

    #endregion

    #region --- Unity methods ---

    private void Start()
    {
        _subject.AddObserver(EUIKey.ItemCost, this);
    }

    #endregion

    #region --- Methods ---

    public void OnClick()
    {
        switch (_itemState)
        {
            case EItemState.Locked:
                if (_coins <= GameManager.Instance.LevelData.coins && GameManager.Instance.LevelData.coins != 0)
                {
                    GameManager.Instance.LevelData.coins -= _coins;
                    GameManager.Instance.SetCoin(GameManager.Instance.LevelData.coins);

                    _subject.NotifyObservers(EUIKey.Button, (int)_itemState + 1);
                }
                break;

            case EItemState.Purchased:
            case EItemState.Equipped:
                _subject.NotifyObservers(EUIKey.Button, (int)_itemState + 1);
                break;
        }
    }

    #endregion

    #region --- Properties ---

    public EItemState ItemState => _itemState;

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private EItemState _itemState;

    [SerializeField] private ShopSubject _subject;

    private int _coins;

    #endregion
}
