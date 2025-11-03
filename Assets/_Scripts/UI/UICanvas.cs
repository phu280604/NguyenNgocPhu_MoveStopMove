using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    #region --- Unity methods ---

    private void Start()
    {
        OnInit();
    }

    #endregion

    #region --- Methods ---

    protected virtual void OnInit()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();

        float ratio = (float)Screen.height / (float)Screen.width;
        if (IsHandlingRabbitEars)
        {
            if(ratio > 2.1f)
            {
                Vector2 leftBottom = m_RectTransform.offsetMin;
                Vector2 rightTop = m_RectTransform.offsetMax;
                rightTop.y = -100f;
                m_RectTransform.offsetMax = rightTop;
                leftBottom.y = 0f;
                m_RectTransform.offsetMin = leftBottom;
                m_OffsetY = 100f;
            }
        }

        if (IsWidescreenProcessing)
        {
            ratio = (float)Screen.width / (float)Screen.height;
            if(ratio < 2.1f)
            {
                float ratioDefault = 850 / 1920f;
                float ratioThis = ratio;

                float value = 1 - (ratioThis - ratioDefault);

                float with = m_RectTransform.rect.width * value;

                m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, with);
            }
        }

        for(int i = 0; i < popups.Length; i++)
        {
            popups[i].ParentsPopup = this;
        }
    }

    /// <summary>
    /// Setup UICanvas into UIManager.
    /// </summary>
    public virtual void Setup()
    {
        UIManager.Instance.AddBackUI(this);
        UIManager.Instance.PushBackAction(this, BackKey);
    }

    /// <summary>
    /// BackKey for android.
    /// </summary>
    public virtual void BackKey()
    {
        UIManager.Instance.BackTopUI?.CloseDirectly();
    }

    /// <summary>
    /// Open canvas.
    /// </summary>
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Close Directly UICanvas.
    /// </summary>
    public virtual void CloseDirectly()
    {
        UIManager.Instance.RemoveBackUI(this);
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Close UICanvas after a period time.
    /// </summary>
    /// <param name="delayTime">a delay time before function 'CloseDirectly' triggered. </param>
    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

    public T GetPopup<T>() where T : UICanvas
    {
        T ui = null;

        for (int i = 0; i < popups.Length; i++)
        {
            if (popups[i] is T)
            {
                ui = popups[i] as T;
                break;
            }
        }

        return ui;
    }

    public T OpenPopup<T>() where T : UICanvas
    {
        T ui = GetPopup<T>();

        ui.Setup();
        ui.Open();

        return ui;
    }

    public bool IsOpenedPopup<T>() where T : UICanvas
    {
        return GetPopup<T>().gameObject.activeSelf;
    }

    public void ClosePopup<T>(float delayTime) where T : UICanvas
    {
        GetPopup<T>().Close(delayTime);
    }

    public void ClosePopupDirect<T>() where T : UICanvas
    {
        GetPopup<T>().CloseDirectly();
    }

    public void CloseAllPopup()
    {
        for(int i = 0; i < popups.Length; i++)
        {
            popups[i].CloseDirectly();
        }
    }

    #endregion

    #region --- Properties ---

    public UICanvas ParentsPopup { get; set; }

    #endregion

    #region --- Fields ---

    public bool IsDestroyOnClose = false;
    public bool IsHandlingRabbitEars = false;
    public bool IsWidescreenProcessing = false;

    protected RectTransform m_RectTransform;

    private Animator m_Animator;

    private float m_OffsetY = 0;

    [Header("Popup Child")]
    [SerializeField] UICanvas[] popups;

    #endregion
}
