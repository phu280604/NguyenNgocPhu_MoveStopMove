using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BotVisualC : CharacterVisualC
{
    #region --- Unity methods ---

    private void OnEnable()
    {
        SetNewVisualData();
        SetItemOnVisual();

        OnconfigureState();
    }

    #endregion

    #region --- Methods ---

    private void OnconfigureState()
    {
        int weaponId = _statsM.VisualData.GetIdByType(EItemType.Weapon);
        _stateM.WeaponType = _itemConfig.GetItemById<ItemWeapon>(weaponId, EItemType.Weapon).weaponType;
    }
    private void SetNewVisualData()
    {
        // Clear previous visual data if it is not null.
        if (_statsM.VisualData != null)
            _statsM.VisualData.itemData.Clear();

        // Create new visual data if it is null.
        else
        {
            _statsM.VisualData = new VisualData()
            {
                itemData = new List<ItemVisualData>()
            };
        }

        // Add random items to visual data.
        _statsM.VisualData.itemData.Add(new ItemVisualData {
            id = _itemConfig.GetRandomIdItemByType(EItemType.Weapon),
            itemType = EItemType.Weapon,
        });
        _statsM.VisualData.itemData.Add(new ItemVisualData {
            id = _itemConfig.GetRandomIdItemByType(EItemType.Hat),
            itemType = EItemType.Hat,
        });
        _statsM.VisualData.itemData.Add(new ItemVisualData {
            id = _itemConfig.GetRandomIdItemByType(EItemType.Pant),
            itemType = EItemType.Pant,
        });
    }

    private void SetItemOnVisual()
    {
        foreach(ItemVisualData item in _statsM.VisualData.itemData)
        {
            SetOnVisual(item.id, item.itemType);
        }

        SetColorSkin();
    }

    private void SetColorSkin()
    {
        _bodyMeshRenderer.materials = new Material[] { 
            _statsM.StatsSO.GetRandomColorMats()
        };
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private BotStateM _stateM;
    [SerializeField] private BotStatsM _statsM;

    #endregion
}
