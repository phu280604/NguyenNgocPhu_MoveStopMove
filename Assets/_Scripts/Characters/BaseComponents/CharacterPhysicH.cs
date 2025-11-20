using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPhysicH : MonoBehaviour
{
    #region --- Unity methods ---

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    #endregion

    #region --- Methods ---

    public void BuildAttackRange(float radius, Vector3 position, string[] layers, Action<Collider> setTarget)
    {
        _radius = radius;

        Collider[] hits = Physics.OverlapSphere(position, radius, LayerMask.GetMask(layers));

        if (hits.Count() > 1)
            setTarget?.Invoke(hits[1]);
        else if (hits.Count() == 1)
            setTarget?.Invoke(hits[0]);
        else
            setTarget?.Invoke(null);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private Color _gizmoColor;
    private float _radius;

    #endregion
}
