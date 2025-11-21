using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopItemC : GameUnit, IObserver<object>
{
    #region --- Unity methods ---

    public void OnDisable()
    {
        _isToggled = false;
    }

    #endregion

    #region --- Methods ---

    public void OnInit(GenericItem item, ShopSubject subject, ToggleGroup toogleGroup)
    {
        _item = item;
        _subject = subject;
        _itemToggle.group = toogleGroup;

        _iconLock.SetActive(_item.itemState == EItemState.Locked);
        _icon.sprite = _item.icon;

        _subject.AddObserver(EUIKey.Button, this);
    }

    public void ChooseItem(bool isToggled)
    {
        _chooseFrame.SetActive(isToggled);
        _itemToggle.isOn = isToggled;

        if (isToggled && !_isToggled)
        {
            _isToggled = isToggled;

            // Notify item change to CharacterVisual.
            _subject.NotifyObservers(EUIKey.VisualItem, _item.id);

            // Notify item state to ShopUIButton.
            _subject.NotifyObservers(EUIKey.Item, (int)_item.itemState);
            _subject.NotifyObservers(
                EUIKey.ItemEffect,
                new KeyValuePair<EItemEffect, float>(
                    _item.itemEffect,
                    _item.effectValue
                )
            );

            // Notify item cost to ShopUIButtonCost.
            _subject.NotifyObservers(EUIKey.ItemCost, _item.cost);

            _subject.CurrentIdItem = _item.id;
        }
        else if (!isToggled)
            _isToggled = false;
    }

    public void OnNotify(object data)
    {
        if (!(data is int d)) return;

        switch (d)
        {
            // After purchased.
            case (int)EItemState.Purchased:
                if (_item.id != _subject.CurrentIdItem) return;

                _iconLock.SetActive(false);
                _item.itemState = (EItemState)d;
                _subject.ItemDataConfig.UpdateItemStateById(_subject.CurrentIdItem, _subject.CurrentItemType, EItemState.Purchased);
                _subject.NotifyObservers(EUIKey.Item, (int)_item.itemState);
                break;

            // After equipped.
            case (int)EItemState.Equipped:
                if (_item.id != _subject.CurrentIdItem) return;
                _subject.ItemDataConfig.UpdateItemStateById(_subject.CurrentIdItem, _subject.CurrentItemType, EItemState.Equipped);

                _subject.NotifyObservers(EUIKey.Item, (int)_item.itemState);
                _subject.NotifyObservers(EUIKey.SaveItem, new ItemVisualData { 
                    id = _subject.CurrentIdItem,
                    itemType = _subject.CurrentItemType
                });
                break;
        }
    }

    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] private GameObject _iconLock;
    [SerializeField] private GameObject _chooseFrame;
    [SerializeField] private Image _icon;
    [SerializeField] private Toggle _itemToggle;

    [Header("Observer")]
    [SerializeField] private ShopSubject _subject;

    private GenericItem _item;

    private bool _isToggled = false;

    #endregion
}
