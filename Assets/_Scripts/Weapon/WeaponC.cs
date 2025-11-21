using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponC : GameUnit
{
    #region --- Unity methods ---

    protected virtual void OnEnable()
    {
        OnDespawn(StatsSO.disableTime);
    }

    protected virtual void OnDisable()
    {
        StateM.HasTarget = false;

        CancelInvoke();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (StateM.HasHit) return;

        if (other.gameObject.CompareTag(ETag.Player.ToString()) || other.gameObject.CompareTag(ETag.Bot.ToString()))
        {
            if (charCtrl == other.gameObject.GetComponent<CharacterC>()) return;

            StateM.HasHit = true;

            other.gameObject.GetComponent<GameUnit>().OnDespawn();
            onAfterEliminating?.Invoke();
            OnDespawn();

            StateM.HasHit = false;
        }
    }

    #endregion

    #region --- Methods ---

    public void OnInit(CharacterC ctrl, Action onAfterEliminating)
    {
        charCtrl = ctrl;

        this.onAfterEliminating = onAfterEliminating;
    }

    #endregion

    #region --- Properties ---

    public WeaponStateM StateM { get; protected set; }
    public WeaponStatsSO StatsSO { get; protected set; }

    #endregion

    #region --- Fields ---

    protected CharacterC charCtrl;

    protected Action onAfterEliminating;

    #endregion
}
