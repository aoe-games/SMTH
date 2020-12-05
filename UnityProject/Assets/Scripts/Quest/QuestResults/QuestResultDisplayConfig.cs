using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResultDisplayConfig
{
  public enum ResultState { Victory, Defeat }

  public ResultState State { get; }
  public EncounterResultData ResultData { get; }

  public ResultDisplayConfig(ResultState state, EncounterResultData resultData)
  {
    State = state;
    ResultData = resultData;
  }
}
