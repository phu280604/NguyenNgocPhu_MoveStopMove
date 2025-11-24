using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BotStats", menuName ="CharacterStats/Bot")]
public class BotStatsSO : ScriptableObject
{
    #region --- Properties ---

    public Material GetRandomColorMats() => color_Materials[Random.Range(0, color_Materials.Count)];

    #endregion

    #region --- Fields ---

    [Header("Sprites & Materials")]
    public List<Material> color_Materials = new List<Material>();

    [Header("Stats")]
    public float maxMovingSpeed;
    public float rotationSpeed;
    public float rangeAttack;
    public float attackDelay;

    [Header("Rewards")]
    public int minCoins;
    public int maxCoins;

    public readonly float triggeredAnimAttack = 0.5f;

    #endregion
}
