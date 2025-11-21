using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class CharacterC : GameUnit
{
    #region --- Unity Methods ---



    #endregion

    #region --- Methods ---

    public abstract void OnInit();
    protected abstract void CheckState();
    protected abstract void OnBuildRangeAttack();

    #endregion

    #region --- Properties ---

    public abstract ICharacterStateM StateM { get; }
    public Animator Animator => _animator;

    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] protected Animator _animator;

    [Header("Character handler components")]
    [SerializeField] protected CharacterPhysicH _physicH;

    #endregion
}
