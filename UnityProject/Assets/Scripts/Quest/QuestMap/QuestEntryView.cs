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
  float defaultScale = 1.0f;
  [SerializeField]
  float completeScale = 0.5f;

  [SerializeField, Header("Available")]
  ColorBlock m_availableColours;
  [SerializeField, Header("In Progress")]
  ColorBlock m_inProgressColours;
  [SerializeField, Header("Resolved")]
  ColorBlock m_resolvedColours;
  [SerializeField, Header("Complete")]
  ColorBlock m_completeColours;  

  public void SetVisible(bool visible)
  {
    gameObject.SetActive(visible);
  }

  public void SetAvailable()
  {
    SetColours(m_availableColours);
    transform.localScale = new Vector3(defaultScale, defaultScale);
  }

  public void SetInProgress()
  {
    SetColours(m_inProgressColours);
    transform.localScale = new Vector3(defaultScale, defaultScale);
  }

  public void SetResolved()
  {
    SetColours(m_resolvedColours);
    transform.localScale = new Vector3(defaultScale, defaultScale);
  }

  public void SetComplete()
  {
    SetColours(m_completeColours);
    transform.localScale = new Vector3(completeScale, completeScale);
  }

  protected void SetColours(ColorBlock colourBlock)
  {
    m_entryBtn.colors = colourBlock;
    m_baseImg.color = colourBlock.highlightedColor;
  }
}
