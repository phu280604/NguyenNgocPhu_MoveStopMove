using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapH : MonoBehaviour
{
    #region --- Methods ---

    /// <summary>
    /// Turn on ground by level Id.
    /// </summary>
    /// <param name="levelId"></param>
    /// <param name="grounds"></param>
    public void SetUpGroundByLevelId(int levelId, List<GameObject> grounds, Action<TerrainM> onAction)
    {
        if(levelId >= grounds.Count)
        {
            Debug.LogError("Over range grounds");
            return;
        }

        foreach (GameObject ground in grounds)
        {
            if(ground.activeSelf)
                ground.SetActive(false);
        }

        grounds[levelId - 1].SetActive(true);
        onAction?.Invoke(grounds[levelId - 1]?.GetComponent<TerrainM>());
    }

    /// <summary>
    /// Set current level data.
    /// </summary>
    /// <param name="levelId"></param>
    /// <param name="maps"></param>
    /// <param name="onAction"></param>
    public void SetCurrentLevelDataByLevelId(int levelId, List<MapSO> maps, Action<MapSO> onAction)
    {
        if (levelId >= maps.Count)
        {
            Debug.LogError("Over range maps");
            return;
        }

        foreach (MapSO map in maps)
        {
            if(map.LevelId == levelId)
            {
                onAction?.Invoke(map);
                return;
            }
        }
    }

    public void SpawnUnit<T>(EPoolType poolType, Vector3 spawnPos, Action<T> onAction = null) where T : GameUnit
    {
        spawnPos.y = 0;

        T unit = PoolManager.Instance.Spawn<T>(
                poolType,
                spawnPos,
                Quaternion.identity
            );

        onAction?.Invoke(unit);
    }

    #endregion
}
