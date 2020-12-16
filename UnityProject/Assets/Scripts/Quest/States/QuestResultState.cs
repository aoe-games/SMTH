using Jalopy;
using SMTH;
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
          bool playerIsVictorious = QuestCtrl.ResultStateForEncounterResult(QuestCtrl.SelectedQuestStateData.ResultData) == ResultDisplayConfig.ResultState.Victory;
          QuestStateData.Status status = playerIsVictorious ? QuestStateData.Status.Complete : QuestStateData.Status.Available;

          QuestStateData stateData = new QuestStateData(
              id: QuestCtrl.SelectedQuestStateData.ID,
              status: status,
              resultData: QuestCtrl.SelectedQuestStateData.ResultData);
          QuestCtrl.QuestStateDatabase.SetQuestState(stateData);

          UpdateHeroStates();

          if (playerIsVictorious)
          {
            SwitchState(QuestStateID.Idle);
          }
          else
          {
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

  protected void UpdateHeroStates()
  {
    IReadOnlyList<string> participantIDs = QuestCtrl.SelectedQuestStateData.ResultData.ParticipantIDs;

    Player player = PlayerManager.Instance.GetPlayer(LocalPlayer.k_localPlayerId);
    EntityRoster roster = player.GetComponent<PlayerRoster>().Roster;

    int count = participantIDs.Count;
    for (int i = 0; i < count; i++)
    {
      EntityData entityData = roster.GetEntityData(participantIDs[i]);

      if (entityData != null)
      {
        entityData.State = EntityData.EntityState.Available;
        roster.SetEntityData(entityData);
      }
    }
  }
}
