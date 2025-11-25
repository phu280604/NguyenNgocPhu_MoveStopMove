using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapM : MonoBehaviour
{
    #region --- Methods ---

    public Vector3 GetRandomSpawnPos()
    {
        if(_curSpawnPositions.Count == 0 && SpawnPositions != null)
            _curSpawnPositions.AddRange(SpawnPositions);

        int index = Random.Range(0, _curSpawnPositions.Count - 1);

        Vector3 spawnPos = _curSpawnPositions[index].position;
        _curSpawnPositions.RemoveAt(index);

        return spawnPos;
    }

    #endregion

    #region --- Properties ---

    public int CurrentBotCount { get; set; } = 0;
    public int CurrentBotEliminatedCount { get; set; } = 0;
    public MapSO CurrentMapData { get; set; }
    public List<MapSO> MapSOs => _maps;
    public List<GameObject> Grounds => _grounds;
    public List<Transform> SpawnPositions { get; set; } = new List<Transform>();

    #endregion

    #region --- Fields ---

    [SerializeField] private List<MapSO> _maps = new List<MapSO>();

    [SerializeField] private List<GameObject> _grounds = new List<GameObject>();

    [SerializeField] private List<Transform> _curSpawnPositions = new List<Transform>();

    #endregion
}
