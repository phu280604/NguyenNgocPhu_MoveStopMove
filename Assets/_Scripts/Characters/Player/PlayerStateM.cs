using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateM : MonoBehaviour, ICharacterStateM
{
    #region --- Properties ---

    public Vector3 Direction { get; set; } = Vector3.zero;
    public Vector3 LastestDirection { get; set; } = Vector3.zero;
    public Transform Target {  get; set; }
    public Transform SpawnWeaponPos { get { return _spawnPos; } }
    public bool IsDelayAttack { get; set; } = false;
    public EPoolType WeaponType { get; set; } = EPoolType.BoomerangProjectile;

    #endregion

    #region --- Fields ---

    [SerializeField] private Transform _spawnPos;

    #endregion
}
