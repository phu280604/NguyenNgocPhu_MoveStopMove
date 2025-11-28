using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUICanvas : UICanvas, IObserver<object>
{
    #region --- Overrides ---

    public override void CloseDirectly()
    {
        base.CloseDirectly();
    }

    public void OnNotify(object data)
    {
        if (data is int d)
        {
            _txtCoins.text = d.ToString();
        }
        else if (data is KeyValuePair<int, int> kv)
        {
            _txtEnemies.text = $"{kv.Value}/{kv.Key}";
        }
    }

    #endregion

    #region --- Unity methods ---

    private void Awake()
    {
        _subject.AddObserver(EUIGamePlayKey.TextCoins, this);
        _subject.AddObserver(EUIGamePlayKey.EnemiesRemaining, this);
    }

    private void OnEnable()
    {
        _subject.NotifyObservers(EUIGamePlayKey.TextCoins, GameplayManager.Instance.Coins);
    }

    #endregion

    #region --- Methods ---

    public void OnBacktoHome()
    {
        PoolManager.Instance.Despawn(EPoolType.Maps);
        PoolManager.Instance.Despawn(EPoolType.Player);
        PoolManager.Instance.Despawn(EPoolType.Bot);

        UIManager.Instance.OpenUI<MenuUICanvas>();

        GameManager.Instance.ChangeState(EGameStates.Menu);
    }

    #endregion

    #region --- Properties ---

    public Subject<EUIGamePlayKey, object> Subject => _subject;

    #endregion

    #region --- Fields ---

    [SerializeField] private Subject<EUIGamePlayKey, object> _subject;

    [SerializeField] private TextMeshProUGUI _txtCoins;
    [SerializeField] private TextMeshProUGUI _txtEnemies;

    #endregion
}
