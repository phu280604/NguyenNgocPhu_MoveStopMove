using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabC : MonoBehaviour, IAudioEvent
{
    #region --- Overrides ---

    public void OnAudioAction()
    {
        GameManager.Instance.GameSubject.NotifyObservers(EEventKey.Audio, EAudioKey.TabClick);
    }

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

    #endregion

    #region --- Fields ---

    [Header("UI components")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;

    [Header("Colors")]
    [SerializeField] private Color _offBackground;
    [SerializeField] private Color _offIcon;
    [SerializeField] private Color _onBackground;
    [SerializeField] private Color _onIcon;

    #endregion
}
