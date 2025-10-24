using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPhysicH : MonoBehaviour
{

    #region --- Unity methods ---

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    #endregion

    #region --- Methods ---

    public void BuildAttackRange(float radius, Action<Collider> setTarget)
    {
        _radius = radius;

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask(ELayer.Bot.ToString()));
        
        if(hits.Count() >= 1)
            setTarget?.Invoke(hits[0]);
        else
            setTarget?.Invoke(null);
    }

    #endregion

    #region --- Fields ---

    private float _radius;

    #endregion
}
