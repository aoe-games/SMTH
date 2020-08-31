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
                    string spriteName = Roster.m_partyMembers[idx].RosterPortraitPath;
                    cellView.SetImageForIndex(spriteName, i);                     
                }
                else
                {
                    cellView.ShowPortraitAtIndex(false, i);
                }
            }
        }
    }

    protected void OnRosterCellSelected(int index)
    {
        RosterMemberSelectedEvent?.Invoke(Roster.m_partyMembers[index]);
    }
}
