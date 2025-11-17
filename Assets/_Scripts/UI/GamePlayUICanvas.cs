using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUICanvas : UICanvas
{
    #region --- Overrides ---

    public override void CloseDirectly()
    {
        PoolManager.Instance.CollectAll();

        UIManager.Instance.OpenUI<MenuUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Menu);

        base.CloseDirectly();
    }

    #endregion

    #region --- Unity methods ---

    private void OnEnable()
    {
        ChangeCoins();
    }

    #endregion

    #region --- Methods ---

    private void ChangeCoins()
    {
        _txtCoins.text = GameManager.Instance.LevelData.coins.ToString();
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _txtCoins;

    #endregion
}
