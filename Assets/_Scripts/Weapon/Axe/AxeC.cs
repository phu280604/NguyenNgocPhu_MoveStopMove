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
    }

    private void OnEnable()
    {
        OnDespawn(5f);
    }

    private void Update()
    {
        if(charCtrl != null)
        {
            transform.position = _handler.OnMove(transform.position, TargetPos, Time.deltaTime * _stats.speed);

            Transform curTrans = transform;
            _handler.OnRotation(ref curTrans, _stats.rotateSpeed);
        }
    }

    #endregion

    #region --- Fields ---

    private AxeH _handler;
    [SerializeField] private AxeStatsSO _stats;

    #endregion
}
