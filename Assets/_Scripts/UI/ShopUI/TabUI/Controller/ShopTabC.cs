using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTabC : MonoBehaviour
{
    #region --- Methods ---

    public void NotifyObserversByKey(bool isToggled)
    {
        if (isToggled)
            _subject.NotifyObservers(EUIKey.Tab, (int)_itemType);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private Subject<EUIKey, int> _subject;

    [SerializeField] private EItemType _itemType;

    #endregion
}
