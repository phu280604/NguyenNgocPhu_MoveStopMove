using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag(_eTag.ToString()).transform;
    }

    private void Update()
    {
        OnFollow(Time.deltaTime);
    }

    #endregion

    #region --- Methods ---

    // Check position of target and this object
    private bool CheckPosition()
    {
        if (transform.position != _target.position + _offset)
            return true;

        return false;
    }

    // Follow target with offset
    private void OnFollow(float time)
    {
        bool canFollow = CheckPosition();

        if (!canFollow && _isFollowing)
        {
            transform.position = _target.position + _offset;
            _isFollowing = false;
            return;
        }

        if (canFollow)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, time * _moveSpeed);
            _isFollowing = true;
            return;
        }
    }

    #endregion

    #region --- Fields ---

    [Header("Unity components")]
    [SerializeField] private Transform _target;

    [Header("Custom components")]
    [SerializeField] private ETag _eTag;

    [Header("Variables")]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private bool _isFollowing = true;
    [SerializeField] private float _moveSpeed;

    #endregion
}
