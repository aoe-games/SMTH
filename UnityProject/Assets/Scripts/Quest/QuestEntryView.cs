using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestEntryView : MonoBehaviour
{
  [SerializeField]
  Button m_entryBtn;
  [SerializeField]
  Image m_baseImg;

  [SerializeField]
  Color m_btnNormalIdle;
  [SerializeField]
  Color m_btnNormalHighlight;
  [SerializeField]
  Color m_btnCompleteIdle;
  [SerializeField]
  Color m_btnCompleteHighlight;

  [SerializeField]
  Color m_baseImgNormal;
  [SerializeField]
  Color m_baseImgComplete;

  public void SetComplete()
  {
    ColorBlock colourBlock = m_entryBtn.colors;
    colourBlock.normalColor = m_btnCompleteIdle;
    colourBlock.highlightedColor = m_btnCompleteHighlight;
    m_entryBtn.colors = colourBlock;

    m_baseImg.color = m_baseImgComplete;
  }
}
