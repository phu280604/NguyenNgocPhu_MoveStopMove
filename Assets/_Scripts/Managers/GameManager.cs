using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region --- Unity methods ---

    private void Awake()
    {
        LevelData = LoadDataManager.Instance.Load<LevelData>(StringCollection.LEVEL_DATA);
        if(LevelData == null)
        {
            LevelData = new LevelData();
            SaveDataManager.Instance.Save<LevelData>(LevelData, StringCollection.LEVEL_DATA);
        }

        UIManager.Instance.OpenUI<MenuUICanvas>();

        ChangeState(EGameStates.Menu);
    }

    #endregion

    #region --- Methods ---

    public void ChangeState(EGameStates newState)
    {
        if(_gameState != newState)
            TriggeredState(newState);

        _gameState = newState;
    }

    private void TriggeredState(EGameStates newState)
    {
        switch (newState)
        {
            case EGameStates.Menu:
                MenuStateTriggered();
                break;
            case EGameStates.Shop:
                ShopStateTriggered();
                break;
            case EGameStates.GamePlay:
                GamePlayStateTriggered();
                break;
        }
    }

    private void MenuStateTriggered()
    {
        _camGamePlay.SetActive(true);
        _camShop.SetActive(false);
    }

    private void ShopStateTriggered()
    {
        _camGamePlay.SetActive(false);
        _camShop.SetActive(true);
    }

    private void GamePlayStateTriggered()
    {
        _camGamePlay.SetActive(true);
        _camShop.SetActive(false);

        LevelManager.Instance.OnInit();
    }

    public void SetCoin(int coin)
    {
        LevelData.coins = coin;
        SaveDataManager.Instance.Save<LevelData>(LevelData, StringCollection.LEVEL_DATA);
    }

    public void SetLevel(int level)
    {
        LevelData.levelId = level;
        SaveDataManager.Instance.Save<LevelData>(LevelData, StringCollection.LEVEL_DATA);
    }

    #endregion

    #region --- Properties ---

    public LevelData LevelData { get; private set; }

    #endregion

    #region --- Fields ---

    private EGameStates _gameState = EGameStates.Menu;

    [Header("Unity components")]
    [SerializeField] private GameObject _camGamePlay;
    [SerializeField] private GameObject _camShop;

    #endregion
}
