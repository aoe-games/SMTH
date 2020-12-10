using Jalopy;
using SMTH;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class QuestCtrl : SLT.StateMachine<QuestState>
{
  public const int k_playerPartyId = 1;

  public PartyData SelectedPartyData { get; set; }

  public QuestData SelectedQuestData { get; protected set; }
  public QuestStateData SelectedQuestStateData { get; protected set; }
  public QuestManager QuestManager { get; protected set; }
  public QuestDatabase QuestDatabase { get; protected set; }
  public QuestStateDatabase QuestStateDatabase { get; protected set; }

  protected override void Start()
  {
    base.Start();

    QuestManager = GameServices.Services.GetService<QuestManager>();
    QuestManager.QuestSelectedEvent += OnQuestSelected;

    Player player = PlayerManager.Instance.GetPlayer(LocalPlayer.k_localPlayerId);
    QuestStateDatabase = player.GetComponent<PlayerQuestLog>().QuestStateDatabase;
    QuestStateDatabase.QuestStateChangedEvent += OnQuestStateChanged;

    foreach (QuestState state in m_states)
    {
      state.QuestCtrl = this;
    }
  }

  protected void OnQuestSelected(string questId)
  {
    SelectedQuestData = QuestManager.QuestDatabase[questId];
    SelectedQuestStateData = QuestStateDatabase[questId];
    CurrentState.OnQuestSelected();
  }

  protected void OnQuestStateChanged(QuestStateData stateData)
  {
    if (stateData.ID == SelectedQuestStateData.ID)
    {
      SelectedQuestStateData = stateData;
      CurrentState.OnQuestStateChanged(stateData);
    }
  }

  #region Helpers

  public ResultDisplayConfig.ResultState ResultStateForEncounterResult(EncounterResultData resultData)
  {
    ResultDisplayConfig.ResultState resultState =
        SelectedQuestStateData.ResultData.WinningPartyId == k_playerPartyId ?
          ResultDisplayConfig.ResultState.Victory :
          ResultDisplayConfig.ResultState.Defeat;

    return resultState;
  }

  #endregion
}
