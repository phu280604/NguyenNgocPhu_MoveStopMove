using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStateM : WeaponStateM
{
    #region --- Properties ---
    public Vector3 MoveDirection { get; set; }
    public bool HasTarget { get; set; } = false;

    #endregion
}
