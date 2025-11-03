using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Enums ---

public enum EGameStates
{
    Menu,
    Shop,
    GamePlay,
}

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

#endregion
