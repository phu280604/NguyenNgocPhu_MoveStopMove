using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTabSpawnerH : MonoBehaviour, IObserver<int>
{
    #region --- Overrides ---

    public void OnNotify(int data)
    {
        PoolManager.Instance.Despawn(EPoolType.Item);

        foreach(ItemSO itemSO in _shopItemSOs[data].itemSOs) 
        {
            ShopItemC item = PoolManager.Instance.Spawn<ShopItemC>(EPoolType.Item, Vector3.zero, Quaternion.identity);

            item.transform.SetParent(_itemTransform);
            item.OnInit(itemSO);
        }
    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        if (_subject != null)
            _subject.AddObserver(EUIKey.Tab, this);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private Transform _itemTransform;

    [SerializeField] private Subject<EUIKey, int> _subject;

    [SerializeField] private List<ShopItemSO> _shopItemSOs = new List<ShopItemSO>();

    #endregion
}
