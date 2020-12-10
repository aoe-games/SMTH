using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestInProgressDisplayConfig
{
  public QuestStateData QuestStateData { get; }

  public QuestInProgressDisplayConfig(QuestStateData questStateData)
  {
    QuestStateData = questStateData;
  }
}
