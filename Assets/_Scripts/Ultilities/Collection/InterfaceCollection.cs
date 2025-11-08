using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Character State ---
public interface ICharacterStateM
{
    #region --- Properties ---
    public EPoolType WeaponType { get; set; }
    public Transform Target { get; set; }
    public Transform SpawnWeaponPos { get; }
    public bool IsDelayAttack { get; set; }
    #endregion
}
#endregion

#region --- Weapon ---

public interface IWeaponHandler
{
    public Vector3 OnMove(Vector3 curPos, Vector3 targetPos, float speed);

    public void OnRotation(ref Transform weaponRot, float speed);
}
#endregion

#region --- Observer ---

public interface IObserver<T>
{
    public void Notify(T data);
}

public interface ISubject<T>
{
    public void AddObserver(IObserver<T> observer);
    public void RemoveObserver(IObserver<T> observer);
    public void NotifyObservers(T data);
}

#endregion
