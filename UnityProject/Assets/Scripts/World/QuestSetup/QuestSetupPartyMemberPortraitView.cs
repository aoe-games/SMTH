using Jalopy.ResourceManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSetupPartyMemberPortraitView : MonoBehaviour
{
    [SerializeField]
    Image m_portraitImg;
    [SerializeField]
    Image m_frameImg;    
    [SerializeField]
    Color m_neutralColour;
    [SerializeField]
    Color m_highlightedColour;

    public void SetPortraitImage(string imagePath)
    {
        m_portraitImg.sprite = ResourceManager.Instance.Inventory.GetRawResourceAtPath<Sprite>(imagePath);
    }

    public void SetHighlighted(bool isHighlighted)
    {
        m_frameImg.color = isHighlighted ? m_highlightedColour : m_neutralColour;
    }
}
