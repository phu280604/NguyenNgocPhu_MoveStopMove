using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponC : GameUnit
{
    #region --- Methods ---

    public void OnInit(CharacterC ctrl)
    {
        charCtrl = ctrl;
    }

    #endregion

    #region --- Properties ---

    public Vector3 TargetPos { get; set; }

    #endregion

    #region --- Fields ---

    protected CharacterC charCtrl;

    #endregion
}
