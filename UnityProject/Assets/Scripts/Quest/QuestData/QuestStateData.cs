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
  public enum Status : byte { Unavailable, Available, InProgress, Complete  }

  public string ID { get; protected set; }
  public Status QuestStatus { get; protected set; }
  public DateTime CompletionTime { get; protected set; }

  public bool HasCompletionTimeExpired
  {
    get
    {
      return CompletionTime >= DateTime.UtcNow;
    }
  }

  public QuestStateData(string id, Status status = Status.Unavailable, DateTime completionTime = default(DateTime))
  {
    ID = id;
    CompletionTime = completionTime;
    QuestStatus = status;
  }
}
