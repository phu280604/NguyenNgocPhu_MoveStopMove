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
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private List<PoolAmountWithRoot> _poolAmountWithRoot;

    #endregion
}
