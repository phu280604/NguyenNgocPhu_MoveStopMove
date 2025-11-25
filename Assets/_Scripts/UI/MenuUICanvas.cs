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
        GameManager.Instance.ChangeState(EGameStates.Shop);

        UIManager.Instance.OpenUI<ShopUICanvas>();
    }

    public void OpenGamePlayUI()
    {
        GameManager.Instance.ChangeState(EGameStates.GamePlay);
    }

    private void ChangeCoin()
    {
        _txtCoin.text = LevelManager.Instance.Coins.ToString();
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _txtCoin;

    private LevelSaveData _levelData;

    #endregion
}
