using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItems", menuName = "Shop/Items")]
public class ShopItemSO : ScriptableObject
{
    
    #region --- Fields ---

    public List<ItemSO> itemSOs = new List<ItemSO>();

    #endregion
}

[System.Serializable]
public class ItemSO
{
    public EItemType type;

    public bool isLock;
    public bool isDefault;

    public int coin;

    public Sprite icon;
    public Sprite albedo;

    public List<Material> materials = new List<Material>();
    public Mesh mesh;
}
