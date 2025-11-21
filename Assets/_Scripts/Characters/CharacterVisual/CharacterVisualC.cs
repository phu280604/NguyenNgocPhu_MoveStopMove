using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterVisualC : MonoBehaviour
{
    #region --- Methods ---

    protected virtual void SetOnVisual(int id, EItemType itemType)
    {
        switch (itemType)
        {
            // Weapon.
            case EItemType.Weapon:
                ItemWeapon weapon = _itemConfig.GetItemById<ItemWeapon>(id, itemType);
                _itemData = weapon;
                if (weapon != null)
                {
                    _weaponMesh.mesh = weapon.mesh;
                    _weaponMeshRenderer.materials = weapon.materials.ToArray();
                    SetTransform(_weaponMesh.transform, weapon.offset, weapon.rotation, weapon.scale);
                }
                break;

            // Hat.
            case EItemType.Hat:
                ItemHat hat = _itemConfig.GetItemById<ItemHat>(id, itemType);
                _itemData = hat;
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
                _itemData = pant;
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
        }

        if(_itemData != null)
            _statsM.OnInitItemEffect(_itemData.effectValue, _itemData.itemEffect);
    }

    protected void SetTransform(Transform transform, Vector3 offset, Vector3 rotation, Vector3 scale)
    {
        transform.localScale = scale;
        transform.localEulerAngles = rotation;
        transform.localPosition = offset;
    }

    #endregion

    #region --- Fields ---

    [Header("Skinned MeshRenderer components")]
    [SerializeField] protected SkinnedMeshRenderer _bodyMeshRenderer;
    [SerializeField] protected SkinnedMeshRenderer _pantMeshRenderer;

    [Header("MeshRenderer components")]
    [SerializeField] protected MeshRenderer _weaponMeshRenderer;
    [SerializeField] protected MeshRenderer _hatMeshRenderer;
    [SerializeField] protected MeshRenderer _backMeshRenderer;
    [SerializeField] protected MeshRenderer _hipMeshRenderer;

    [Header("MeshFilter components")]
    [SerializeField] protected MeshFilter _weaponMesh;
    [SerializeField] protected MeshFilter _hatMesh;
    [SerializeField] protected MeshFilter _backMesh;
    [SerializeField] protected MeshFilter _hipMesh;

    [Header("SkinData")]
    [SerializeField] protected ItemDataConfig _itemConfig;
    protected GenericItem _itemData;

    [Header("Model")]
    [SerializeField] protected CharacterStatsM _statsM;

    #endregion
}
