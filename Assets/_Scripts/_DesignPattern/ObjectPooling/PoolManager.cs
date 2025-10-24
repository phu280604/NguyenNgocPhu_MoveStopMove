using System.Collections;
using System.Collections.Generic;
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
        //Debug.Log(_poolInstance.Count);
        return _poolInstance[poolType].Spawn(position, rotation) as T;
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

    #region --- Fields ---

    private Dictionary<EPoolType, PoolUnit> _poolInstance = new Dictionary<EPoolType, PoolUnit>();

    #endregion
}
