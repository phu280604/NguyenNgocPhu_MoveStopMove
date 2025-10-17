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

#region -- Enum State --

public enum EState
{
    Idle,
    Movement,
    Attack,
    Dead,
}

#endregion

#region -- Enum Animation Paremeters --
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
