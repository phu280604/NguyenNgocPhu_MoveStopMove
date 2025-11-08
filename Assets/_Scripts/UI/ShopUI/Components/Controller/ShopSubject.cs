using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSubject : Subject<EUIKey, int>
{
    #region --- Unity methods ---

    private void OnDestroy()
    {
        _observers.Clear();
    }

    #endregion
}
