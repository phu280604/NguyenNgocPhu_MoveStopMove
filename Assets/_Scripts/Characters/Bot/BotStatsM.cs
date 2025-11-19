using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BotStatsM : MonoBehaviour
{
    #region --- Unity methods ---

    private void OnEnable()
    {
        WaitingTime = Random.Range(MIN_WAITING_TIME,MAX_WAITING_TIME);
        RangeMoving = Random.Range(MIN_RANGE_MOVING, MAX_RANGE_MOVING);
    }

    #endregion

    #region --- Properties ---

    public BotStatsSO BotStatsSO => _botStatsSO;
    public float WaitingTime { get; private set; }
    public float RangeMoving { get; private set; }
    public float DistanceStop => DISTANCE_STOP;

    #endregion

    #region --- Fields ---

    [SerializeField] private BotStatsSO _botStatsSO;

    private const float MIN_WAITING_TIME = 1f;
    private const float MAX_WAITING_TIME = 2.5f;

    private const float MIN_RANGE_MOVING = 5f;
    private const float MAX_RANGE_MOVING = 8f;

    private const float DISTANCE_STOP = 0.5f;

    #endregion
}
