using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AxeC : WeaponC
{
    #region --- Unity methods ---

    private void Awake()
    {
        _handler = new AxeH();
        StateM = _stateM;
    }

    private void OnEnable()
    {
        OnDespawn(5f);
    }

    private void OnDisable()
    {
        _stateM.HasTarget = false;

        CancelInvoke();
    }

    private void Update()
    {
        if(charCtrl != null)
        {
            if(!_stateM.HasTarget && _stateM.TargetPos != null)
            {
                _stateM.MoveDirection = (_stateM.TargetPos - transform.position).normalized;
                _stateM.HasTarget = true;
            }
            
            transform.position = _handler.OnMove(transform.position, _stateM.MoveDirection, Time.deltaTime * _stats.speed);

            Transform curTrans = transform;
            _handler.OnRotation(ref curTrans, _stats.rotateSpeed);
        }
    }

    #endregion

    #region --- Fields ---

    private AxeH _handler;

    [SerializeField] private AxeStateM _stateM;
    [SerializeField] private AxeStatsSO _stats;

    #endregion
}
