using Jalopy.ResourceManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRosterCellPortraitView : MonoBehaviour
{
    [SerializeField]
    Button m_portraitBtn = null;
    [SerializeField]
    Image m_frameImg = null;

    public bool IsVisible { get; private set; } = true;
    public bool IsHighlighted { get; private set; } = false;
    public bool IsSelectable { get; private set; } = true;

    public void SetVisible(bool isVisible)
    {
        IsVisible = isVisible;
        m_portraitBtn.image.enabled = isVisible;
        m_frameImg.enabled = isVisible && IsHighlighted;
    }

    public void SetHighlighted(bool isHighlighted)
    {
        IsHighlighted = isHighlighted;
        m_frameImg.enabled = IsVisible && isHighlighted;
    }

    public void SetSelectable(bool isSelectable)
    {
        IsSelectable = isSelectable;
        m_portraitBtn.interactable = IsVisible && isSelectable;
    }

    public void SetPortraitImage(string imagePath)
    {
        m_portraitBtn.image.sprite = ResourceManager.Instance.Inventory.GetRawResourceAtPath<Sprite>(imagePath);
    }            
}
