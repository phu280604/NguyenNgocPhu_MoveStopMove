using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCache : MonoBehaviour
{
    #region --- Nestest classes ---

    [System.Serializable]
    class PoolAmountWithRoot
    {
        [Header("Unity components")]
        public Transform parent;
        public GameObject prefab;

        [Header("Enum components")]
        public EPoolType poolType;

        public int amount;
    }

    [System.Serializable]
    class PoolAmountWithoutRoot
    {
        [Header("Unity components")]
        public GameObject prefab;

        [Header("Enum components")]
        public EPoolType poolType;
    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        for(int i = 0; i < _poolAmountWithRoot.Count; i++)
        {
            PoolManager.Instance.Preload(
                _poolAmountWithRoot[i].prefab,
                _poolAmountWithRoot[i].prefab.GetComponent<GameUnit>(),
                _poolAmountWithRoot[i].amount,
                _poolAmountWithRoot[i].parent
            );
        }

        for (int i = 0; i < _poolAmountWithoutRoot.Count; i++)
        {
            PoolManager.Instance.Preload(
                _poolAmountWithoutRoot[i].prefab,
                _poolAmountWithoutRoot[i].prefab.GetComponent<GameUnit>(),
                0,
                null
            );
        }
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private List<PoolAmountWithRoot> _poolAmountWithRoot;
    [SerializeField] private List<PoolAmountWithoutRoot> _poolAmountWithoutRoot;

    #endregion
}
