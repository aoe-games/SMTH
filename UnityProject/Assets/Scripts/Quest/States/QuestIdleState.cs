using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIdleState : QuestState
{  
  protected override void SetStateID()
  {
    m_stateID = (int)QuestStateID.Idle;
  }

  public override void UpdateState(float deltaTime) { }

  public override void OnEnter()
  {
    
  }

  public override void OnExit()
  {
    
  }

  public override void OnQuestSelected()
  {
    switch (QuestCtrl.SelectedQuestStateData.QuestStatus)
    {
      case QuestStateData.Status.InProgress:
        {
          SwitchState(QuestStateID.InProgess);
        }
        break;
      case QuestStateData.Status.Complete:
      case QuestStateData.Status.Resolved:
        {
          SwitchState(QuestStateID.Results);
        }
        break;
      default:
        {
          SwitchState(QuestStateID.Setup);
        }
        break;
    }
  }
}
