using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeH : IWeaponHandler
{
    #region --- Methods ---

    public Vector3 OnMove(Vector3 curPos, Vector3 targetPos, float speed)
    {
        targetPos.y = 0;
        Vector3 nextPos = curPos + targetPos;
        return Vector3.MoveTowards(curPos, nextPos, speed);
    }

    public void OnRotation(ref Transform weaponRot, float speed)
    {
        weaponRot.Rotate(Vector3.up * speed);
    }

    #endregion
}
