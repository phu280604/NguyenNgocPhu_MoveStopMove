using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomerangC : WeaponC
{
    #region --- Unity methods ---

    private void Awake()
    {
        _handler = new BoomerangH();

        _moveBack = new Timer(_stats.moveBackTime, () => { _state.MoveDirection *= -1; });

        StateM = _state;
        StatsSO = _stats;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _state.LastestPosition = transform.position;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _moveBack.OnReset();

        _state.IsReturning = false;
        _state.MoveDirection = Vector3.zero;
    }

    private void Update()
    {
        if(charCtrl != null)
        {
            if(!_state.HasTarget && _state.TargetPos != null)
            {
                _state.MoveDirection = (_state.TargetPos - transform.position);
                _state.HasTarget = true;
            }

            _moveBack.OnCountDown(Time.deltaTime);

            transform.position = _handler.OnMove(transform.position, _state.MoveDirection, Time.deltaTime * _stats.speed);

            Transform curTrans = transform;
            _handler.OnRotation(ref curTrans, _stats.rotateSpeed);
        }
    }

    #endregion

    #region --- Fields ---

    private BoomerangH _handler;

    private Timer _moveBack;

    [SerializeField] private BoomerangStateM _state;
    [SerializeField] private BoomerangStatsSO _stats;

    #endregion
}
