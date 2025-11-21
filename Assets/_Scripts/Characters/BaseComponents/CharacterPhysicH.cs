using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    public void BuildAttackRange(float radius, Vector3 position, string[] layers, CharacterC characterC)
    {
        _radius = radius;

        Collider[] hits = Physics.OverlapSphere(position, radius, LayerMask.GetMask(layers));

        for (int i = 0; i < hits.Length; i++)
        {
            CharacterC c = hits[i].GetComponent<CharacterC>();
            if (c != null && c != characterC)
            {
                characterC.StateM.Target = c.transform;
                return;
            }
        }

        characterC.StateM.Target = null;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private Color _gizmoColor;
    private float _radius;

    #endregion
}
