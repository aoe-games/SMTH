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
    List<Button> m_portraitBtns = new List<Button>();

    public event Action<int> OnCellSelectedEvent = null;

    public override Rect CellSize
    {
        get => m_rectTransform.rect;
    }

    // number of portraits that this parent view supports
    public int PortraitCount { get => m_portraitBtns.Count; }

    public void SetImageForIndex(string resourceID, int index)
    {
        //m_portraitBtns[index].image.sprite = Resources.Load<Sprite>(resourceID);
        m_portraitBtns[index].image.sprite = ResourceManager.Instance.Inventory.GetRawResourceAtPath<Sprite>(resourceID);
        ShowPortraitAtIndex(true, index);
    }

    public void ShowPortraitAtIndex(bool shouldShow, int index)
    {
        m_portraitBtns[index].image.enabled = shouldShow;
    }

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
}
