using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualC : MonoBehaviour
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

        for (int i = _visualData.itemData.Count - 1; i >= 0; i--)
        {
            SetOnVisual(_visualData.itemData[i].id, _visualData.itemData[i].itemType);
        }
    }

    private void SetOnVisual(int id, EItemType itemType)
    {
        switch (itemType)
        {
            // Weapon.
            case EItemType.Weapon:
                ItemWeapon weapon = _itemConfig.GetItemById<ItemWeapon>(id, itemType);
                if (weapon != null)
                {
                    _weaponMesh.mesh = weapon.mesh;
                    _weaponMeshRenderer.materials = weapon.materials.ToArray();
                    _stateM.WeaponType = weapon.bulletType;
                    SetTransform(_weaponMesh.transform, weapon.offset, weapon.rotation, weapon.scale);
                }
                break;

            // Hat.
            case EItemType.Hat:
                ItemHat hat = _itemConfig.GetItemById<ItemHat>(id, itemType);
                if (hat != null)
                {
                    _hatMesh.mesh = hat.mesh;
                    _hatMeshRenderer.materials = hat.materials.ToArray();
                    SetTransform(_hatMesh.transform, hat.offset, hat.rotation, hat.scale);
                }
                break;

            // Pant.
            case EItemType.Pant:
                ItemPant pant = _itemConfig.GetItemById<ItemPant>(id, itemType);
                if (pant != null)
                {
                    _pantMeshRenderer.materials = pant.materials.ToArray();
                    foreach (Material mat in _pantMeshRenderer.materials)
                    {
                        if (pant.albedo == null) break;
                        Texture2D texture = pant.albedo.texture;
                        mat.SetTexture("_MainTex", texture);
                    }
                }
                break;

            // Back.
            case EItemType.Back:
                ItemBack back = _itemConfig.GetItemById<ItemBack>(id, itemType);
                if (back != null)
                {
                    _backMesh.mesh = back.mesh;
                    _backMeshRenderer.materials = back.materials.ToArray();
                    SetTransform(_backMesh.transform, back.offset, back.rotation, back.scale);
                }
                break;

            // Hip.
            case EItemType.Hip:
                ItemHip hip = _itemConfig.GetItemById<ItemHip>(id, itemType);
                if (hip != null)
                {
                    _hipMesh.mesh = hip.mesh;
                    _hipMeshRenderer.materials = hip.materials.ToArray();
                    SetTransform(_hipMesh.transform, hip.offset, hip.rotation, hip.scale);
                }
                break;

            // Set.
            case EItemType.Set:
                ItemSetSkin setSkin = _itemConfig.GetItemById<ItemSetSkin>(id, itemType);
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

    private void SetTransform(Transform transform, Vector3 offset, Vector3 rotation, Vector3 scale)
    {
        transform.localScale = scale;
        transform.localEulerAngles = rotation;
        transform.localPosition = offset;
    }

    #endregion

    #region --- Fields ---

    [Header("Skinned MeshRenderer components")]
    [SerializeField] private SkinnedMeshRenderer _bodyMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer _pantMeshRenderer;

    [Header("MeshRenderer components")]
    [SerializeField] private MeshRenderer _hatMeshRenderer;
    [SerializeField] private MeshRenderer _weaponMeshRenderer;
    [SerializeField] private MeshRenderer _backMeshRenderer;
    [SerializeField] private MeshRenderer _hipMeshRenderer;

    [Header("MeshFilter components")]
    [SerializeField] private MeshFilter _hatMesh;
    [SerializeField] private MeshFilter _weaponMesh;
    [SerializeField] private MeshFilter _backMesh;
    [SerializeField] private MeshFilter _hipMesh;

    [SerializeField] private ItemDataConfig _itemConfig;
    [SerializeField] private VisualData _visualData = new VisualData();

    [SerializeField] private PlayerStateM _stateM;

    private bool _isTrigged = true;

    #endregion
}
