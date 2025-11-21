using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BotVisualC : CharacterVisualC
{
    #region --- Unity methods ---

    private void OnEnable()
    {
        if(_statsM is BotStatsM stats)
        {
            SetNewVisualData(stats);
            SetItemOnVisual(stats);

            OnconfigureState(stats);

            _stateM.NavMesh.speed = stats.MaxSpeed;
            _stateM.NavMesh.angularSpeed = stats.StatsSO.rotationSpeed;
        }
    }

    #endregion

    #region --- Methods ---

    private void OnconfigureState(BotStatsM stats)
    {
        int weaponId = stats.VisualData.GetIdByType(EItemType.Weapon);
        _stateM.WeaponType = _itemConfig.GetItemById<ItemWeapon>(weaponId, EItemType.Weapon).weaponType;
    }
    private void SetNewVisualData(BotStatsM stats)
    {
        // Clear previous visual data if it is not null.
        if (stats.VisualData != null)
            stats.VisualData.itemData.Clear();

        // Create new visual data if it is null.
        else
        {
            stats.VisualData = new VisualData()
            {
                itemData = new List<ItemVisualData>()
            };
        }

        // Add random items to visual data.
        stats.VisualData.itemData.Add(new ItemVisualData {
            id = _itemConfig.GetRandomIdItemByType(EItemType.Weapon),
            itemType = EItemType.Weapon,
        });
        stats.VisualData.itemData.Add(new ItemVisualData {
            id = _itemConfig.GetRandomIdItemByType(EItemType.Hat),
            itemType = EItemType.Hat,
        });
        stats.VisualData.itemData.Add(new ItemVisualData {
            id = _itemConfig.GetRandomIdItemByType(EItemType.Pant),
            itemType = EItemType.Pant,
        });
    }

    private void SetItemOnVisual(BotStatsM stats)
    {
        foreach(ItemVisualData item in stats.VisualData.itemData)
        {
            SetOnVisual(item.id, item.itemType);
        }

        stats.AddBaseStats();

        SetColorSkin(stats);
    }

    private void SetColorSkin(BotStatsM stats)
    {
        _bodyMeshRenderer.materials = new Material[] {
            stats.StatsSO.GetRandomColorMats()
        };
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private BotStateM _stateM;

    #endregion
}
