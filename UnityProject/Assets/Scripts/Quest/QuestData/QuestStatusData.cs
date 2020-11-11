using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusData
{
  public enum QuestStatus : byte { Resolved, Unresolved }

  public string ID { get; protected set; }
  public QuestStatus Status { get; protected set; }
  public DateTime CompletionTime { get; protected set; }

  public bool IsComplete
  {
    get
    {
      return CompletionTime >= DateTime.UtcNow;
    }
  }

  public QuestStatusData(string id, DateTime completionTime, QuestStatus status = QuestStatus.Unresolved)
  {
    ID = id;
    CompletionTime = completionTime;
    Status = status;
  }
}
