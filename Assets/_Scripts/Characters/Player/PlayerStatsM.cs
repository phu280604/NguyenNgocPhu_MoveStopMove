using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsM : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Awake()
    {
        CurrentRangeAttack = _statsSO.rangeAttack;
    }

    #endregion

    #region --- Properties ---

    public float CurrentSpeed { get; set; } = 0;

    public float CurrentRangeAttack { get; set; }

    public PlayerStatsSO StatsSO => _statsSO;

    #endregion

    #region --- Fields ---

    [SerializeField] private PlayerStatsSO _statsSO;

    #endregion
}
