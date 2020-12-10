using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestRunEncounterState : QuestState
{
  [SerializeField]
  public List<PartyData> m_debug_parties = new List<PartyData>(); 
  protected List<PartyData> m_parties = new List<PartyData>();

  [Header("Encounter")]
  [SerializeField]
  protected EncounterCtrl m_encounterCtrl = null;
  [SerializeField]
  protected EncounterObserver m_encounterObserver = null;
  [SerializeField]
  protected RectTransform m_encounterObserverView = null;
  [SerializeField]
  protected ActorFactory m_actorFactory = null;

  protected override void SetStateID()
  {
    m_stateID = (int)QuestStateID.RunEncounter;
  }

  public override void UpdateState(float deltaTime) { }

  public override void OnEnter()
  {
    m_encounterCtrl.EncounterCompleted += OnEncounterCompleted;
    StartEncounterWithSelectedParties(QuestCtrl.SelectedPartyData);
  }

  public override void OnExit()
  {
    m_encounterCtrl.EncounterCompleted -= OnEncounterCompleted;
  }
  
  protected void StartEncounterWithSelectedParties(PartyData selectedParty)
  {
    if (selectedParty.m_partyMembers.Count < 0) return;

    m_encounterObserver.EncounterObservationComplete += OnBackToPartySelectionSelected;
    m_encounterObserverView.gameObject.SetActive(true);

    m_parties.Clear();
    m_parties.Add(selectedParty);
    m_parties.Add(QuestCtrl.SelectedQuestData.EnemyParty);

    SetupAndExecuteEncounter();
  }

  protected void SetupAndExecuteEncounter()
  {
    m_encounterCtrl.Reset();

    int count = m_parties.Count;
    for (int i = 0; i < count; i++)
    {
      // encounter system uses bit flag to determine active teams
      // bit shift 1 for each additional party so that each has a unique flag value
      // TODO: somehow have this driven by the encounter API; this is not an obvious requirement of the system
      int teamId = QuestCtrl.k_playerPartyId << i;
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

  protected void OnBackToPartySelectionSelected()
  {
    m_encounterObserver.EncounterObservationComplete -= OnBackToPartySelectionSelected;
    m_encounterObserverView.gameObject.SetActive(false);
    SwitchState(QuestStateID.Idle);
  }

  protected void OnEncounterCompleted(EncounterResultData resultData)
  {
    QuestStateData stateData = new QuestStateData(
      id: QuestCtrl.SelectedQuestData.ID,
      status: QuestStateData.Status.InProgress,
      completionTime: DateTime.UtcNow + QuestCtrl.SelectedQuestData.Duration,
      resultData: resultData);
    QuestCtrl.QuestStateDatabase[stateData.ID] = stateData;
  }

  #region Debug

  public void OnStartEncounterWithDebugParties()
  {
    m_parties.Clear();
    m_parties.Add(m_debug_parties[0]);
    m_parties.Add(m_debug_parties[1]);

    SetupAndExecuteEncounter();
  }

  #endregion
}
