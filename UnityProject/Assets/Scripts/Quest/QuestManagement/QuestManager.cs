using Jalopy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Services/QuestManager", fileName = "QuestManager")]
public class QuestManager : Service
{
  public event Action<string> QuestSelectedEvent = null;

  [SerializeField]
  QuestDatabase m_questDatabase = null;
  
  #region PlayerComponents

  protected Player Player { get; set; }
  protected QuestStateDatabase QuestStateDatabase { get; set; }

  #endregion

  public QuestDatabase QuestDatabase { get => m_questDatabase; }

  public override void Load(MonoBehaviour coroutineRunner, bool doForceReload = false)
  {
    QuestDatabase.Load();
    FireLoadCompletedEvent(null);
  }

  /// <summary>
  /// Set the player reference so the manager can associate various events and player states
  /// with the quest database to evaluate availabilty and progress
  /// </summary>
  /// <param name="player">The player to reference state against</param>
  public void SetPlayer(Player player)
  {
    if (Player != null)
    {
      QuestStateDatabase.QuestStateChangedEvent -= OnQuestStateChanged;
    }

    Player = player;

    QuestStateDatabase = player.GetComponent<PlayerQuestLog>().QuestStateDatabase;
    QuestStateDatabase.QuestStateChangedEvent += OnQuestStateChanged;

    AssessAndUpdateQuestStates();
  }

  public void OnQuestSelected(string questId)
  {
    QuestSelectedEvent?.Invoke(questId);
  }

  public void OnQuestStateChanged(QuestStateData stateData)
  {
    AssessAndUpdateQuestStates();
  }

  public void AssessAndUpdateQuestStates()
  {
    ReadOnlyCollection<QuestData> questDataCollection = m_questDatabase.QuestDataCollection;
    int count = questDataCollection.Count;
    for (int i = 0; i < count; i++)
    {
      QuestData questData = questDataCollection[i];

      // if the quest has it's visibility conditions met but there is no associated state data,
      // create the state data and add it to the player
      QuestStateData associatedStateData = QuestStateDatabase[questData.ID];
      if (associatedStateData == null)
      {
        bool questVisible = IsQuestVisible(questData);
        if (questVisible)
        {
          QuestStateData newStateData = new QuestStateData(questData.ID, QuestStateData.Status.Available);
          QuestStateDatabase.SetQuestState(newStateData);
        }
      }
    }
  }

  public bool IsQuestVisible(QuestData questDataToCheck)
  {
    bool questVisible = true;

    ReadOnlyCollection<QuestCondition> questVisibilityConditions = questDataToCheck.VisibilityConditions;
    int count = questVisibilityConditions.Count;
    for (int i = 0; i < count; i++)
    {
      questVisible = AssessCondition(questVisibilityConditions[i]);
      if (!questVisible)
      {
        break;
      }
    }

    return questVisible;
  }

  public bool AssessCondition(QuestCondition questCondition)
  {
    switch (questCondition.ConditionType)
    {
      case QuestCondition.Type.QuestComplete:
        return QuestCompleteConditionAssessor.AssessCondition(questCondition, QuestStateDatabase);
      default:
        return false;
    }
  }

}
