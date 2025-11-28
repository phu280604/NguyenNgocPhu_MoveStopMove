using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualC : CharacterVisualC
{
    #region --- Unity methods ---

    private void Awake()
    {
        LoadFromFile();
        _isTrigged = false;
    }

    private void OnEnable()
    {
        if(_isTrigged)
            LoadFromFile();
    }

    private void OnDisable()
    {
        _isTrigged = true;
    }

    #endregion

    #region --- Methods ---

    private void LoadFromFile()
    {
        _visualData = LoadDataManager.Instance.Load<VisualData>(StringCollection.VISUAL_DATA);
        if (_visualData == null) return;

        if (_statsM is PlayerStatsM stats)
            stats.AddBaseStats();

        for (int i = _visualData.itemData.Count - 1; i >= 0; i--)
        {
            SetOnVisual(_visualData.itemData[i].id, _visualData.itemData[i].itemType);
        }

        
    }

    protected override void SetOnVisual(int id, EItemType itemType)
    {
        switch (itemType)
        {
            // Weapon.
            case EItemType.Weapon:
                ItemWeapon weapon = _itemConfig.GetItemById<ItemWeapon>(id, itemType);
                _stateM.WeaponType = weapon.weaponType;
                break;

            // Set.
            case EItemType.Set:
                ItemSetSkin setSkin = _itemConfig.GetItemById<ItemSetSkin>(id, itemType);
                _itemData = setSkin;
                idSetSkin = _itemData.id;
                if (setSkin != null)
                {
                    _bodyMeshRenderer.materials = setSkin.materials.ToArray();

                    SetSkin<ItemWeapon>(setSkin.weapon, EItemType.Weapon);
                    SetSkin<ItemHat>(setSkin.hat, EItemType.Hat);
                    SetSkin<ItemPant>(setSkin.pant, EItemType.Pant);
                    SetSkin<ItemBack>(setSkin.back, EItemType.Back);
                    SetSkin<ItemHip>(setSkin.hip, EItemType.Hip);
                }
                break;
        }

        base.SetOnVisual(id, itemType);
    }

    private void SetSkin<T>(T itemInSet, EItemType itemType) where T : GenericItem
    {
        switch (itemType)
        {
            case EItemType.Weapon:
                if (itemInSet.itemState == EItemState.None) return;
                _weaponMesh.mesh = itemInSet.mesh;
                _weaponMeshRenderer.materials = itemInSet.materials.ToArray();
                if (itemInSet is ItemWeapon weapon)
                    SetTransform(_weaponMesh.transform, weapon.offset, weapon.rotation, weapon.scale);
                break;

            // Hat.
            case EItemType.Hat:
                _itemConfig.UpdateItemStateById(-1, itemType, EItemState.Equipped);
                _hatMesh.mesh = itemInSet.mesh;
                _hatMeshRenderer.materials = itemInSet.materials.ToArray();
                if (itemInSet is ItemHat hat)
                    SetTransform(_hatMesh.transform, hat.offset, hat.rotation, hat.scale);
                break;

            // Pant.
            case EItemType.Pant:
                _itemConfig.UpdateItemStateById(-1, itemType, EItemState.Equipped);
                _pantMeshRenderer.materials = itemInSet.materials.ToArray();
                if (itemInSet is ItemPant pant)
                    foreach (Material mat in _pantMeshRenderer.materials)
                    {
                        if (pant.albedo == null) break;
                        Texture2D texture = pant.albedo.texture;
                        mat.SetTexture("_MainTex", texture);
                    }
                break;

            // Back.
            case EItemType.Back:
                _backMesh.mesh = itemInSet.mesh;
                _backMeshRenderer.materials = itemInSet.materials.ToArray();
                if (itemInSet is ItemBack back)
                    SetTransform(_backMesh.transform, back.offset, back.rotation, back.scale);
                break;

            // Hip.
            case EItemType.Hip:
                _hipMesh.mesh = itemInSet.mesh;
                _hipMeshRenderer.materials = itemInSet.materials.ToArray();
                if (itemInSet is ItemHip hip)
                    SetTransform(_hipMesh.transform, hip.offset, hip.rotation, hip.scale);
                break;
        }
    }

    #endregion

    #region --- Fields ---

    
    [SerializeField] private VisualData _visualData = new VisualData();

    [SerializeField] private PlayerStateM _stateM;

    private bool _isTrigged = true;

    #endregion
}
