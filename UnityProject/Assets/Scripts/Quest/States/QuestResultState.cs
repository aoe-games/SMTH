using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultState : QuestState
{
  [SerializeField]
  protected QuestResultViewCtrl m_questResultCtrl = null;

  protected override void SetStateID()
  {
    m_stateID = (int)QuestStateID.Results;
  }

  public override void UpdateState(float deltaTime) { }

  protected override void Init()
  {
    m_questResultCtrl.DisplayEnded += OnResultDisplayFinished;
    base.Init();
  }

  public override void OnEnter()
  {
    EncounterResultData resultData = QuestCtrl.SelectedQuestStateData.ResultData;
    ResultDisplayConfig config = new ResultDisplayConfig(QuestCtrl.ResultStateForEncounterResult(resultData), resultData);
    m_questResultCtrl.SetConfig(config);
    m_questResultCtrl.StartDisplay();
  }

  public override void OnExit()
  {
    
  }

  protected void OnResultDisplayFinished()
  {
    switch (QuestCtrl.SelectedQuestStateData.QuestStatus)
    {
      case QuestStateData.Status.Resolved:
        {
          if (QuestCtrl.ResultStateForEncounterResult(QuestCtrl.SelectedQuestStateData.ResultData) == ResultDisplayConfig.ResultState.Victory)
          {
            QuestStateData stateData = new QuestStateData(
              id: QuestCtrl.SelectedQuestStateData.ID,
              status: QuestStateData.Status.Complete,
              resultData: QuestCtrl.SelectedQuestStateData.ResultData);
            QuestCtrl.QuestStateDatabase.SetQuestState(stateData);

            SwitchState(QuestStateID.Idle);
          }
          else
          {
            QuestStateData stateData = new QuestStateData(
              id: QuestCtrl.SelectedQuestStateData.ID,
              status: QuestStateData.Status.Available);
            QuestCtrl.QuestStateDatabase.SetQuestState(stateData);

            SwitchState(QuestStateID.Setup);
          }
        }
        break;
      default:
        {
          SwitchState(QuestStateID.Idle);
        }
        break;
    }
  }
}
