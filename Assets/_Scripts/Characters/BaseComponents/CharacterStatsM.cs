using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsM : MonoBehaviour
{
    #region --- Unity methods ---

    protected virtual void OnDisable()
    {
        OnResetStats();
    }

    #endregion

    #region --- Methods ---

    public virtual void OnResetStats()
    {
        MaxSpeed = 0;
        CurrentRangeAttack = 0;

        effectStats.Clear();
    }

    public virtual void OnInitItemEffect(float bonusStats, EItemEffect effectType)
    {
        switch (effectType)
        {
            case EItemEffect.BaseRange:
                CurrentRangeAttack += bonusStats;
                break;

            case EItemEffect.MovementSpeed:
                MaxSpeed += bonusStats;
                break;

            case EItemEffect.RangeAfterEliminating:
                effectStats.Add(effectType, bonusStats);
                break;

            case EItemEffect.MovementSpeedAfterEliminating:
                effectStats.Add(effectType, bonusStats);
                break;
        }
    }

    public virtual void OnUpdateStatsAfterEliminating()
    {
        foreach(KeyValuePair<EItemEffect, float> effect in effectStats)
        {
            switch (effect.Key)
            {
                case EItemEffect.RangeAfterEliminating:
                    CurrentRangeAttack += effect.Value;
                    break;

                case EItemEffect.MovementSpeedAfterEliminating:
                    MaxSpeed += effect.Value;
                    break;
            }
        }
    }

    #endregion

    #region --- Properties ---

    public float MaxSpeed { get; protected set; }
    public float CurrentRangeAttack { get; protected set; }

    #endregion

    #region --- Fields ---

    protected Dictionary<EItemEffect, float> effectStats = new Dictionary<EItemEffect, float>();

    #endregion
}
