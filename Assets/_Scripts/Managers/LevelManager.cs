using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region --- Unity methods ---


    private void Start()
    {
        PoolManager.Instance.Spawn<PlayerC>(EPoolType.Player, Vector3.zero, Quaternion.identity);
    }

    #endregion
}
