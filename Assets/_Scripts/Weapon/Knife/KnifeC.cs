using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnifeC : WeaponC
{
    #region --- Overrides ---

    protected override void OnMove()
    {
        if (!_stateM.HasTarget && _stateM.TargetPos != null)
        {
            _stateM.MoveDirection = (_stateM.TargetPos - transform.position).normalized;
            _stateM.HasTarget = true;
        }

        transform.position = _handler.OnMove(transform.position, _stateM.MoveDirection, Time.deltaTime * _statsM.speed);
    }

    protected override void OnRotation()
    {

    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        _handler = new KnifeH();

        StateM = _stateM;
        StatsSO = _statsM;

        if (audioSubject != null)
            audioSubject.AddObserver(EEventKey.Audio, weaponObserver);
    }

    #endregion

    #region --- Fields ---

    [Header("Model components")]
    [SerializeField] protected KnifeStateM _stateM;
    [SerializeField] protected WeaponStatsSO _statsM;

    #endregion
}
