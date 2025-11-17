using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Enums ---

#region -- Enum GameStates --
public enum EGameStates
{
    Menu,
    Shop,
    GamePlay,
}
#endregion

#region -- Enum Tags ---
public enum ETag
{
    Manager,
    Player,
    Bot,
    Control,
    MainCamera,
    Character,
    VisualParent,
}
#endregion

#region -- Enum Layers --
public enum ELayer
{
    Player,
    Bot,
}
#endregion

#region -- Enum State --

public enum EState
{
    Idle,
    Movement,
    Attack,
    Dead,
}

#endregion

#region -- Enum Animations --
public enum EAnim
{
    Idle,
    Run,
    Attack,
    Dead,
    Dance_Win,
    Ulti,
}

public enum EAnimParams
{
    IsDelayAttack,
}
#endregion

#region -- Enum Pool --
public enum EPoolType
{
    VisualObject,
    UIShopItem,
    Player,
    Bot,
    AxeProjectile,
    BoomerangProjectile,
    Item,
}

#endregion

#region -- Enum Bonus Stats --
public enum EBonusStats
{
    None,
    Speed,
    Shield,
}
#endregion

#region -- Enum ShopItems --
public enum EItemType
{
    Weapon = 0,
    Hat = 1,
    Pant = 2,
    Set = 3,
    Back = 4,
    Hip = 5,
}
#endregion

#region --- Enum ItemState ---

public enum EItemState
{
    None,
    Locked,
    Purchased,
    Equipped,
}

#endregion

#region -- Enum KeyObserver --

public enum EUIKey
{
    Tab = 0,
    Button = 1,
    Item = 2,
    VisualItem = 3,
    ItemCost = 4,
    SaveItem = 5,
}

#endregion

#endregion

public static class EnumCollection
{
    public static T GetEnumByString<T>(string eName) where T : Enum
    {
        if (Enum.TryParse(typeof(T), eName, out var result))
        {
            return (T)result;
        }
        throw new ArgumentException($"'{eName}' is not a valid value of enum {typeof(T).Name}");
    }
}

