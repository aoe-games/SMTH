using Jalopy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Services/QuestManager", fileName = "QuestManager")]
public class QuestManager : Service
{
  public event Action<string> QuestSelectedEvent = null;

  [SerializeField]
  QuestDatabase m_database;

  public QuestDatabase Database { get => m_database; }

  public override void Load(MonoBehaviour coroutineRunner, bool doForceReload = false)
  {
    m_database.Load();
    FireLoadCompletedEvent(null);
  }

  public void OnQuestSelected(string questId)
  {
    QuestSelectedEvent?.Invoke(questId);
  }
}
