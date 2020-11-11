using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest Data Database", fileName = "QuestDatabase")]
public class QuestDatabase : ScriptableObject, ISerializationCallbackReceiver
{
  [SerializeField]
  List<QuestData> m_questList = new List<QuestData>();

  Dictionary<string, QuestData> m_questData = new Dictionary<string, QuestData>();

  public QuestData this[string id] { get => GetQuestData(id); }

  public QuestData GetQuestData(string id)
  {
    QuestData questData = null;               
    m_questData.TryGetValue(id, out questData);
    return questData;
  }

  void RefreshKeyValueLists()
  {
    m_questList.Clear();

    foreach (var data in m_questData)
    {
      m_questList.Add(data.Value);
    }
  }

  public void UpdateDatabase(Dictionary<string, QuestData> questData)
  {
    m_questData = questData;
    RefreshKeyValueLists();
  }

  public void OnBeforeSerialize()
  {
    RefreshKeyValueLists();
  }

  public void OnAfterDeserialize()
  {
    m_questData = new Dictionary<string, QuestData>();

    foreach (QuestData questData in m_questList)
    {
      m_questData.Add(questData.ID, questData);
    }
  }
}
