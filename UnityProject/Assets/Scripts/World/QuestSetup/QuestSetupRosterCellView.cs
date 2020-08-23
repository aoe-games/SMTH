using Jalopy.ResourceManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSetupRosterCellView : InfiniteScrollCell
{
    [SerializeField]
    RectTransform m_rectTransform;
    [SerializeField]
    List<Button> m_portraitBtns = new List<Button>();

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
}
