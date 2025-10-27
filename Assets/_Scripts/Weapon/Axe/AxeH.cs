using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeH : IWeaponHandler
{
    public Vector3 OnMove(Vector3 curPos, Vector3 targetPos, float speed)
    {
        targetPos.y = 0;
        return Vector3.MoveTowards(curPos, targetPos, speed);
    }

    public void OnRotation(ref Transform weaponRot, float speed)
    {
        weaponRot.Rotate(Vector3.up * speed);
    }
}
