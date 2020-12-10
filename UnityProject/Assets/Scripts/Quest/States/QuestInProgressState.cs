using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInProgressState : QuestState
{
  [SerializeField]
  protected QuestInProgressViewCtrl m_questInProgressCtrl = null;
  protected QuestStateID m_stateToSwitchTo;

  protected override void SetStateID()
  {
    m_stateID = (int)QuestStateID.InProgess;
  }

  public override void UpdateState(float deltaTime) { }

  public override void OnEnter()
  { 
    QuestInProgressDisplayConfig config = new QuestInProgressDisplayConfig(QuestCtrl.SelectedQuestStateData);
    m_questInProgressCtrl.SetConfig(config);
    m_questInProgressCtrl.DisplayEnded += OnInProgressDisplayFinished;
    m_questInProgressCtrl.StartDisplay();

    m_stateToSwitchTo = QuestStateID.Idle;
  }

  public override void OnExit()
  {
    QuestCtrl.QuestStateDatabase.QuestStateChangedEvent -= OnQuestStateChanged;
    m_questInProgressCtrl.DisplayEnded -= OnInProgressDisplayFinished;
  }

  public override void OnQuestStateChanged(QuestStateData stateData)
  {
    if (QuestCtrl.SelectedQuestStateData.QuestStatus == QuestStateData.Status.Resolved)
    {
      m_stateToSwitchTo = QuestStateID.Results;
      m_questInProgressCtrl.EndDisplay();     
    }
  }

  void OnInProgressDisplayFinished()
  {
    SwitchState(m_stateToSwitchTo);
  }
}
