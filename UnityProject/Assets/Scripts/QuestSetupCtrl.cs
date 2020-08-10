using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupCtrl : MonoBehaviour
{
    [SerializeField]
    public EncounterCtrl m_encounterCtrl;
    [SerializeField]
    protected ActorFactory m_actorFactory = null;
    [SerializeField]
    public List<PartyData> m_parties = new List<PartyData>();    

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
