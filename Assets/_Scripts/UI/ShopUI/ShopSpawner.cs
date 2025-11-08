using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpawner : IObserver<int>
{
    #region --- Overrides ---

    /// <summary>
    /// Spawn shop item.
    /// </summary>
    /// <param name="data">Index of tabItem.</param>
    public void OnNotify(int data)
    {
        PoolManager.Instance.Despawn(EPoolType.Item);

        foreach (ItemSO itemSO in _shopItems[data].itemSOs)
        {
            ShopItem item = PoolManager.Instance.Spawn<ShopItem>(EPoolType.Item, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(_itemTrans);
            item.OnInit(itemSO);
        }
    }

    #endregion

    #region --- Unity methods ---

    
    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] private Transform _itemTrans;

    [Header("Shop components")]
    [SerializeField] private List<ShopItemSO> _shopItems = new List<ShopItemSO>();

    #endregion
}
