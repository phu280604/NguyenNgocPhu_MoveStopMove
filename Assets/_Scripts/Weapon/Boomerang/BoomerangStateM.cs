using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangStateM : WeaponStateM
{
    #region --- Properties ---
    public Vector3 MoveDirection { get; set; }
    public Vector3 LastestPosition { get; set; }
    public bool IsReturning { get; set; } = false;

    #endregion
}
