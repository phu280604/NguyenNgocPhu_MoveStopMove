using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsM : CharacterStatsM
{
    #region --- Overrides ---

    public override void OnInitItemEffect(float bonusStats, EItemEffect effectType)
    {
        base.OnInitItemEffect(bonusStats, effectType);
    }

    #endregion

    #region --- Methods ---

    public void AddBaseStats()
    {
        CurrentRangeAttack += _statsSO.rangeAttack;
        MaxSpeed += _statsSO.maxMovementSpeed;
    }

    #endregion

    #region --- Properties ---

    public float CurrentSpeed { get; set; } = 0;
    public PlayerStatsSO StatsSO => _statsSO;

    #endregion

    #region --- Fields ---

    [SerializeField] private PlayerStatsSO _statsSO;

    #endregion
}
