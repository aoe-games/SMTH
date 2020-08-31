using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupPartySelectionCtrl : MonoBehaviour
{
    [SerializeField]
    protected PartyData m_heroRoster = null; // note - this will be controlled from player data [Dß]           
   
    [SerializeField]
    QuestSetupRosterCtrl m_heroRosterCtrl = null;

    [SerializeField]
    protected List<QuestSetupPartyMemberView> m_memberViews = new List<QuestSetupPartyMemberView>();
    protected QuestSetupPartyMemberView m_selectedMemberView = null;  

    protected List<EntityData> m_memberData = null;

    protected void Awake()
    {     
        // initialize party members
        int partyMemberCount = m_memberViews.Count;
        m_memberData = new List<EntityData>(partyMemberCount);

        for (int i = 0; i < partyMemberCount; i++)
        {
            m_memberData.Add(null);

            QuestSetupPartyMemberView view = m_memberViews[i];
            if (view != null)
            {
                view.Index = i;
                view.MemberSelectedEvent += OnPartyMemberCharacterSelected;
            }
        }

        // initialize hero roster
        m_heroRosterCtrl.RosterMemberSelectedEvent += OnRosterMemberSelected;
        m_heroRosterCtrl.Roster = m_heroRoster;
    }

    // Start is called before the first frame update
    public void  StartSetup()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Member View

    void OnPartyMemberCharacterSelected(int index)
    {
        SwapCurrentMember(index);
    }

    void SwapCurrentMember(int newIndex)
    {
        // deselect the currently selected view
        if (m_selectedMemberView != null)
        {
            m_selectedMemberView.Deselect();
        }

        // setup newly selected view
        QuestSetupPartyMemberView newView = null; 
        if (newIndex >= 0 && newIndex < m_memberViews.Count)
        {
            newView = m_memberViews[newIndex];
        }

        m_selectedMemberView = newView;
    }

    #endregion

    void OnRosterMemberSelected(EntityData entityData)
    {           
        if (m_selectedMemberView != null)
        {
            int selectedViewIndex = m_selectedMemberView.Index;
            m_memberData[selectedViewIndex] = entityData;

            m_selectedMemberView.UpdateView(entityData);
        }  
    }       

    #region Helper

    

    #endregion
}
