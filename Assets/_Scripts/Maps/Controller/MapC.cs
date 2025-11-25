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
                if(CheckEnemiesCount())
                    NextLevel();
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

        // Set enemies count on UI.
        ResetEnemiesCount();

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
        _handler?.SpawnUnit<PlayerC>(
            EPoolType.Player,
            _model.GetRandomSpawnPos(),
            (d) => {
                if (d == null) return;

                d.gameObject.name = StringCollection.PLAYER_NAME;
                d.MapSubject = _subject;
            }
        );

        for(int i = 0; i < _model.CurrentMapData.LevelMaxEnemiesOnGround; i++)
        {
            _handler?.SpawnUnit<BotC>(
                EPoolType.Bot,
                _model.GetRandomSpawnPos(),
                (d) => {
                    if (d == null) return;

                    d.OnInit();
                    d.MapSubject = _subject;

                    _model.CurrentBotCount += 1;
                    d.gameObject.name = StringCollection.BOT_NAME + $" #{_model.CurrentBotCount}";
                }
            );
        }
        
    }
    #endregion

    #region -- Handle events --
    private void RespawnBot()
    {
        _handler?.SpawnUnit<BotC>(
            EPoolType.Bot,
            _model.GetRandomSpawnPos(),
            (d) => {
                if(d == null) return;

                d.OnInit();
                d.MapSubject = _subject;

                _model.CurrentBotCount += 1;
                d.gameObject.name = StringCollection.BOT_NAME + $" #{_model.CurrentBotCount}";
            }
        );
    }

    private void ResetEnemiesCount()
    {
        if(UIManager.Instance.BackTopUI is GamePlayUICanvas ui)
        {
            ui.Subject.NotifyObservers(
                    EUIGamePlayKey.EnemiesRemaining,
                    new KeyValuePair<int, int>(
                        key: _model.CurrentMapData.LevelMaxEnemiesCount,
                        value: 0
                    )
                );
        }

        _model.CurrentBotEliminatedCount = 0;
    }

    private bool CheckEnemiesCount()
    {
        _model.CurrentBotEliminatedCount += 1;

        if (UIManager.Instance.BackTopUI is GamePlayUICanvas ui)
            ui.Subject.NotifyObservers(
                EUIGamePlayKey.EnemiesRemaining,
                new KeyValuePair<int, int>(
                    key: _model.CurrentMapData.LevelMaxEnemiesCount,
                    value: _model.CurrentBotEliminatedCount
                )
            );

        if(_model.CurrentBotEliminatedCount >= _model.CurrentMapData.LevelMaxEnemiesCount)
            return true;

        return false;
    }

    private void NextLevel()
    {
        int newLevelId = _model.CurrentMapData.LevelId + 1;

        if (newLevelId > _model.MapSOs.Count)
        {
            newLevelId = _model.MapSOs[0].LevelId;
        }

        LevelManager.Instance.OnNextLevel(newLevelId);
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
