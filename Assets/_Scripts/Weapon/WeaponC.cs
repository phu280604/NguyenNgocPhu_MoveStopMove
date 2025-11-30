using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponC : GameUnit
{
    #region --- Unity methods ---

    protected virtual void Update()
    {
        OnActionPhysic();
    }

    protected virtual void OnEnable()
    {
        audioSubject.NotifyObservers(EEventKey.Audio, EAudioKey.ThrowObject);
        OnDespawn(StatsSO.disableTime);
    }

    protected virtual void OnDisable()
    {
        OnShowWeapon();
        CancelInvoke();

        StateM.HasTarget = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (StateM.HasHit) return;

        CharacterC charC = other.GetComponent<CharacterC>();

        if (other.gameObject.CompareTag(ETag.Player.ToString()) || other.gameObject.CompareTag(ETag.Bot.ToString()))
        {
            if (charCtrl == charC) return;

            StateM.HasHit = true;

            OnHideWeapon();
            OnCallAudio();
            OnSpawnParticle(
                other.ClosestPoint(transform.position), 
                Quaternion.LookRotation(Vector3.up)
            );

            charC.OnDead();

            onAfterEliminating?.Invoke(AfterGetHit(charC));

            OnDespawn(1f);

            StateM.HasHit = false;
        }
    }

    #endregion

    #region --- Methods ---

    public void OnInit(CharacterC ctrl, Action<int> onAfterEliminating)
    {
        charCtrl = ctrl;

        this.onAfterEliminating = onAfterEliminating;
    }

    #region -- Action when hit --
    protected void OnCallAudio()
    {
        audioSubject.NotifyObservers(EEventKey.Audio, EAudioKey.HitObject);
    }

    protected void OnSpawnParticle(Vector3 pos, Quaternion rot)
    {
        GameManager.Instance.GameSubject.NotifyObservers(
            EEventKey.Particle,
            new ParticleData(EParticle.BloodParticle, pos, rot)
        );
    }

    private int AfterGetHit(CharacterC charC)
    {
        if (!(charC is BotC botCtrl)) return -1;

        return botCtrl.StatsM.CoinDrops;
    }
    #endregion

    #region -- Physics --

    protected abstract void OnMove();
    protected abstract void OnRotation();

    protected void OnActionPhysic()
    {
        if (charCtrl == null) return;

        OnMove();
        OnRotation();
    }

    #endregion

    #region -- Visual --

    private void OnHideWeapon()
    {
        _collider.enabled = false;
        _view.enabled = false;
    }

    private void OnShowWeapon()
    {
        _collider.enabled = true;
        _view.enabled = true;
    }

    #endregion

    #endregion

    #region --- Properties ---

    public WeaponStateM StateM { get; protected set; }
    public WeaponStatsSO StatsSO { get; protected set; }

    #endregion

    #region --- Fields ---

    protected CharacterC charCtrl;

    protected Action<int> onAfterEliminating;

    [Header("Unity components")]
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _view;

    [Header("Observer components")]
    [SerializeField] protected Subject<EEventKey, object> audioSubject;
    [SerializeField] protected AudioObserver weaponObserver;

    protected IWeaponHandler _handler;

    #endregion
}
