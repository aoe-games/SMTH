using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestStateDatabase
{
  public event Action<QuestStateData> QuestStateChangedEvent = null;

  Dictionary<string, QuestStateData> m_questStates = new Dictionary<string, QuestStateData>();

  public QuestStateData this[string id]
  {
    get => GetQuestStateData(id);
    set => SetQuestState(value);
  }

  public QuestStateData GetQuestStateData(string id)
  {
    QuestStateData questData = null;
    m_questStates.TryGetValue(id, out questData);
    return questData;
  }

  public void SetQuestState(QuestStateData questStateData)
  {
    m_questStates[questStateData.ID] = questStateData;
    QuestStateChangedEvent?.Invoke(questStateData);
  }
}
