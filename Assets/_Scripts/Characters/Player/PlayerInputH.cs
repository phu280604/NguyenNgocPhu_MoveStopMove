using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputH : MonoBehaviour
{
    #region --- Unity methods ---

    private void Start()
    {
        _control = GameObject.FindWithTag(ETag.Control.ToString()).GetComponent<Joystick>();
    }

    #endregion

    #region --- Methods ---

    public void GetInput(Action<Vector3> onAction)
    {
        Vector3 vec3D = Convert2Dto3D(_control.Direction);

        onAction?.Invoke(vec3D);
    }

    private Vector3 Convert2Dto3D(Vector2 vec2D) => new Vector3(vec2D.x, 0, vec2D.y);

    #endregion

    #region --- Fields ---

    [SerializeField] private Joystick _control;

    #endregion
}
