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
    Weapon,
    Hat,
    Pant,
    Set,
}
#endregion

#endregion
