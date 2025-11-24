using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Map", menuName ="LevelGame/Map")]
public class MapSO : ScriptableObject
{
    #region --- Properties ---

    public int LevelId => _levelData.levelId;
    public int LevelMaxEnemiesOnGround => _levelData.maxEnemiesGroundedInTime;
    public int LevelMaxEnemiesCount => _levelData.maxEnemiesCount;

    #endregion

    #region --- Fields ---

    [SerializeField] private LevelData _levelData;

    #endregion
}
