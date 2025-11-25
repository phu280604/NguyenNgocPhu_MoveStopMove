using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region --- Unity methods ---

    private void Awake()
    {
        OnInit();
    }

    #endregion

    #region --- Methods ---

    public void OnInit()
    {
        LevelManager.Instance.OnInit(true);
        UIManager.Instance.OpenUI<MenuUICanvas>();

        ChangeState(EGameStates.Menu);
    }

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
            case EGameStates.Losing:
                LosingStateTriggered();
                break;
            case EGameStates.Victory:
                VictoryStateTriggered();
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

        UIManager.Instance.OpenUI<GamePlayUICanvas>();
        LevelManager.Instance.OnInit();
    }

    private void LosingStateTriggered()
    {
        UIManager.Instance.OpenUI<LosingUICanvas>();
    }

    private void VictoryStateTriggered()
    {
        UIManager.Instance.OpenUI<VictoryUICanvas>();
    }

    #endregion

    #region --- Fields ---

    private EGameStates _gameState = EGameStates.Menu;

    [Header("Unity components")]
    [SerializeField] private GameObject _camGamePlay;
    [SerializeField] private GameObject _camShop;

    #endregion
}
