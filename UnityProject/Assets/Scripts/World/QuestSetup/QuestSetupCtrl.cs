using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupCtrl : MonoBehaviour
{
    [SerializeField]
    protected PartyData m_heroRoster = null; // note - this will be controlled from player data [Dß]
    [SerializeField]
    protected PartyData m_enemyRoster = null; // note - this will be controlled from the quest entry data [Dß]
    [SerializeField]
    protected EncounterCtrl m_encounterCtrl = null;
    [SerializeField]
    protected ActorFactory m_actorFactory = null;
    [SerializeField]
    protected List<PartyData> m_parties = new List<PartyData>();

    // data providers
    [SerializeField]
    QuestSetupRosterCtrl m_heroRosterCtrl = null;
    [SerializeField]
    QuestSetupRosterCtrl m_enemyRosterCtrl = null;

    protected void Awake()
    {
        // TODO - eventually have this controlled by the quest entry point so it can be shown and hidden
        Show(); 
    }

    public void Show()
    {
        m_heroRosterCtrl.Roster = m_heroRoster;
        m_enemyRosterCtrl.Roster = m_enemyRoster;
    }

    public void StartEncounter()
    {
        m_encounterCtrl.Reset();

        int count = m_parties.Count;
        for (int i = 0; i < count; i++)
        {
            PartyData partyData = m_parties[i];
            foreach (EntityData partyMemberData in partyData.m_partyMembers)
            {
                // translate entity data into actor data to be compatible with encounter system
                ActorData actorData = new ActorData(partyMemberData, i);
                ActorCtrl actorCtrl = m_actorFactory.CreateActor(actorData);
                m_encounterCtrl.AddActor(actorCtrl);
            }
        }

        m_encounterCtrl.StartEncounter();
    }

}
