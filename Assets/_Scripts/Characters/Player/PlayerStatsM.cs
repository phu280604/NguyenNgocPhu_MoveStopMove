using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsM : MonoBehaviour
{
    #region --- Properties ---

    public float CurrentSpeed { get; set; } = 0;
    public PlayerStatsSO StatsSO => _statsSO;

    #endregion

    #region --- Fields ---

    [SerializeField] private PlayerStatsSO _statsSO;

    #endregion
}
