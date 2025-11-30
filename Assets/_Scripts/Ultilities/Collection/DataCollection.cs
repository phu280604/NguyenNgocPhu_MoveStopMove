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

#region --- Particle ---

[System.Serializable]
public class ParticleData
{
    public ParticleData(EParticle particleType, Vector3 position, Quaternion rotation)
    {
        this.particleType = particleType;
        this.position = position;
        this.rotation = rotation;
    }

    #region --- Fields ---

    public EParticle particleType;
    public Vector3 position;
    public Quaternion rotation;

    #endregion
}

#endregion
