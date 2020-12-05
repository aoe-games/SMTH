using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultView : MonoBehaviour
{
  [SerializeField]
  RectTransform m_resultMessage;

  public event Action Shown;
  public event Action Hidden;

  public void Show()
  {
    gameObject.SetActive(true);
    Shown?.Invoke();
  }

  public void Hide()
  {
    gameObject.SetActive(false);
    Hidden?.Invoke();
  }

  public void OnCloseSelected()
  {
    Hide();
  }
}
