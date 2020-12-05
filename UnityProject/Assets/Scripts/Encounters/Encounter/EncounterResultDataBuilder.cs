using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exposes an API that allows the manipulation of protected base class properties and 
/// the creation of an immutable EncounterResultData based on the settings of this builder 
/// object, essentially making an immutable clone itself. This allows for results data
/// to be manipulate and then translated into an immutable object before leaving the encounter
/// system.
/// </summary>
public class EncounterResultDataBuilder : EncounterResultData
{
  public EncounterResultDataBuilder()
  : base(0)
  {  }

  public EncounterResultData CreateResultData()
  {
    return new EncounterResultData (
      WinningPartyId
    );
  }

  public EncounterResultDataBuilder WithWinningPartyId(int partyId)
  {
    WinningPartyId = partyId;
    return this;
  }
}
