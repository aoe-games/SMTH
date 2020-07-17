using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupCtrl : MonoBehaviour
{
    public EncounterCtrl m_encounterCtrl;
    public List<PartyData> m_parties = new List<PartyData>();
           
    void Start()
    {
        foreach (PartyData partyData in m_parties)
        {
            foreach (EntityData partyMember in partyData.m_partyMembers)
            {                
                ActorCtrl actorCtrl = ActorFactory.CreateActorFromEntityData(
                    partyMember, 
                    partyData.m_teamID, 
                    m_encounterCtrl.transform);

                m_encounterCtrl.AddActor(actorCtrl);
            }
        }
        
        m_encounterCtrl.StartEncounter();
    }                                                     

}
