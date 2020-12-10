using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestView : MonoBehaviour
{
  public event Action Shown;
  public event Action Hidden;

  public virtual void Show()
  {
    gameObject.SetActive(true);
    Shown?.Invoke();
  }

  public virtual void Hide()
  {
    gameObject.SetActive(false);
    Hidden?.Invoke();
  }

  public virtual void OnCloseSelected()
  {
    Hide();
  }
}
