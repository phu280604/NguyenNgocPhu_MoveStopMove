using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabItem : MonoBehaviour
{
    #region --- Overrides ---



    #endregion

    #region --- Methods ---

    public void OnInit(bool isToggled)
    {
        _toggle.isOn = isToggled;
        OnTabChange(_toggle.isOn);
    }

    public void OnTabChange(bool isToggled)
    {
        if (isToggled)
        {
            _background.color = _onBackground;
            _icon.color = _onIcon;
        }
        else
        {
            _background.color = _offBackground;
            _icon.color = _offIcon;
        }
    }

    public void OnActionChange()
    {
        if (_toggle.isOn && !_isToggled)
        {

        }
        else if(!_toggle.isOn)
            OnDisableTab();
    }

    private void OnDisableTab()
    {
        PoolManager.Instance.Despawn(EPoolType.Item);
        _isToggled = false;
    }

    #endregion

    #region --- Fields ---

    [Header("UI components")]
    [SerializeField] private Toggle _toggle;

    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;

    [Header("Off colors")]
    [SerializeField] private Color _offBackground;
    [SerializeField] private Color _offIcon;

    [Header("On colors")]
    [SerializeField] private Color _onBackground;
    [SerializeField] private Color _onIcon;

    [Header("ScriptableObjects")]
    [SerializeField] private ShopItemSO _itemInfo;

    private bool _isToggled = false;

    #endregion
}
