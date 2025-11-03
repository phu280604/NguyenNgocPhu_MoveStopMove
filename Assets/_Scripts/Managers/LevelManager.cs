using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region --- Unity methods ---

    private void Start()
    {
        //OnInit();
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
            PoolManager.Instance.Spawn<BotC>(
                EPoolType.Bot,
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                Quaternion.identity
            );
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

    #endregion
}
