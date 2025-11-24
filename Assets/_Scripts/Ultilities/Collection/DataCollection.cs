using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region --- Level ---

[System.Serializable]
public class LevelSaveData
{
    #region --- Fields ---

    public int levelId = 1;
    public int coins = 0;

    #endregion
}

[System.Serializable]
public class LevelData
{
    public int levelId;
    public int maxEnemiesGroundedInTime;
    public int maxEnemiesCount;
}

#endregion
