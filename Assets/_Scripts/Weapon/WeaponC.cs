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

            other.gameObject.GetComponent<GameUnit>().OnDespawn();

            onAfterEliminating?.Invoke();
            AfterGetHit(charC);

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

    private void AfterGetHit(CharacterC charC)
    {
        if (charCtrl is PlayerC playerCtrl && charC != charCtrl)
        {
            //TODO: Triggered coin drops.
            if (charC is BotC botCtrl)
                playerCtrl.GetCoinDrop(botCtrl.StatsM.CoinDrops);
        }
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
