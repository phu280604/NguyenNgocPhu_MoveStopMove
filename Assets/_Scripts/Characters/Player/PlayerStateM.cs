using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateM : MonoBehaviour
{
    #region --- Properties ---

    public Vector3 Direction { get; set; } = Vector3.zero;

    public Vector3 LastestDirection { get; set; } = Vector3.zero;

    #endregion
}
