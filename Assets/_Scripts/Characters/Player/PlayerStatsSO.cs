using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "CharacterStats/Player")]
public class PlayerStatsSO : ScriptableObject
{
    public float maxMovementSpeed;
    public float acceleration;

    public float rotationSpeed;

    public float rangeAttack;

    public readonly float minAnimSpeed = 0.4f;
    public readonly float maxAnimSpeed = 1f;

    public readonly float triggeredAnimAttack = 0.5f;
}
