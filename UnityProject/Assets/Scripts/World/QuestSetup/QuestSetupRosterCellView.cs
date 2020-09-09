using Jalopy.ResourceManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSetupRosterCellView : InfiniteScrollCell
{
    [SerializeField]
    RectTransform m_rectTransform = null;
    [SerializeField]
    List<QuestSetupRosterCellPortraitView> m_portraitViews = new List<QuestSetupRosterCellPortraitView>();

    public event Action<int> OnCellSelectedEvent = null;

    public override Rect CellSize
    {
        get => m_rectTransform.rect;
    }

    // number of portraits that this parent view supports
    public int PortraitCount { get => m_portraitViews.Count; }

    public void SetImageForIndex(string resourceID, int index)
    {
        m_portraitViews[index].SetPortraitImage(resourceID);
        ShowPortraitAtIndex(true, index);
    }

    public void ShowPortraitAtIndex(bool shouldShow, int index)
    {
        m_portraitViews[index].SetVisible(shouldShow);
    }

    public void SetHighlighted(bool isHighlighted, int index)
    {
        m_portraitViews[index].SetHighlighted(isHighlighted);
    }

    public void SetSelectable(bool isSelectable, int index)
    {
        m_portraitViews[index].SetSelectable(isSelectable);
    }

    #region Event Triggers

    public void OnCellOneSelected()
    {
        FireCellSelectedEvent(0);
    }

    public void OnCellTwoSelected()
    {
        FireCellSelectedEvent(1);
    }

    public void OnCellThreeSelected()
    {
        FireCellSelectedEvent(2);
    }

    protected void FireCellSelectedEvent(int portraitOffset)
    {
        OnCellSelectedEvent?.Invoke((index * PortraitCount) + portraitOffset);
    }

    #endregion
}
