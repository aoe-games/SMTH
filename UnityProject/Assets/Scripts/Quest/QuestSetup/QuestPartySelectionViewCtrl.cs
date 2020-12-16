using Jalopy;
using SMTH;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class QuestPartySelectionViewCtrl : MonoBehaviour
{
  [SerializeField]
  QuestRosterCtrl m_heroRosterCtrl = null;
  [SerializeField]
  RectTransform m_partySelectionView = null;

  [SerializeField]
  protected List<QuestPartyMemberView> m_memberViews = new List<QuestPartyMemberView>();
  protected QuestPartyMemberView m_selectedMemberView = null;

  protected List<EntityData> m_selectedMembersData = null;
  protected EntityData[] m_unavailableEntities = null;

  public event System.Action<PartyData> SelectionConfirmedEvent = null;
  public event System.Action SelectionCancelledEvent = null;

  protected void Reset()
  {
    Player player = PlayerManager.Instance.GetPlayer(LocalPlayer.k_localPlayerId);
           
    // initialize hero roster
    m_heroRosterCtrl.Roster = player.GetComponent<PlayerRoster>().Roster.GetEntityDataCollection();
    m_heroRosterCtrl.Title = "Heroes";
    m_heroRosterCtrl.RosterMemberSelectedEvent += OnRosterMemberSelected;

    // initialize party members
    int partyMemberCount = m_memberViews.Count;
    m_selectedMembersData = new List<EntityData>(partyMemberCount);
    m_unavailableEntities = Array.FindAll(m_heroRosterCtrl.Roster, x => x.State != EntityData.EntityState.Available);

    for (int i = 0; i < partyMemberCount; i++)
    {
      EntityData entityData = null;
      m_selectedMembersData.Add(entityData);

      QuestPartyMemberView view = m_memberViews[i];
      if (view != null)
      {
        view.index = i;
        view.MemberSelectedEvent += OnPartyMemberPortraitSelected;
        view.RemoveSelectedEvent += OnRemovePartyMemberSelected;
        view.UpdateView(entityData);
      }
    }
      
    UpdateSelectedMember(0);
  }

  public void StartSelection()
  {
    Reset();
    m_partySelectionView.gameObject.SetActive(true);
  }

  public void EndSelection()
  {
    m_partySelectionView.gameObject.SetActive(false);
  }
  
  #region Member View

  void OnRemovePartyMemberSelected(int index)
  {
    m_selectedMembersData[index] = null;

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
    if (!m_selectedMembersData.Contains(entityData) && m_selectedMemberView != null)
    {
      m_selectedMemberView.UpdateView(entityData);

      int selectedViewIndex = m_selectedMemberView.index;
      m_selectedMembersData[selectedViewIndex] = entityData;

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
    PartyData partyData = ScriptableObject.CreateInstance<PartyData>();
    partyData.m_partyMembers.AddRange(m_selectedMembersData);
    return partyData;
  }

  void UpdateRosterSelectionStates()
  {
    // update roster highlight and available entries to match selected entity
    EntityData selectedEntityData = null;
    if (m_selectedMemberView != null)
    {
      int index = m_selectedMemberView.index;
      selectedEntityData = m_selectedMembersData[index];
    }

    string highlightId = selectedEntityData == null ? string.Empty : selectedEntityData.ID;
    m_heroRosterCtrl.SetRosterEntryToHighlight(highlightId);

    List<string> unavailableEntries = new List<string>(m_selectedMembersData.Count);
    for (int i = 0; i < m_selectedMembersData.Count; i++)
    {
      EntityData entityData = m_selectedMembersData[i];
      if (entityData != null)
      {
        // if the entity is currently selected in the party view, then it's portrait should be 
        // visually available in the roster view as well while the other selected party members\
        // remain visually unavailable
        if (selectedEntityData == null || entityData.ID != selectedEntityData.ID)
        {
          unavailableEntries.Add(entityData.ID);
        }
      }
    }

    foreach (EntityData unavailableEntity in m_unavailableEntities)
    {
      unavailableEntries.Add(unavailableEntity.ID);
    }

    m_heroRosterCtrl.SetUnavailableEntries(unavailableEntries);
  }

  #endregion
}
