using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObserver : MonoBehaviour, IObserver<object>
{
    #region --- Overrides ---

    public void OnNotify(object data)
    {
        if (!(data is ParticleData d)) return;

        switch (d.particleType)
        {
            case EParticle.BloodParticle:
                PoolManager.Instance.Spawn<ParticleC>(EPoolType.BloodParticle, d.position, d.rotation);
                break;
        }
    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        GameManager.Instance.GameSubject.AddObserver(EEventKey.Particle, this);
    }

    #endregion
}