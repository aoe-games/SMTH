using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultCtrl : MonoBehaviour
{
  [SerializeField]
  QuestResultView m_victoryView;
  [SerializeField]
  QuestResultView m_defeatView;

  QuestResultView m_activeResultView;

  public event Action ResultDisplayWillStart = null;
  public event Action ResultDisplayStarted = null;
  public event Action ResultDisplayWillEnd = null;
  public event Action ResultDisplayEnded = null;

  public void StartResultsDisplay(ResultDisplayConfig resultViewModel)
  {
    // ... ensure the results view has what it needs

    ResultDisplayWillStart?.Invoke();

    // ... load and run any animations to setup screen

    m_activeResultView = resultViewModel.State == ResultDisplayConfig.ResultState.Victory ? m_victoryView : m_defeatView;
    m_activeResultView.Shown += OnViewShown;
    m_activeResultView.Show();
  }

  void OnViewShown()
  {
    m_activeResultView.Shown -= OnViewShown;
    ResultDisplayStarted?.Invoke();
  }

  public void EndResultsDisplay()
  {
    // ... setup for ending process and let listeners know the results view will close

    ResultDisplayWillEnd?.Invoke();

    // ... unloading logic and view animations

    m_activeResultView.Hidden += OnViewHidden;
    m_activeResultView.Hide();
  }

  void OnViewHidden()
  {
    m_activeResultView.Hidden -= OnViewHidden;
    m_activeResultView = null;
    ResultDisplayEnded?.Invoke();
  }
}
