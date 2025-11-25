using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerStateM : MonoBehaviour, ICharacterStateM
{
    #region --- Overrides ---

    #region -- Properties --
    public EPoolType WeaponType { get; set; } = EPoolType.AxeProjectile;
    public Transform Target { get; set; }
    public Transform AtkRangePos => _atkRangePos;
    public Transform SpawnWeaponPos { get { return _spawnPos; } }
    public string[] LayerTargets { get; set; }
    public bool IsDelayAttack { get; set; } = false;
    public bool IsChangeRange { get; set; } = true;
    public bool IsDelayDecay { get; set; } = false;
    public bool IsDead { get; set; } = false;
    #endregion

    #endregion

    #region --- Methods ---

    public void OnReset()
    {
        _collider.enabled = true;

        WeaponType = EPoolType.AxeProjectile;

        Target = null;

        IsDelayAttack = false;
        IsChangeRange = true;
        IsDead = false;

        Direction = Vector3.zero;
        LastestDirection = Vector3.zero;
    }

    public void OnHideCollider()
    {
        _collider.enabled = false;
    }

    #endregion

    #region --- Properties ---

    public Vector3 Direction { get; set; } = Vector3.zero;
    public Vector3 LastestDirection { get; set; } = Vector3.zero;

    #endregion

    #region --- Fields ---

    [SerializeField] private Collider _collider;

    [SerializeField] private Transform _atkRangePos;
    [SerializeField] private Transform _spawnPos;

    #endregion
}
