using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabSpawnerH : MonoBehaviour, IObserver<object>
{
    #region --- Overrides ---

    public void OnNotify(object data)
    {
        PoolManager.Instance.Despawn(EPoolType.Item);

        if(data is int d)
        {
            List<GenericItem> items = new List<GenericItem>();

            switch ((EItemType)d)
            {
                case EItemType.Weapon:
                    items.AddRange(_subject.ItemDataConfig.GetItemsByType<ItemWeapon>(EItemType.Weapon));
                    break;
                case EItemType.Hat:
                    items.AddRange(_subject.ItemDataConfig.GetItemsByType<ItemHat>(EItemType.Hat));
                    break;
                case EItemType.Pant:
                    items.AddRange(_subject.ItemDataConfig.GetItemsByType<ItemPant>(EItemType.Pant));
                    break;
                case EItemType.Set:
                    items.AddRange(_subject.ItemDataConfig.GetItemsByType<ItemSetSkin>(EItemType.Set));
                    break;
            }

            SpawnItem(items);
        }
    }

    private void SpawnItem(List<GenericItem> items)
    {
        ShopItemC chooseItem = null;
        foreach(GenericItem item in items)
        {
            ShopItemC itemUI = PoolManager.Instance.Spawn<ShopItemC>(EPoolType.Item, Vector3.zero, Quaternion.identity);

            if(itemUI == null) continue;

            itemUI.transform.SetParent(_itemTransform);
            itemUI.OnInit(item, _subject, _itemToggleGroup);
            itemUI.ChooseItem(false);

            if (item.itemState == EItemState.Equipped)
                chooseItem = itemUI;
        }

        if(chooseItem != null)
            chooseItem.ChooseItem(true);
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
    [SerializeField] private ToggleGroup _itemToggleGroup;

    [SerializeField] private ShopSubject _subject;

    #endregion
}
