using Jalopy;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestRosterCtrl : DataSource
{
    #region View Elements - todo: move to view class

    [SerializeField]
    protected InfiniteScrollManager m_scrollView = null;
    [SerializeField]
    TextMeshProUGUI m_partyName;

    #endregion

    protected PartyData m_roster = null;
    protected string m_entryToHighlight = string.Empty;
    protected List<string> m_unavailableEntries = new List<string>();

    public event Action<EntityData> RosterMemberSelectedEvent = null;

    /// <summary>
    /// Set's the roster data used to present the roster list.
    /// The view displaying the roster will be reset with this new data.
    /// </summary>
    public virtual PartyData Roster
    {
        get => m_roster;
        set
        {
            m_roster = value;
            UpdateView();
        }
    }

    protected void UpdateView()
    {
        if (m_partyName != null)
        {
            m_partyName.text = m_roster.m_name;
        }

        if (m_scrollView != null)
        {
            m_scrollView.ResetView();
        }
    }

    public override int NumberOfCells
    {
        get
        {
            QuestRosterCellView view = m_scrollView.m_cellPrototype.GetComponent<QuestRosterCellView>();
            float portraitsPerRow = view != null ? (float)view.PortraitCount : 0f;
            return Mathf.CeilToInt(Roster.m_partyMembers.Count / portraitsPerRow);
        }        
    }

    public override void CellAtIndex(InfiniteScrollCell cell, int index)
    {
        QuestRosterCellView cellView = cell as QuestRosterCellView;
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
