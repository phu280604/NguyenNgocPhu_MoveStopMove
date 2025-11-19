using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotVisualC : CharacterVisualC
{
    #region --- Unity methods ---

    private void OnEnable()
    {
        SetOnVisual(_itemConfig.GetRandomIdItemByType(EItemType.Weapon), EItemType.Weapon);
        SetOnVisual(_itemConfig.GetRandomIdItemByType(EItemType.Hat), EItemType.Hat);
        SetOnVisual(_itemConfig.GetRandomIdItemByType(EItemType.Pant), EItemType.Pant);

        SetColorSkin();
    }

    #endregion

    #region --- Methods ---

    private void SetColorSkin()
    {
        _bodyMeshRenderer.materials = new Material[] { 
            _statsM.BotStatsSO.GetRandomColorMats()
        };
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private BotStatsM _statsM;

    #endregion
}
