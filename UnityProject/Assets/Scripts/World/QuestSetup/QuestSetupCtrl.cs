using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class QuestSetupCtrl : MonoBehaviour
{
    [SerializeField]
    protected QuestSetupPartySelectionCtrl m_partySelectionCtrl = null;

    [SerializeField]
    protected PartyData m_enemyRoster = null; // note - this will be controlled from the quest entry data [Dß]
    [SerializeField]
    QuestSetupRosterCtrl m_enemyRosterCtrl = null;
    [SerializeField]
    protected EncounterCtrl m_encounterCtrl = null;
    [SerializeField]
    protected RectTransform m_encounterObserverView = null;
    [SerializeField]
    protected ActorFactory m_actorFactory = null;    
    [SerializeField]
    protected List<PartyData> m_debug_parties = new List<PartyData>();

    protected List<PartyData> m_parties = new List<PartyData>();

    protected void Awake()
    {
        // TODO - eventually have this controlled by the quest entry point so it can be shown and hidden
        Show(); 
    }

    public void Show()
    {
        m_encounterObserverView.gameObject.SetActive(false);
        m_partySelectionCtrl.StartSelection();
        m_enemyRosterCtrl.Roster = m_enemyRoster;
    }

    public void StartEncounterWithSelectedParties()
    {
        m_partySelectionCtrl.EndSelection();

        m_encounterObserverView.gameObject.SetActive(true);
        m_encounterCtrl.Reset();

        m_parties.Clear();
        m_parties.Add(m_partySelectionCtrl.SelectedMembers);
        m_parties.Add(m_enemyRoster);

        SetupAndExecuteEncounter();
    }

    public void StartEncounterWithDebugParties()
    {
        m_encounterCtrl.Reset();

        m_parties.Clear();
        m_parties.Add(m_debug_parties[0]);
        m_parties.Add(m_debug_parties[1]);

        SetupAndExecuteEncounter();
    }

    void SetupAndExecuteEncounter()
    {
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

    public void OnBackToPartySelectionSelected()
    {
        Show();
    }

}
