using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorView : InfiniteScrollCell
{
    [SerializeField]
    RectTransform m_rectTransform;
    [SerializeField]
    TMPro.TextMeshProUGUI m_nameTxt;
    [SerializeField]
    RectTransform m_healthRatioBar;

    public string Name
    {
        set => m_nameTxt.text = value;
    }

    public float HealthRatio
    {
        set => m_healthRatioBar.localScale = new Vector3(value,1,1);
    }

    public override Rect CellSize
    {
        get => m_rectTransform.rect;
    }

    /// <summary>
    /// Override this function to implement custom clear logic before the cell is recycled. 
    /// Note: This is only called if the infinite scroller has its clear on recycle flag set to true. 
    /// </summary>
    public override void ResetCell() { }
}
