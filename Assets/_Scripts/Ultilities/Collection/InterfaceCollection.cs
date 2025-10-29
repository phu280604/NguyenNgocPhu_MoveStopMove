using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Character State ---
public interface ICharacterStateM
{
    #region --- Properties ---
    public Transform Target { get; set; }
    public Transform SpawnWeaponPos { get; }
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
