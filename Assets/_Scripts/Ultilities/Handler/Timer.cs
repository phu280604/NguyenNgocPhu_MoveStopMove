using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer
{
    public Timer(float maxTime, Action onTrigger)
    {
        _maxTime = maxTime;
        _curTime = _maxTime;

        _isTriggered = false;

        _onTrigger = onTrigger;
    }

    #region --- Methods ---

    public void OnCountDown(float timeValue, bool isRepeat = false)
    {
        if (_curTime <= 0 && !_isTriggered)
        {
            _onTrigger?.Invoke();
            _isTriggered = true;

            if (!isRepeat)
                return;

            OnReset();
        }

        if (_curTime > 0)
            _curTime -= timeValue;
    }
    
    public void OnReset()
    {
        _curTime = _maxTime;
        _isTriggered = false;
    }

    #endregion

    #region --- Fields ---

    private float _maxTime;
    private float _curTime;

    private bool _isTriggered;

    private Action _onTrigger;

    #endregion
}
