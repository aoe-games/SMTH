using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
                                    
[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest Data Database", fileName = "QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
  [SerializeField]
  List<QuestData> m_questList = new List<QuestData>();

  Dictionary<string, int> m_questDataIndeces = null;

  public ReadOnlyCollection<QuestData> QuestDataCollection { get => m_questList.AsReadOnly(); }

  public QuestData this[string id] { get => GetQuestData(id); }

  public QuestData GetQuestData(string id)
  {
    int index = 0;               
    m_questDataIndeces.TryGetValue(id, out index);

    QuestData questData = m_questList[index];
    return questData;
  }

  public void Load()
  {
    if (m_questDataIndeces != null)
    {
      return;
    }

    int count = m_questList.Count;
    m_questDataIndeces = new Dictionary<string, int>(count);

    for (int i = 0; i < count; i++)
    {
      QuestData data = m_questList[i];
      m_questDataIndeces.Add(data.ID, i);
    }
  }

  public void UpdateDatabase(List<QuestData> questList)
  {
    m_questList = questList;
  }

  public override string ToString()
  {
    string retString = string.Empty;

    foreach (int index in m_questDataIndeces.Values)
    {
      retString += m_questList[index].ID + "\n";
    }

    return retString;
  }
}
