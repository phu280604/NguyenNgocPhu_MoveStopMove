using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUnit
{
    public PoolUnit(GameObject prefab, int amount, Transform parent)
    {
        _parent = parent;
        _prefab = prefab;

        BaseCount = amount;

        _unitInactive = new Queue<GameUnit>(amount);
        _unitActive = new List<GameUnit>();
    }

    #region --- Methods ---

    public GameUnit Spawn(Vector3 position, Quaternion rotation)
    {
        if (UnitActiveCount > BaseCount && BaseCount != 0)
            return null;

        GameUnit unit;

        if (_unitInactive.Count <= 0)
        {
            unit = GameObject.Instantiate(_prefab, _parent).GetComponent<GameUnit>();
        }
        else
        {
            unit = _unitInactive.Dequeue();
        }

        unit.Parent.SetPositionAndRotation(position, rotation);
        unit.gameObject.SetActive(true);
        _unitActive.Add(unit);

        return unit;
    }

    public GameUnit Spawn(Transform parent, Vector3 position, Quaternion rotation)
    {
        GameUnit unit;

        if (_unitInactive.Count <= 0)
        {
            GameObject obj = GameObject.Instantiate(_prefab, _parent);
            obj.transform.SetParent(parent);

            unit = obj.GetComponent<GameUnit>();
        }
        else
        {
            unit = _unitInactive.Dequeue();
        }

        unit.Parent.SetPositionAndRotation(position, rotation);
        unit.gameObject.SetActive(true);
        _unitActive.Add(unit);

        return unit;
    }

    public void Despawn(GameUnit unit)
    {
        if(unit != null && unit.gameObject.activeSelf)
        {
            unit.gameObject.SetActive(false);
            _unitInactive.Enqueue(unit);
        }

        _unitActive.Remove(unit);
    }

    public void Collect()
    {
        while (_unitActive.Count > 0)
        {
            Despawn(_unitActive[0]);
        }
    }

    #endregion

    #region --- Properties ---

    public List<GameUnit> UnitActive => _unitActive;
    public int UnitActiveCount => _unitActive.Count;
    public int Count => _unitInactive.Count + _unitActive.Count;
    public int BaseCount { get; private set; }
    public Transform Parent => _parent;

    #endregion

    #region --- Fields ---

    private Transform _parent;
    private GameObject _prefab;

    private Queue<GameUnit> _unitInactive;
    private List<GameUnit> _unitActive;

    #endregion
}
