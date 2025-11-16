using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopButtonC : MonoBehaviour, IObserver<object>
{
    #region --- Overrides ---

    public void OnNotify(object data)
    {
        if (_text != null && data is int cost)
            _text.text = cost.ToString();
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
        _subject.NotifyObservers(EUIKey.Button, (int)_itemState + 1);
    }

    #endregion

    #region --- Properties ---

    public EItemState ItemState => _itemState;

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private EItemState _itemState;

    [SerializeField] private ShopSubject _subject;

    #endregion
}
