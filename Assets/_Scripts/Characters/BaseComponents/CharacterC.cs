using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterC : GameUnit
{
    #region --- Unity Methods ---



    #endregion

    #region --- Methods ---

    public abstract void OnInit();

    #endregion

    #region --- Properties ---

    public abstract ICharacterStateM StateM { get; }
    public Animator Animator => _animator;

    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] protected Animator _animator;

    #endregion
}
