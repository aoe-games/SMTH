using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class QuestCtrl : MonoBehaviour
{
    [SerializeField]
    protected QuestPartySelectionCtrl m_partySelectionCtrl = null;

    [SerializeField]
    protected PartyData m_enemyRoster = null; // note - this will be controlled from the quest entry data [Dß]
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

    protected void Awake()
    {
         
    }

    public void StartQuestSetup()
    {
        ConfigurePartySelectionEventRegistration(true);
        m_partySelectionCtrl.StartSelection();

        m_enemyRosterCtrl.Roster = m_enemyRoster;
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
        m_parties.Add(m_enemyRoster);

        SetupAndExecuteEncounter();
    }

    void SetupAndExecuteEncounter()
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

    public void OnStartEncounterWithSelectedParties(PartyData selectedParty)
    {
        StartEncounterWithSelectedParties(selectedParty);
    }    

    public void OnStartQuestSelected()
    {
        StartQuestSetup();
    }

    public void OnExitQuestSetup()
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

    public void OnBackToPartySelectionSelected()
    {
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
