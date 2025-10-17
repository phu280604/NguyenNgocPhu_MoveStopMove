using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterC<TStateM, TStatsM> : MonoBehaviour
{
    #region --- Unity Methods ---



    #endregion

    #region --- Methods ---

    public virtual void OnInit() { }

    #endregion

    #region --- Properties ---
    
    public Animator Animator => _animator;
    public TStateM StateM => _stateM;
    public TStatsM StatsM => _statsM;

    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] private Animator _animator;

    [Header("Custom components")]
    [SerializeField] protected TStateM _stateM;
    [SerializeField] protected TStatsM _statsM;

    #endregion
}
