using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region --- Unity methods ---


    private void Start()
    {
        PoolManager.Instance.Spawn<PlayerC>(EPoolType.Player, Vector3.zero, Quaternion.identity);

        if(_cameraH == null)
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
