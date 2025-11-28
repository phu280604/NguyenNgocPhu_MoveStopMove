using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacterVisualC : GameUnit, IObserver<object>
{
    #region --- Overrides ---

    public void OnNotify(object data)
    {
        if ((data is int d))
        {
            // Set item to visual when choosing item in shop UI.
            if (d > 0)
                SetOnVisual(d, _subject.CurrentItemType);
            else
                ResetItem();
        }
        else if(data is ItemVisualData item)
        {
            // Save data.
            _visualData.SetNewIdByType(item.id, item.itemType);
            if(item.itemType == EItemType.Set)
            {
                ItemSetSkin setSkin = _subject.ItemDataConfig.GetItemById<ItemSetSkin>(item.id, EItemType.Set);
                _visualData.SetNewIdByType(_subject.ItemDataConfig.GetIdFirstItem(EItemType.Hat), EItemType.Hat);
                _visualData.SetNewIdByType(_subject.ItemDataConfig.GetIdFirstItem(EItemType.Pant), EItemType.Pant);
            }
            SaveDataManager.Instance.Save<VisualData>(_visualData, StringCollection.VISUAL_DATA);
        }
    }

    #endregion

    #region --- Unity methods ---



    #endregion

    #region --- Methods ---

    public void OnInit(ShopSubject subject)
    {
        _subject = subject;

        LoadFromFile();

        SaveBaseData();

        _subject.AddObserver(EUIKey.VisualItem, this);
        _subject.AddObserver(EUIKey.SaveItem, this);
    }
    private void SaveBaseData()
    {
        if (_visualData != null) return;

        _visualData = new VisualData();

        _visualData.itemData.Add(new ItemVisualData
        {
            id = _subject.ItemDataConfig.GetIdFirstItem(EItemType.Weapon),
            itemType = EItemType.Weapon
        });
        _visualData.itemData.Add(new ItemVisualData
        {
            id = _subject.ItemDataConfig.GetIdFirstItem(EItemType.Hat),
            itemType = EItemType.Hat
        });
        _visualData.itemData.Add(new ItemVisualData
        {
            id = _subject.ItemDataConfig.GetIdFirstItem(EItemType.Pant),
            itemType = EItemType.Pant
        });
        _visualData.itemData.Add(new ItemVisualData
        {
            id = _subject.ItemDataConfig.GetIdFirstItem(EItemType.Set),
            itemType = EItemType.Set
        });

        SaveDataManager.Instance.Save<VisualData>(_visualData, StringCollection.VISUAL_DATA);
    }
    private void LoadFromFile()
    {
        _visualData = LoadDataManager.Instance.Load<VisualData>(StringCollection.VISUAL_DATA);
        if (_visualData == null) return;

        for(int i = _visualData.itemData.Count - 1; i >= 0; i--)
        {
            SetOnVisual(_visualData.itemData[i].id, _visualData.itemData[i].itemType);
        }
    }
    private void ResetItem()
    {
        LoadFromFile();
    }
    private void SetOnVisual(int id, EItemType itemType)
    {
        switch (itemType)
        {
            // Weapon.
            case EItemType.Weapon:
                ItemWeapon weapon = _subject.ItemDataConfig.GetItemById<ItemWeapon>(id, itemType);
                if (weapon != null)
                {
                    _weaponMesh.mesh = weapon.mesh;
                    _weaponMeshRenderer.materials = weapon.materials.ToArray();
                    SetTransform(_weaponMesh.transform, weapon.offset, weapon.rotation, weapon.scale);
                }
                break;

            // Hat.
            case EItemType.Hat:
                if (HasOtherItemFromSet(id, itemType)) return;

                ItemHat hat = _subject.ItemDataConfig.GetItemById<ItemHat>(id, itemType);
                if (hat != null)
                {
                    _hatMesh.mesh = hat.mesh;
                    _hatMeshRenderer.materials = hat.materials.ToArray();
                    SetTransform(_hatMesh.transform, hat.offset, hat.rotation, hat.scale);

                    if(id == _visualData.GetIdByType(EItemType.Hat))
                        SetSkin<ItemHat>(hat, EItemType.Hat);
                }
                break;

            // Pant.
            case EItemType.Pant:
                if (HasOtherItemFromSet(id, itemType)) return;

                ItemPant pant = _subject.ItemDataConfig.GetItemById<ItemPant>(id, itemType);
                if (pant != null)
                {
                    _pantMeshRenderer.materials = pant.materials.ToArray();
                    foreach(Material mat in _pantMeshRenderer.materials)
                    {
                        if (pant.albedo == null) break;
                        Texture2D texture = pant.albedo.texture;
                        mat.SetTexture("_MainTex", texture);
                    }

                    if (id == _visualData.GetIdByType(EItemType.Pant))
                        SetSkin<ItemPant>(pant, EItemType.Pant);
                }
                break;

            // Back.
            case EItemType.Back:
                ItemBack back = _subject.ItemDataConfig.GetItemById<ItemBack>(id, itemType);
                if (back != null)
                {
                    _backMesh.mesh = back.mesh;
                    _backMeshRenderer.materials = back.materials.ToArray();
                    SetTransform(_backMesh.transform, back.offset, back.rotation, back.scale);
                }
                break;

            // Hip.
            case EItemType.Hip:
                ItemHip hip = _subject.ItemDataConfig.GetItemById<ItemHip>(id, itemType);
                if (hip != null)
                {
                    _hipMesh.mesh = hip.mesh;
                    _hipMeshRenderer.materials = hip.materials.ToArray();
                    SetTransform(_hipMesh.transform, hip.offset, hip.rotation, hip.scale);
                }
                break;

            // Set.
            case EItemType.Set:
                ItemSetSkin setSkin = _subject.ItemDataConfig.GetItemById<ItemSetSkin>(id, itemType);
                    
                if (setSkin != null)
                {
                    idSetSkin = setSkin.id;

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
                if(itemInSet is ItemWeapon weapon)
                    SetTransform(_weaponMesh.transform, weapon.offset, weapon.rotation, weapon.scale);
                break;

            // Hat.
            case EItemType.Hat:
                _subject.ItemDataConfig.UpdateItemStateById(itemInSet.id, itemType, EItemState.Equipped);
                _hatMesh.mesh = itemInSet.mesh;
                _hatMeshRenderer.materials = itemInSet.materials.ToArray();
                if (itemInSet is ItemHat hat)
                    SetTransform(_hatMesh.transform, hat.offset, hat.rotation, hat.scale);
                break;

            // Pant.
            case EItemType.Pant:
                _subject.ItemDataConfig.UpdateItemStateById(itemInSet.id, itemType, EItemState.Equipped);
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

    private bool HasOtherItemFromSet(int id, EItemType itemType)
    {
        if (_subject.ItemDataConfig.GetIdFirstItem(itemType) != id || idSetSkin == 0) return false;

        switch (itemType)
        {
            case EItemType.Hat:
                if (_subject.ItemDataConfig.GetItemById<ItemSetSkin>(idSetSkin, EItemType.Set).hat.itemState != EItemState.None)
                    return true;
                break;

            case EItemType.Pant:
                if (_subject.ItemDataConfig.GetItemById<ItemSetSkin>(idSetSkin, EItemType.Set).pant.itemState != EItemState.None)
                    return true;
                break;
        }

        return false;
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

    [Header("Observer")]
    [SerializeField] private ShopSubject _subject;

    [SerializeField] private VisualData _visualData = new VisualData();

    private int idSetSkin;

    #endregion
}

[System.Serializable]
public class ItemVisualData
{
    public int id;
    public EItemType itemType;
}

[System.Serializable]
public class VisualData
{
    public List<ItemVisualData> itemData = new List<ItemVisualData>();

    public int GetIdByType(EItemType type)
    {
        if (itemData == null || itemData.Count <= 0) return -1;

        foreach(ItemVisualData item in itemData)
        {
            if(item.itemType == type)
                return item.id;
        }

        return -1;
    }

    public void SetNewIdByType(int newId, EItemType type)
    {
        if (itemData == null || itemData.Count <= 0) return;

        foreach (ItemVisualData item in itemData)
        {
            if (item.itemType == type)
            {
                item.id = newId;
                break;
            }
        }
    }
}
