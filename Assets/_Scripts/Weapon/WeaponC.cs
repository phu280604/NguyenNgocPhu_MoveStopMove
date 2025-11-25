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

        CharacterC charC = other.GetComponent<CharacterC>();

        if (other.gameObject.CompareTag(ETag.Player.ToString()) || other.gameObject.CompareTag(ETag.Bot.ToString()))
        {
            if (charCtrl == charC) return;

            StateM.HasHit = true;

            charC.OnDead();

            onAfterEliminating?.Invoke(AfterGetHit(charC));

            OnDespawn();

            StateM.HasHit = false;
        }
    }

    #endregion

    #region --- Methods ---

    public void OnInit(CharacterC ctrl, Action<int> onAfterEliminating)
    {
        charCtrl = ctrl;

        this.onAfterEliminating = onAfterEliminating;
    }

    private int AfterGetHit(CharacterC charC)
    {
        if (!(charC is BotC botCtrl)) return -1;

        return botCtrl.StatsM.CoinDrops;
    }

    #endregion

    #region --- Properties ---

    public WeaponStateM StateM { get; protected set; }
    public WeaponStatsSO StatsSO { get; protected set; }

    #endregion

    #region --- Fields ---

    protected CharacterC charCtrl;

    protected Action<int> onAfterEliminating;

    #endregion
}
