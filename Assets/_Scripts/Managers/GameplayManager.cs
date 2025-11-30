using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    #region --- Methods ---

    #region -- Initialization --
    public void OnInit(bool isJusLoadData = false)
    {
        #region - Check conditions -
        if (isJusLoadData)
        {
            OnLoadData();
            return;
        }

        if(_levelData == null)
        {
            Debug.LogError("Level data can not be null!");
            return;
        }
        #endregion

        #region -- Handle --
        OnSpawnUnit();
        OnSetUpCamera();
        #endregion
    }
    #endregion

    #region -- Handle data --
    private void OnLoadData()
    {
        _levelData = LoadDataManager.Instance.Load<LevelSaveData>(StringCollection.LEVEL_DATA);
        if (_levelData == null)
        {
            _levelData = new LevelSaveData();
            SaveDataManager.Instance.Save<LevelSaveData>(_levelData, StringCollection.LEVEL_DATA);
        }
    }

    public void OnNextLevel(int nextLevelId)
    {
        // TODO: Next level.
        SetLevel(nextLevelId);
        GameManager.Instance.ChangeState(EGameStates.Victory);
    }
    #endregion

    #region -- Spawn handler --
    private void OnSpawnUnit()
    {
        MapC newMap = PoolManager.Instance.Spawn<MapC>(
            EPoolType.Maps,
            Vector3.zero,
            Quaternion.identity
        );

        if (newMap == null)
        {
            Debug.Log("hi");
            return;
        }

        newMap.OnInit(_levelData.levelId);
    }
    #endregion

    #region -- Camera handler --
    private void OnSetUpCamera()
    {
        if (_cameraH == null)
        {
            _cameraH = GameObject
                .FindGameObjectWithTag(ETag.MainCamera.ToString())
                .GetComponent<FollowingObject>();
        }
        _cameraH.OnInit();
    }
    #endregion

    #region -- Level data handler --
    public void SetCoin(int coin)
    {
        _levelData.coins += coin;
        SaveDataManager.Instance.Save<LevelSaveData>(_levelData, StringCollection.LEVEL_DATA);
    }

    public void SetLevel(int level)
    {
        _levelData.levelId = level;
        SaveDataManager.Instance.Save<LevelSaveData>(_levelData, StringCollection.LEVEL_DATA);
    }
    #endregion

    #endregion

    #region --- Properties ---

    public Subject<EEventKey, object> GameplaySubject => GameManager.Instance.GameSubject;
    public int Coins => _levelData.coins;

    #endregion

    #region --- Fields ---

    [SerializeField] private FollowingObject _cameraH;

    [SerializeField] private Transform _ground;

    [SerializeField] private LevelSaveData _levelData;

    #endregion
}
