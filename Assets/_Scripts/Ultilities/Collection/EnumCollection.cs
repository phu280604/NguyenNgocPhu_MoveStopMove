using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Enums ---

#region -- Enum Tags ---
public enum ETag
{
    Manager,
    Player,
    Enemy,
    Control,
    MainCamera,
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
#endregion

#region -- Enum Pool --
public enum EPoolType
{
    Player,
    Bot,
    AxeProjectile,
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
