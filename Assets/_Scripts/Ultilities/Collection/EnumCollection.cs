using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Enums ---

#region -- Enum Tags ---
public enum ETag
{
    Player,
    Enemy,
    Control,
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

#endregion
