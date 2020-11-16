using Jalopy;
using SMTH;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class QuestCtrl : MonoBehaviour
{
  [SerializeField]
  protected QuestPartySelectionCtrl m_partySelectionCtrl = null;

  [SerializeField]
  QuestRosterCtrl m_enemyRosterCtrl = null;
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
  QuestData m_questData = null;
  QuestManager m_questManager = null;
  QuestDatabase m_questDatabase = null;
  QuestStateDatabase m_questStateDatabase = null;

  protected virtual void Start()
  {            
    m_questManager = GameServices.Services.GetService<QuestManager>();
    m_questManager.QuestSelectedEvent += OnQuestSelected;

    Player player = PlayerManager.Instance.GetPlayer(LocalPlayer.k_localPlayerId);
    m_questStateDatabase = player.GetComponent<PlayerQuestLog>().QuestStateDatabase;
  }

  protected void StartQuestSetup()
  {
    ConfigurePartySelectionEventRegistration(true);
    m_partySelectionCtrl.StartSelection();

    m_enemyRosterCtrl.Roster = m_questData.EnemyParty;
    m_encounterObserverView.gameObject.SetActive(false);
  }

  protected void StartEncounterWithSelectedParties(PartyData selectedParty)
  {
    if (selectedParty.m_partyMembers.Count < 0) return;

    m_partySelectionCtrl.EndSelection();
    ConfigurePartySelectionEventRegistration(false);

    m_encounterObserver.EncounterObservationComplete += OnBackToPartySelectionSelected;
    m_encounterObserverView.gameObject.SetActive(true);

    m_parties.Clear();
    m_parties.Add(selectedParty);
    m_parties.Add(m_questData.EnemyParty);

    SetupAndExecuteEncounter();
  }

  protected void SetupAndExecuteEncounter()
  {
    m_encounterCtrl.Reset();

    int count = m_parties.Count;
    for (int i = 0; i < count; i++)
    {
      int teamId = 1 << i; // encounter system uses bit flag to determine active teams
      PartyData partyData = m_parties[i];
      foreach (EntityData partyMemberData in partyData.m_partyMembers)
      {
        if (partyMemberData != null)
        {
          // translate entity data into actor data to be compatible with encounter system                         
          ActorData actorData = new ActorData(partyMemberData, teamId);
          ActorCtrl actorCtrl = m_actorFactory.CreateActor(actorData);
          m_encounterCtrl.AddActor(actorCtrl);
        }
      }
    }

    m_encounterCtrl.StartEncounter();
  }

  #region Callbacks

  protected void OnStartEncounterWithSelectedParties(PartyData selectedParty)
  {
    StartEncounterWithSelectedParties(selectedParty);
  }

  protected void OnQuestSelected(string questId)
  {
    m_questData = m_questManager.Database[questId];
    StartQuestSetup();
  }

  protected void OnExitQuestSetup()
  {
    m_partySelectionCtrl.EndSelection();
    ConfigurePartySelectionEventRegistration(false);
  }

  public void OnStartEncounterWithDebugParties()
  {
    m_parties.Clear();
    m_parties.Add(m_debug_parties[0]);
    m_parties.Add(m_debug_parties[1]);

    SetupAndExecuteEncounter();
  }

  protected void OnBackToPartySelectionSelected()
  {
    QuestStateData stateData = m_questStateDatabase[m_questData.ID];
    if (stateData != null)
    {
      stateData = new QuestStateData(stateData.ID, stateData.CompletionTime, QuestStateData.Status.Complete);      
    }
    else if (stateData == null)
    {
      stateData = new QuestStateData(m_questData.ID, DateTime.UtcNow, QuestStateData.Status.Complete);
    }

    m_questStateDatabase[stateData.ID] = stateData;

    m_encounterObserver.EncounterObservationComplete -= OnBackToPartySelectionSelected;
    StartQuestSetup();
  }

  #endregion

  #region Helpers

  protected void ConfigurePartySelectionEventRegistration(bool doRegister)
  {
    if (doRegister)
    {
      m_partySelectionCtrl.SelectionConfirmedEvent += OnStartEncounterWithSelectedParties;
      m_partySelectionCtrl.SelectionCancelledEvent += OnExitQuestSetup;
    }
    else
    {
      m_partySelectionCtrl.SelectionConfirmedEvent -= OnStartEncounterWithSelectedParties;
      m_partySelectionCtrl.SelectionCancelledEvent -= OnExitQuestSetup;
    }
  }

  #endregion
}
