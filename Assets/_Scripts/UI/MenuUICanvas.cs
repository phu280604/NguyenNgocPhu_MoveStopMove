using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUICanvas : UICanvas
{
    #region --- Overrides ---

    protected override void OnInit()
    {
        base.OnInit();
    }

    #endregion

    #region --- Unity methods ---

    private void OnEnable()
    {
        ChangeCoin();
    }

    #endregion

    #region --- Methods ---

    public void OpenShopUI()
    {
        UIManager.Instance.BackTopUI?.CloseDirectly();
        UIManager.Instance.OpenUI<ShopUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Shop);
    }

    public void OpenGamePlayUI()
    {
        UIManager.Instance.BackTopUI?.CloseDirectly();
        UIManager.Instance.OpenUI<GamePlayUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.GamePlay);
    }

    private void ChangeCoin()
    {
        _txtCoin.text = GameManager.Instance.LevelData.coins.ToString();
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _txtCoin;

    private LevelData _levelData;

    #endregion
}
