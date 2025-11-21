using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    #region --- Methods ---

    public void OnDespawn(float time)
    {
        Invoke(nameof(OnDespawn), time);
    }

    public void OnDespawn()
    {
        PoolManager.Instance.Despawn(this);
    }

    #endregion

    #region --- Properties ---

    public Transform Parent
    {
        get
        {
            if(_parent == null)
            {
                _parent = transform;
            }

            return _parent;
        }
    }

    public EPoolType PoolType => _poolType;

    #endregion

    #region --- Fields ---

    [Header("Enum components")]
    [SerializeField] private EPoolType _poolType;

    private Transform _parent;

    #endregion
}
