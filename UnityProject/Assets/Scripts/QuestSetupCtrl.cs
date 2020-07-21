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
           
    void Start()
    {
        m_encounterCtrl.Reset();

        foreach (PartyData partyData in m_parties)
        {
            foreach (EntityData partyMemberData in partyData.m_partyMembers)
            {
                // translate entity data into actor data to be compatible with encounter system
                ActorData actorData = new ActorData(partyMemberData, partyData.m_teamID);
                ActorCtrl actorCtrl = m_actorFactory.CreateActor(actorData);
                m_encounterCtrl.AddActor(actorCtrl);
            }
        }
        
        m_encounterCtrl.StartEncounter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Start();
        }
    }

}
