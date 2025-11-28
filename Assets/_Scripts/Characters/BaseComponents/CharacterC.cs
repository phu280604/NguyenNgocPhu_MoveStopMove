using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class CharacterC : GameUnit
{
    #region --- Unity Methods ---

    private void Awake()
    {
        _audioSubject.AddObserver(EEventKey.Audio, _audioObserver);
    }

    #endregion

    #region --- Methods ---

    public abstract void OnInit();
    public virtual void OnDead()
    {
        StateM.IsDead = true;
    }
    protected abstract void CheckState();
    protected abstract void OnBuildRangeAttack();

    #endregion

    #region --- Properties ---

    public abstract ICharacterStateM StateM { get; }
    public Animator Animator => _animator;

    public Subject<EEventKey, object> AudioSubject => _audioSubject;

    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] protected Animator _animator;

    [Header("Observer components")]
    [SerializeField] private Subject<EEventKey, object> _audioSubject;
    [SerializeField] private AudioObserver _audioObserver;

    [Header("Character handler components")]
    [SerializeField] protected CharacterPhysicH _physicH;

    #endregion
}
