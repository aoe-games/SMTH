using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Immutable data class that contains various details and statistics outlining 
/// the outcome of an encounter simulation.
/// </summary>
public class EncounterResultData
{
  public int WinningPartyId { get; protected set; }

  public EncounterResultData(int winningPartyId)
  {
    WinningPartyId = winningPartyId;
  }
}
