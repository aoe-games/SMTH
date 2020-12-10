using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInProgressViewCtrl : QuestViewCtrl
{
  QuestInProgressDisplayConfig m_config;
  QuestInProgressView m_inProgressView;

  Action TimerUpdateMethod;

  public void SetConfig(QuestInProgressDisplayConfig config)
  {
    m_config = config;
  }

  public override void StartDisplay()
  {
    m_inProgressView = m_questView as QuestInProgressView;
    TimerUpdateMethod = UpdateTimer;
    base.StartDisplay();
  }

  public override void EndDisplay()
  {
    TimerUpdateMethod = null;
    base.EndDisplay();
  }

  protected void Update()
  {
    TimerUpdateMethod?.Invoke();
  }

  protected void UpdateTimer()
  {
    TimeSpan timeLeft = m_config.QuestStateData.CompletionTime - DateTime.UtcNow;
    m_inProgressView.SetTimeLeft(timeLeft);
  }
}
