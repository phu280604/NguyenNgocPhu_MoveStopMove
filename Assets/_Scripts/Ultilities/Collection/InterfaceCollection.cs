using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Character State ---
public interface ICharacterStateM
{
    #region --- Properties ---

    public EPoolType WeaponType { get; set; }
    public Transform Target { get; set; }
    public Transform AtkRangePos { get; }
    public Transform SpawnWeaponPos { get; }
    public string[] LayerTargets { get; set; }
    public bool IsChangeRange { get; set; }
    public bool IsDelayAttack { get; set; }

    #endregion
}
#endregion

#region --- Weapon ---

public interface IWeaponHandler
{
    public Vector3 OnMove(Vector3 curPos, Vector3 targetPos, float speed);

    public void OnRotation(ref Transform weaponRot, float speed);
}
#endregion
