using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks a quest's progress based on it's associated quest ID. Immutability is 
/// enforced through a protected interface so that observing objects do not miss state changes.
/// </summary>
public class QuestStateData
{
  public enum Status : byte
  {
    Unavailable, // quest is visible to the player, but they cannot access it
    Available, // quest is ready to be started by the player
    InProgress, // quest currently underway 
    Resolved, // quest has reached a conclusion, but player has not acknowledged it (eg. results not processed)
    Complete // quest has successfully been completed and player has acknowledged the quest completion 
  }

  public string ID { get; protected set; }
  public Status QuestStatus { get; protected set; }
  public DateTime CompletionTime { get; protected set; }
  public EncounterResultData ResultData { get; protected set; }

  public bool HasCompletionTimeExpired
  {
    get
    {
      return CompletionTime <= DateTime.UtcNow;
    }
  }

  public QuestStateData(
    string id, 
    Status status = Status.Unavailable,
    DateTime completionTime = default(DateTime), 
    EncounterResultData resultData = null
  )
  {
    ID = id;
    CompletionTime = completionTime;
    QuestStatus = status;
    ResultData = resultData;
  }
}
