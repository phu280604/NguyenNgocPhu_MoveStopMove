using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponC : GameUnit
{
    #region --- Unity methods ---

    protected void OnTriggerEnter(Collider other)
    {
        if (StateM.HasHit) return;

        if (other.gameObject.CompareTag(ETag.Player.ToString()) || other.gameObject.CompareTag(ETag.Bot.ToString()))
        {
            if (charCtrl == other.gameObject.GetComponent<CharacterC>()) return;

            StateM.HasHit = true;

            other.gameObject.GetComponent<GameUnit>().OnDespawn();
            OnDespawn();

            StateM.HasHit = false;
        }
    }

    #endregion

    #region --- Methods ---

    public void OnInit(CharacterC ctrl)
    {
        charCtrl = ctrl;
    }

    #endregion

    #region --- Properties ---

    public WeaponStateM StateM { get; protected set; }

    #endregion

    #region --- Fields ---

    protected CharacterC charCtrl;

    #endregion
}
