using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomerangC : WeaponC
{
    #region --- Overrides ---

    protected override void OnMove()
    {
        if (!_stateM.HasTarget && _stateM.TargetPos != null)
        {
            _stateM.MoveDirection = (_stateM.TargetPos - transform.position);
            _stateM.HasTarget = true;
        }

        _moveBack.OnCountDown(Time.deltaTime);
        transform.position = _handler.OnMove(transform.position, _stateM.MoveDirection, Time.deltaTime * _statsM.speed);
    }

    protected override void OnRotation()
    {
        Transform curTrans = transform;
        _handler.OnRotation(ref curTrans, _statsM.rotateSpeed);
    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        _handler = new BoomerangH();

        _moveBack = new Timer(_statsM.moveBackTime, () => { _stateM.MoveDirection *= -1; });

        StateM = _stateM;
        StatsSO = _statsM;

        if (audioSubject != null)
            audioSubject.AddObserver(EEventKey.Audio, weaponObserver);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stateM.LastestPosition = transform.position;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _moveBack.OnReset();

        _stateM.IsReturning = false;
        _stateM.MoveDirection = Vector3.zero;
    }

    #endregion

    #region --- Fields ---

    private Timer _moveBack;

    [Header("Model components")]
    [SerializeField] private BoomerangStateM _stateM;
    [SerializeField] private BoomerangStatsSO _statsM;

    #endregion
}
