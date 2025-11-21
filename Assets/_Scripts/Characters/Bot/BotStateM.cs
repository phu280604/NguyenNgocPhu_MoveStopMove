using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BotStateM : MonoBehaviour, ICharacterStateM
{
    #region --- Overrides ---

    #region -- Properties --
    public EPoolType WeaponType { get; set; }
    public Transform Target { get; set; }
    public Transform AtkRangePos => _atkRangePos;
    public Transform SpawnWeaponPos => _weaponPos;
    public string[] LayerTargets { get; set; }
    public bool IsChangeRange { get; set; } = true;
    public bool IsDelayAttack { get; set; } = false;
    #endregion

    #endregion

    #region --- Unity methods ---

    private void OnDisable()
    {
        IsChangeRange = true;
        IsDelayAttack = false;
        IsMoving = false;
    }

    #endregion

    #region --- Properties ---

    public NavMeshAgent NavMesh => _navMesh;
    public SpriteRenderer SelectionRing => _selectionRing;
    public bool IsMoving { get; set; } = false;

    #endregion

    #region --- Fields ---

    [SerializeField] private Transform _atkRangePos;
    [SerializeField] private Transform _weaponPos;

    [SerializeField] private NavMeshAgent _navMesh;

    [SerializeField] private SpriteRenderer _selectionRing;

    #endregion
}
