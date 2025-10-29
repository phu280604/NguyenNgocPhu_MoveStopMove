using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateM : MonoBehaviour
{
    #region --- Properties ---

    public Vector3 TargetPos { get; set; }
    public ETag TargetTag { get; set; }
    public bool HasHit { get; set; }

    #endregion
}
