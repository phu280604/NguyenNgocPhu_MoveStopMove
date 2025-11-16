using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTabC : MonoBehaviour
{
    #region --- Unity methods ---

    public void OnDisable()
    {
        _isToggled = false;
    }

    #endregion

    #region --- Methods ---

    public void NotifyObserversByKey(bool isToggled)
    {
        if (isToggled && !_isToggled)
        {
            // Notify tab change to ShopSubject.
            _subject.NotifyObservers(EUIKey.Tab, (int)_itemType);
            _subject.NotifyObservers(EUIKey.VisualItem, -1);

            _subject.CurrentItemType = _itemType;

            _isToggled = true;
        }
        else if (!isToggled)
            _isToggled = false;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private ShopSubject _subject;

    [SerializeField] private EItemType _itemType;
    [SerializeField] private bool _isToggled = false;

    #endregion
}
