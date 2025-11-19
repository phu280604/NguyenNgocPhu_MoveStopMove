using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotStateM : MonoBehaviour, ICharacterStateM
{
    #region --- Properties ---

    public EPoolType WeaponType { get; set; }
    public Transform Target { get; set; }
    public Transform SpawnWeaponPos => _weaponPos;
    public NavMeshAgent NavMesh => _navMesh;
    public bool IsDelayAttack { get; set; }
    public bool IsMoving { get; set; } = false;

    #endregion

    #region --- Fields ---

    [SerializeField] private Transform _weaponPos;

    [SerializeField] private NavMeshAgent _navMesh;

    #endregion
}
