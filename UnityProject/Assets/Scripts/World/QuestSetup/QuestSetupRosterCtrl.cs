using Jalopy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupRosterCtrl : DataSource
{
    [SerializeField]
    protected InfiniteScrollManager m_scrollView = null;

    protected PartyData m_roster = null;
    protected string m_entryToHighlight = string.Empty;
    protected List<string> m_unavailableEntries = new List<string>();

    public event Action<EntityData> RosterMemberSelectedEvent = null;

    /// <summary>
    /// Set's the roster data used to present the roster list.
    /// The view displaying the roster will be reset with this new data.
    /// </summary>
    public PartyData Roster
    {
        get => m_roster;
        set
        {
            m_roster = value;
            m_scrollView.ResetView();
        }
    }

    public override int NumberOfCells
    {
        get
        {
            QuestSetupRosterCellView view = m_scrollView.m_cellPrototype.GetComponent<QuestSetupRosterCellView>();
            float portraitsPerRow = view != null ? (float)view.PortraitCount : 0f;
            return Mathf.CeilToInt(Roster.m_partyMembers.Count / portraitsPerRow);
        }        
    }

    public override void CellAtIndex(InfiniteScrollCell cell, int index)
    {
        QuestSetupRosterCellView cellView = cell as QuestSetupRosterCellView;
        if (cellView != null)
        {
            cellView.OnCellSelectedEvent += OnRosterCellSelected;

            int portraitCount = cellView.PortraitCount;
            for (int i = 0; i < portraitCount; i++)
            {            
                int idx = (index * portraitCount) + i;
                if (idx < Roster.m_partyMembers.Count)
                {
                    EntityData entityData = Roster.m_partyMembers[idx];

                    string spriteName = entityData.RosterPortraitPath;
                    cellView.SetImageForIndex(spriteName, i);
                    cellView.ShowPortraitAtIndex(true, i);
                    cellView.SetHighlighted(m_entryToHighlight == entityData.ID, i);
                    cellView.SetSelectable(!m_unavailableEntries.Contains(entityData.ID), i);
                }
                else
                {
                    cellView.ShowPortraitAtIndex(false, i);
                }
            }
        }
    }

    public void SetRosterEntryToHighlight(string ID)
    {
        m_entryToHighlight = ID;
        m_scrollView.RefreshView();
    }

    public void SetUnavailableEntries(List<string> IDs)
    {
        m_unavailableEntries.Clear();
        m_unavailableEntries.AddRange(IDs);
        m_scrollView.RefreshView();
    }

    protected void OnRosterCellSelected(int index)
    {
        RosterMemberSelectedEvent?.Invoke(Roster.m_partyMembers[index]);
    }
}
