using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : GameUnit
{
    #region --- Methods ---

    public void OnInit(ItemSO item)
    {
        _item = item;

        _iconLock.SetActive(_item.isLock);
        _icon.sprite = _item.icon;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private GameObject _iconLock;

    [SerializeField] private Image _icon;

    private ItemSO _item;

    #endregion
}
