using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ShopSubject : Subject<EUIKey, object>
{
    #region --- Unity methods ---

    private void OnEnable()
    {
        SpawnVisual();
    }

    private void OnDisable()
    {
        CurrentIdItem = 0;
    }

    private void OnDestroy()
    {
        _observers.Clear();
    }

    #endregion

    #region --- Methods ---
    
    public void SpawnVisual()
    {
        _characterVisual = PoolManager.Instance.Spawn<ShopCharacterVisualC>(
            EPoolType.VisualObject,
            _visualOffet,
            Quaternion.LookRotation(Vector3.back)
        );

        _characterVisual.OnInit(this);
    }

    #endregion

    #region --- Properties ---

    public ShopCharacterVisualC CharacterVisual { get; private set; }
    public ItemDataConfig ItemDataConfig => _itemDataConfig;
    public int CurrentIdItem { get; set; }
    public EItemType CurrentItemType { get; set; }

    #endregion

    #region --- Fields ---

    [SerializeField] private ItemDataConfig _itemDataConfig;

    [SerializeField] private Vector3 _visualOffet;
    [SerializeField] private ShopCharacterVisualC _characterVisual;

    #endregion
}
