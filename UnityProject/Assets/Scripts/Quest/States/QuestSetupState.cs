using Jalopy;
using SMTH;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupState : QuestState
{
  [SerializeField]
  protected QuestPartySelectionViewCtrl m_partySelectionCtrl = null;
  [SerializeField]
  protected QuestRosterCtrl m_enemyRosterCtrl = null;

  [Header("Encounter")]
  [SerializeField]
  protected EncounterCtrl m_encounterCtrl = null;
  [SerializeField]
  protected EncounterObserver m_encounterObserver = null;
  [SerializeField]
  protected RectTransform m_encounterObserverView = null;
  [SerializeField]
  protected ActorFactory m_actorFactory = null;

  [SerializeField]
  protected List<PartyData> m_debug_parties = new List<PartyData>();
  protected List<PartyData> m_parties = new List<PartyData>();

  protected override void SetStateID()
  {
    m_stateID = (int)QuestStateID.Setup;
  }

  public override void UpdateState(float deltaTime) { }

  public override void OnEnter()
  {
    ConfigurePartySelectionEventRegistration(true);
    m_partySelectionCtrl.StartSelection();

    m_enemyRosterCtrl.Roster = QuestCtrl.SelectedQuestData.EnemyParty.m_partyMembers.ToArray();
    m_enemyRosterCtrl.Title = QuestCtrl.SelectedQuestData.EnemyParty.m_name;
    m_encounterObserverView.gameObject.SetActive(false);
  }

  public override void OnExit()
  {
    ConfigurePartySelectionEventRegistration(false);
  }

  protected void ConfigurePartySelectionEventRegistration(bool doRegister)
  {
    if (doRegister)
    {
      m_partySelectionCtrl.SelectionConfirmedEvent += OnStartEncounterWithSelectedParties;
      m_partySelectionCtrl.SelectionCancelledEvent += OnExitQuestSetupSelected;
    }
    else
    {
      m_partySelectionCtrl.SelectionConfirmedEvent -= OnStartEncounterWithSelectedParties;
      m_partySelectionCtrl.SelectionCancelledEvent -= OnExitQuestSetupSelected;
    }
  }

  void SetHeroesInPartyUnavailable(PartyData selectedPartyData)
  {
    Player player = PlayerManager.Instance.GetPlayer(LocalPlayer.k_localPlayerId);
    EntityRoster roster = player.GetComponent<PlayerRoster>().Roster;

    int count = selectedPartyData.m_partyMembers.Count;
    for (int i = 0; i < count; i++)
    {
      EntityData entityData = selectedPartyData.m_partyMembers[i];
      if (entityData != null)
      {
        entityData.State = EntityData.EntityState.Questing;
        roster.SetEntityData(entityData);
      }
    }
  }

  protected void OnStartEncounterWithSelectedParties(PartyData selectedParty)
  {
    m_partySelectionCtrl.EndSelection();
    ConfigurePartySelectionEventRegistration(false);

    QuestCtrl.SelectedPartyData = selectedParty;
    SetHeroesInPartyUnavailable(QuestCtrl.SelectedPartyData);

    SwitchState(QuestStateID.RunEncounter);
  }
 
  protected void OnExitQuestSetupSelected()
  {
    m_partySelectionCtrl.EndSelection();
    ConfigurePartySelectionEventRegistration(false);
    SwitchState(QuestStateID.Idle);
  }  
}
