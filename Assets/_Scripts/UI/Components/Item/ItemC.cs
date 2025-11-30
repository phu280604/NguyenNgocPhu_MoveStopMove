using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemC : MonoBehaviour, IAudioEvent
{
    #region --- Overrides ---

    public void OnAudioAction()
    {
        GameManager.Instance.GameSubject.NotifyObservers(EEventKey.Audio, EAudioKey.ItemClick);
    }

    #endregion

    #region --- Methods ---

    public void OnSelected(bool isSelected)
    {
        if (isSelected)
            OnAudioAction();
    }

    #endregion
}
