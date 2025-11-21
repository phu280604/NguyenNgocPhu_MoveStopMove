using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region --- Unity methods ---

    private void Start()
    {
        _groundSize = ((int)_ground.localScale.x / 2) - GROUND_SIZE;
    }

    #endregion

    #region --- Methods ---

    public void OnInit()
    {
        PoolManager.Instance.Spawn<PlayerC>(
            EPoolType.Player,
            Vector3.zero,
            Quaternion.identity
        );

        for (int i = 0; i < PoolManager.Instance.PoolAmount(EPoolType.Bot); i++)
        {
            BotC bot = PoolManager.Instance.Spawn<BotC>(
                EPoolType.Bot,
                new Vector3(Random.Range(-_groundSize, _groundSize), 0, Random.Range(-_groundSize, _groundSize)),
                Quaternion.identity
            );

            bot.gameObject.name = $"Bot #{count++}";
        }


        if (_cameraH == null)
        {
            _cameraH = GameObject
                .FindGameObjectWithTag(ETag.MainCamera.ToString())
                .GetComponent<FollowingObject>();
        }
        _cameraH.OnInit();
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private FollowingObject _cameraH;

    [SerializeField] private Transform _ground;

    private const int GROUND_SIZE = 3;
    private int _groundSize;

    private int count = 0;

    #endregion
}
