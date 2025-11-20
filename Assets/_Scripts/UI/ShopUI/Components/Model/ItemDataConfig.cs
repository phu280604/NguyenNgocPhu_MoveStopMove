using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataConfig", menuName = "DataConfig/Items")]
public class ItemDataConfig : ScriptableObject
{
    #region --- Unity methods ---

    private void OnValidate()
    {
        GenerateRandomId<ItemWeapon>(weapons);
        GenerateRandomId<ItemHat>(hats);
        GenerateRandomId<ItemPant>(pants);
        GenerateRandomId<ItemSetSkin>(setSkins);
    }

    #endregion

    #region --- Methods ---

    private void GenerateRandomId<T>(List<T> items) where T : GenericItem
    {
        foreach(T item in items)
        {
            if(item == null) continue;

            if(item.id == 0)
            {
                item.SetId(CreateId());
            }

            usedId.Add(item.id);
        }
    }

    private int CreateId()
    {
        int newId;

        do
        {
            newId = Random.Range(1000, 9999);
        } while (usedId.Contains(newId));

        return newId;
    }

    public List<T> GetItemsByType<T>(EItemType type)
    {
        List<T> items = new List<T>();
        switch (type)
        {
            case EItemType.Weapon:
                items = weapons as List<T>;
                break;

            case EItemType.Hat:
                items = hats as List<T>;
                break;

            case EItemType.Pant:
                items = pants as List<T>;
                break;

            case EItemType.Set:
                items = setSkins as List<T>;
                break;
        }

        return items;
    }

    public T GetItemById<T>(int id, EItemType type) where T : GenericItem
    {
        List<T> items = GetItemsByType<T>(type);

        foreach(T item in items)
        {
            if(item.id == id)
                return item;
        }

        return null;
    }

    public int GetIdFirstItem(EItemType type)
    {
        switch (type)
        {
            case EItemType.Weapon:
                return weapons[0].id;
            case EItemType.Hat:
                return hats[0].id;
            case EItemType.Pant:
                return pants[0].id;
            case EItemType.Set:
                return setSkins[0].id;
        }

        return -1;
    }

    public int GetRandomIdItemByType(EItemType type)
    {
        switch (type)
        {
            case EItemType.Weapon:
                return weapons[Random.Range(0, weapons.Count)].id;
            case EItemType.Hat:
                return hats[Random.Range(0, hats.Count)].id;
            case EItemType.Pant:
                return pants[Random.Range(0, pants.Count)].id;
            case EItemType.Set:
                return setSkins[Random.Range(0, setSkins.Count)].id;
        }

        return -1;
    }

    public void UpdateItemStateById(int id, EItemType type, EItemState state)
    {
        switch (type)
        {
            case EItemType.Weapon:
                if (state == EItemState.Equipped)
                    RemoveEquippedState<ItemWeapon>(weapons);
                if(id != -1)
                    GetItemById<ItemWeapon>(id, type).itemState = state;
                break;
            case EItemType.Hat:
                if (state == EItemState.Equipped)
                    RemoveEquippedState<ItemHat>(hats);
                if (id != -1)
                    GetItemById<ItemHat>(id, type).itemState = state;
                break;
            case EItemType.Pant:
                if (state == EItemState.Equipped)
                    RemoveEquippedState<ItemPant>(pants);
                if (id != -1)
                    GetItemById<ItemPant>(id, type).itemState = state;
                break;
            case EItemType.Set:
                if (state == EItemState.Equipped)
                    RemoveEquippedState<ItemSetSkin>(setSkins);
                if (id != -1)
                    GetItemById<ItemSetSkin>(id, type).itemState = state;
                break;
        }
    }
    
    private void RemoveEquippedState<T>(List<T> items) where T : GenericItem
    {
        foreach (T item in items)
        {
            if (item.itemState == EItemState.Equipped)
            {
                item.itemState = EItemState.Purchased;
                break;
            }

        }
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private List<ItemWeapon> weapons = new List<ItemWeapon>();
    [SerializeField] private List<ItemHat> hats = new List<ItemHat>();
    [SerializeField] private List<ItemPant> pants = new List<ItemPant>();
    [SerializeField] private List<ItemSetSkin> setSkins = new List<ItemSetSkin>();

    private HashSet<int> usedId = new HashSet<int>();

    #endregion
}

[System.Serializable]
public class GenericItem
{
    #region --- Methods ---

    public void SetId(int newId)
    {
        _id = newId;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private int _id;
    public int id => _id;

    [Header("Cost")]
    public int cost;

    [Header("State")]
    public EItemState itemState;

    [Header("Mesh & Mats")]
    public List<Material> materials = new List<Material>();
    public Mesh mesh;

    [Header("Sprite")]
    public Sprite icon;

    #endregion
}

[System.Serializable]
public class ItemWeapon : GenericItem
{
    [Header("Bullet types")]
    public EPoolType weaponType;

    [Header("Size & Offset")]
    public Vector3 offset;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;
}

[System.Serializable]
public class ItemHat : GenericItem
{
    [Header("Size & Offset")]
    public Vector3 offset;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;
}

[System.Serializable]
public class ItemBack : GenericItem
{
    [Header("Size & Offset")]
    public Vector3 offset;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;
}

[System.Serializable]
public class ItemHip : GenericItem
{
    [Header("Size & Offset")]
    public Vector3 offset;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;
}

[System.Serializable]
public class ItemPant : GenericItem
{
    public Sprite albedo;
}

[System.Serializable]
public class ItemSetSkin : GenericItem
{
    public ItemWeapon weapon;
    public ItemHat hat;
    public ItemPant pant;
    public ItemBack back;
    public ItemHip hip;
}
