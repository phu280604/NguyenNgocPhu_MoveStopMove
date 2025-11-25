using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapC : GameUnit, IObserver<EMapKey>
{
    #region --- Overrides ---

    public void OnNotify(EMapKey data)
    {
        switch (data)
        {
            case EMapKey.RespawnBot:
                RespawnBot();
                break;
            case EMapKey.NextLevel:
                break;
        }
    }

    #endregion

    #region --- Unity methods ---

    private void OnDisable()
    {
        _model.CurrentMapData = null;
        _model.CurrentBotCount = 0;
    }

    #endregion

    #region --- Methods ---

    #region -- Initialization --
    public void OnInit(int levelId)
    {
        // Handle level.
        OnHandleLevel(levelId);

        // Handle unit.
        OnHandleSpawnUnit();

        _subject.AddObserver(ELevelEventKey.Map, this);
    }
    #endregion

    #region -- Level handler --
    private void OnHandleLevel(int levelId)
    {
        _handler.SetUpGroundByLevelId(levelId, _model.Grounds, (d) => {
            if (d != null)
            {
                _model.SpawnPositions = d.spawnPos;
            }
        });
        _handler.SetCurrentLevelDataByLevelId(levelId, _model.MapSOs, (d) => { _model.CurrentMapData = d; });
    }
    #endregion

    #region -- Spawn unit handler --
    private void OnHandleSpawnUnit()
    {
        _handler.SpawnUnit<PlayerC>(
            EPoolType.Player,
            _model.GetRandomSpawnPos(),
            (d) => {
                d.gameObject.name = StringCollection.PLAYER_NAME;
                d.MapSubject = _subject;
                Debug.Log("hello");
            }
        );

        

        for(int i = 0; i < _model.CurrentMapData.LevelMaxEnemiesOnGround; i++)
        {
            _handler.SpawnUnit<BotC>(
                EPoolType.Bot,
                _model.GetRandomSpawnPos(),
                (d) => {
                    _model.CurrentBotCount += 1;
                    d.gameObject.name = StringCollection.BOT_NAME + $" #{_model.CurrentBotCount}";
                    d.MapSubject = _subject;
                }
            );
        }
        
    }
    #endregion

    #region -- Handle events --
    private void RespawnBot()
    {
        if(_model.CurrentBotCount >= _model.CurrentMapData.LevelMaxEnemiesCount)
            return;

        if(PoolManager.Instance.PoolActiveAmount(EPoolType.Bot) < _model.CurrentMapData.LevelMaxEnemiesOnGround)
            _handler.SpawnUnit<BotC>(
                EPoolType.Bot,
                _model.GetRandomSpawnPos(),
                (d) => {
                    _model.CurrentBotCount += 1;
                    d.gameObject.name = StringCollection.BOT_NAME + $" #{_model.CurrentBotCount}";
                }
            );
    }

    private void NextLevel()
    {
        LevelManager.Instance.OnNextLevel();
    }
    #endregion

    #endregion

    #region --- Fields ---

    [Header("Handler components")]
    [SerializeField] private MapH _handler;

    [Header("Model components")]
    [SerializeField] private MapM _model;

    [Header("Observer components")]
    [SerializeField] private Subject<ELevelEventKey, EMapKey> _subject;

    #endregion
}
