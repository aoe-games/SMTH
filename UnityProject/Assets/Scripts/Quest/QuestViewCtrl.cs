using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestViewCtrl : MonoBehaviour
{
  [SerializeField]
  protected QuestView m_questView;

  public event Action DisplayWillStart = null;
  public event Action DisplayStarted = null;
  public event Action DisplayWillEnd = null;
  public event Action DisplayEnded = null;

  public virtual void StartDisplay()
  {
    // ... ensure the results view has what it needs

    DisplayWillStart?.Invoke();

    // ... load and run any animations to setup screen

    m_questView.Shown += OnViewShown;
    m_questView.Show();
  }

  protected virtual void OnViewShown()
  {
    m_questView.Shown -= OnViewShown;
    m_questView.Hidden += OnViewHidden;
    DisplayStarted?.Invoke();
  }

  public virtual void EndDisplay()
  {
    // ... setup for ending process and let listeners know the results view will close

    DisplayWillEnd?.Invoke();

    // ... unloading logic and view animations

    m_questView.Hide();
  }

  protected virtual void OnViewHidden()
  {
    m_questView.Hidden -= OnViewHidden;
    DisplayEnded?.Invoke();
  }
}
