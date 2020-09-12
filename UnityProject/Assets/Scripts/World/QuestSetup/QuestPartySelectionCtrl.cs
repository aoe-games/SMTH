using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class QuestPartySelectionCtrl : MonoBehaviour
{
    [SerializeField]
    protected PartyData m_heroRoster = null; // note - this will be controlled from player data [Dß]         
    [SerializeField]
    QuestRosterCtrl m_heroRosterCtrl = null;
    [SerializeField]
    RectTransform m_partySelectionView = null;

    [SerializeField]
    protected List<QuestPartyMemberView> m_memberViews = new List<QuestPartyMemberView>();
    protected QuestPartyMemberView m_selectedMemberView = null;

    protected List<EntityData> m_memberData = null;

    public event Action<PartyData> SelectionConfirmedEvent = null;
    public event Action SelectionCancelledEvent = null;

    protected void Awake()
    {     
        // initialize party members
        int partyMemberCount = m_memberViews.Count;
        m_memberData = new List<EntityData>(partyMemberCount);

        for (int i = 0; i < partyMemberCount; i++)
        {
            EntityData entityData = null;
            m_memberData.Add(entityData);

            QuestPartyMemberView view = m_memberViews[i];
            if (view != null)
            {
                view.index = i;
                view.MemberSelectedEvent += OnPartyMemberPortraitSelected;
                view.RemoveSelectedEvent += OnRemovePartyMemberSelected;
                view.UpdateView(entityData);
            }
        }

        // initialize hero roster
        m_heroRosterCtrl.RosterMemberSelectedEvent += OnRosterMemberSelected;
        m_heroRosterCtrl.Roster = m_heroRoster;
    }

    // Start is called before the first frame update
    public void StartSelection()
    {
        m_partySelectionView.gameObject.SetActive(true);
    }

    public void EndSelection()
    {
        m_partySelectionView.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Member View

    void OnRemovePartyMemberSelected(int index)
    {
        m_memberData[index] = null;

        QuestPartyMemberView memberView = m_memberViews[index];
        memberView.UpdateView(null);
        
        UpdateRosterSelectionStates();
    }

    void OnPartyMemberPortraitSelected(int index)
    {
        UpdateSelectedMember(index);
    }

    void UpdateSelectedMember(int newIndex)
    {
        // deselect the currently selected view
        if (m_selectedMemberView != null)
        {
            m_selectedMemberView.SetSelected(false);
        }

        // setup newly selected view
        m_selectedMemberView = m_memberViews[newIndex];
        m_selectedMemberView.SetSelected(true);
                      
        UpdateRosterSelectionStates();
    }

    #endregion

    void OnRosterMemberSelected(EntityData entityData)
    {
        // a hero can only be assigned to the party once, so make sure the selected
        // entity isn't added to the party twice
        if (!m_memberData.Contains(entityData) && m_selectedMemberView != null)
        {  
            m_selectedMemberView.UpdateView(entityData);

            int selectedViewIndex = m_selectedMemberView.index;
            m_memberData[selectedViewIndex] = entityData;

            UpdateRosterSelectionStates();            
        }  
    }

    public void OnConfirmPartySelection()
    {
        SelectionConfirmedEvent?.Invoke(GetSelectedMembersAsParty()); 
    }

    public void OnCancelPartySelection()
    {
        SelectionCancelledEvent?.Invoke();
    }

    #region Helpers

    protected PartyData GetSelectedMembersAsParty()
    { 
        PartyData partyData = new PartyData();
        partyData.m_partyMembers.AddRange(m_memberData);
        return partyData;
    }

    void UpdateRosterSelectionStates()
    {
        // update roster highlight and available entries to match selected entity
        EntityData selectedEntityData = null;
        if (m_selectedMemberView != null)
        {
            int index = m_selectedMemberView.index;
            selectedEntityData = m_memberData[index];
        }                 
        
        string highlightId = selectedEntityData == null ? string.Empty : selectedEntityData.ID;
        m_heroRosterCtrl.SetRosterEntryToHighlight(highlightId);

        // -1 because we don't include the selected party member, since it will have a different visual highlight
        List<string> unavailableEntries = new List<string>(m_memberData.Count - 1);
        for (int i = 0; i < m_memberData.Count; i++)
        {
            EntityData entityData = m_memberData[i];
            if (entityData != null)
            {
                if (selectedEntityData == null || entityData.ID != selectedEntityData.ID)
                {
                    unavailableEntries.Add(entityData.ID);
                }
            }
        }

        m_heroRosterCtrl.SetUnavailableEntries(unavailableEntries);
    }

    #endregion
}
