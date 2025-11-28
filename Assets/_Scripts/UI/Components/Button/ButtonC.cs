using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonC : MonoBehaviour, IAudioEvent
{
    #region --- Methods ---

    public void OnAudioAction()
    {
        GameManager.Instance.AudioSubject.NotifyObservers(EEventKey.Audio, EAudioKey.ButtonClick);
    }

    #endregion
}
