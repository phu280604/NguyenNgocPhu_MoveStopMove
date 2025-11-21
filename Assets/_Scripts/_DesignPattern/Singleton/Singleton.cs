using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region --- Properties ---

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    _instance.transform.parent = GameObject.FindWithTag(_tag.ToString()).transform;
                }
            }

            return _instance;
        }
    }

    #endregion

    #region --- Fields ---

    private static T _instance;
    private const ETag _tag = ETag.Manager;

    #endregion
}
