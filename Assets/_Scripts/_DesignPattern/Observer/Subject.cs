using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject<TKey, TData> : MonoBehaviour
{
    #region --- Methods ---

    public void AddObserver(TKey key, IObserver<TData> newObs)
    {
        Debug.Log("hello");
        if (!_observers.ContainsKey(key))
            _observers.Add(key, new List<IObserver<TData>>());
        

        if (newObs != null && !_observers[key].Contains(newObs))
            _observers[key].Add(newObs);
    }

    public void RemoveObserver(TKey key)
    {
        if (_observers.ContainsKey(key))
            _observers.Remove(key);
    }

    public void RemoveObserver(TKey key, IObserver<TData> obs)
    {
        if (_observers.ContainsKey(key))
        {
            _observers[key].Remove(obs);
            if (_observers[key].Count == 0)
                _observers.Remove(key);
        }
    }

    public void NotifyObservers(TKey key, TData data)
    {
        if (!_observers.ContainsKey(key)) return;

        foreach (IObserver<TData> observer in _observers[key])
            observer.OnNotify(data);
    }

    #endregion

    #region --- Fields ---

    protected Dictionary<TKey, List<IObserver<TData>>> _observers = new Dictionary<TKey, List<IObserver<TData>>>();

    #endregion
}
