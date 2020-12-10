using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestState : SLT.State
{
  public enum QuestStateID { Idle, Setup, RunEncounter, InProgess, Results }

  public QuestCtrl QuestCtrl { get; set; }

  public override void UpdateState(float deltaTime) { }

  public override void OnEnter() { }

  public override void OnExit() { }

  public virtual void OnQuestSelected() { }

  public virtual void OnQuestStateChanged(QuestStateData stateData) { }

  protected void SwitchState(QuestStateID stateId)
  {
    QuestCtrl.SwitchState((int)stateId);
  }
}
