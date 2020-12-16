using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

/// <summary>
/// Immutable data class that contains various details and statistics outlining 
/// the outcome of an encounter simulation.
/// </summary>
public class EncounterResultData
{
  protected List<string> m_participantIDs = new List<string>();

  public int WinningPartyId { get; protected set; }
  public ReadOnlyCollection<string> ParticipantIDs
  { 
    get
    {
      return m_participantIDs == null ? null : m_participantIDs.AsReadOnly();
    }
  }

  protected void SetParticipantIDs(List<string> participants)
  {
    m_participantIDs.Clear();

    if (participants != null)
    {
      m_participantIDs.Capacity = participants.Count;
      m_participantIDs.AddRange(participants);
    }
  }

  public EncounterResultData(int winningPartyId, List<string> participantIDs)
  {    
    WinningPartyId = winningPartyId;
    SetParticipantIDs(participantIDs);   
  }
}
