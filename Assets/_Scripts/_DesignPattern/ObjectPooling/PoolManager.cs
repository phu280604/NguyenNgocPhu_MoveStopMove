using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    #region --- Methods ---

    public void Preload(GameObject prefab, GameUnit unit, int amount, Transform parent = null)
    {
        if(prefab != null && !_poolInstance.ContainsKey(unit.PoolType))
        {
            _poolInstance.Add(unit.PoolType, new PoolUnit(prefab, amount, parent));
        }
    }

    public T Spawn<T>(EPoolType poolType, Vector3 position, Quaternion rotation) where T : GameUnit
    {
        return _poolInstance[poolType].Spawn(position, rotation) as T;
    }

    public T SpawnWithTransform<T>(EPoolType poolType, Transform parent, Vector3 position, Quaternion rotation) where T : GameUnit
    {
        return _poolInstance[poolType].Spawn(parent, position, rotation) as T;
    }

    public void Despawn(EPoolType poolType)
    {
        if(_poolInstance.ContainsKey(poolType))
        {
            _poolInstance[poolType].Collect();
        }
    }

    public void Despawn(GameUnit unit)
    {
        _poolInstance[unit.PoolType].Despawn(unit);
    }

    public void CollectAll()
    {
        foreach (PoolUnit pool in _poolInstance.Values)
        {
            pool.Collect();
        }
    }

    #endregion

    #region --- Properties ---

    public int PoolAmount(EPoolType type) => _poolInstance[type].BaseCount;

    #endregion

    #region --- Fields ---

    private Dictionary<EPoolType, PoolUnit> _poolInstance = new Dictionary<EPoolType, PoolUnit>();

    #endregion
}
