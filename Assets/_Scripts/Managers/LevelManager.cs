using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region --- Unity methods ---

    private void Start()
    {

    }

    #endregion

    #region --- Methods ---

    public void OnInit()
    {
        // TODO: Open spawn player from pool.
        //PoolManager.Instance.Spawn<PlayerC>(
        //    EPoolType.Player,
        //    Vector3.zero,
        //    Quaternion.identity
        //);

        int size = (int)_ground.localScale.x - GROUND_SIZE;
        for (int i = 0; i < PoolManager.Instance.PoolAmount(EPoolType.Bot); i++)
        {
            PoolManager.Instance.Spawn<BotC>(
                EPoolType.Bot,
                new Vector3(Random.Range(-size, size), 0, Random.Range(-size, size)),
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

    [SerializeField] private Transform _ground;
    private const int GROUND_SIZE = 10;

    #endregion
}
