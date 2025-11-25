using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    #region --- Methods ---

    public bool IsLoaded<T>() where T : UICanvas
    {
        System.Type type = typeof(T);
        return _uiCanvas.ContainsKey(type) && _uiCanvas[type] != null;
    }

    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && _uiCanvas[typeof(T)].gameObject.activeInHierarchy;
    }

    public T GetUIPrefab<T>() where T : UICanvas
    {
        if (!_uiCanvasPrefab.ContainsKey(typeof(T)))
        {
            for(int i = 0; i < _uiResources.Length; i++)
            {
                if (_uiResources[i] is T)
                {
                    _uiCanvasPrefab[typeof(T)] = _uiResources[i];
                    break;
                }
            }
        }

        return _uiCanvasPrefab[typeof(T)] as T;
    }

    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            UICanvas canvas = Instantiate(GetUIPrefab<T>(), CanvasParentTF);
            _uiCanvas[typeof(T)] = canvas;
        }

        return _uiCanvas[typeof(T)] as T;
    }

    public T OpenUI<T>() where T : UICanvas
    {
        UICanvas canvas = GetUI<T>();

        canvas.Setup();
        canvas.Open();

        return canvas as T;
    }

    public void CloseUI<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().CloseDirectly();
        }
    }

    public void CloseUI<T>(float delayTime) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().Close(delayTime);
        }
    }

    public void CloseLastestUI()
    {
        BackTopUI.CloseDirectly();
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!_backCanvases.Contains(canvas))
        {
            if(BackTopUI != null)
                BackTopUI.CloseDirectly();

            _backCanvases.Add(canvas);
        }
        else
        {
            RemoveBackUI(canvas);
            _backCanvases.Add(canvas);
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!_backActionEvents.ContainsKey(canvas))
        {
            _backActionEvents.Add(canvas, action);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        _backCanvases.Remove(canvas);
    }

    public void ClearBackKey()
    {
        _backCanvases.Clear();
    }

    #endregion

    #region --- Properties ---

    public UICanvas BackTopUI
    {
        get
        {
            UICanvas canvas = null;
            if (_backCanvases.Count > 0)
            {
                canvas = _backCanvases[_backCanvases.Count - 1];
            }

            return canvas;
        }
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private UICanvas[] _uiResources;

    private Dictionary<System.Type, UICanvas> _uiCanvasPrefab = new Dictionary<System.Type, UICanvas>();

    private Dictionary<System.Type, UICanvas> _uiCanvas = new Dictionary<System.Type, UICanvas>();

    public Transform CanvasParentTF;

    private Dictionary<UICanvas, UnityAction> _backActionEvents = new Dictionary<UICanvas, UnityAction>();

    [SerializeField] private List<UICanvas> _backCanvases = new List<UICanvas>();

    #endregion
}
