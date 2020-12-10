using Jalopy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Services/QuestManager", fileName = "QuestManager")]
public class QuestManager : Service
{
  public static event Action<string> QuestSelectedEvent = null;

  [SerializeField]
  QuestDatabase m_questDatabase = null;

  SLT.Timer m_questsInProgressTimer;
  List<QuestStateData> m_questsInProgress = new List<QuestStateData>();

  #region PlayerComponents

  protected Player Player { get; set; }
  protected QuestStateDatabase QuestStateDatabase { get; set; }

  #endregion

  public QuestDatabase QuestDatabase { get => m_questDatabase; }

  public override void Load(MonoBehaviour coroutineRunner, bool doForceReload = false)
  {
    GameObject timerObj = new GameObject("QuestTimer");
    timerObj.AddComponent<DontDestroy>();
    m_questsInProgressTimer = timerObj.AddComponent<SLT.Timer>();
    m_questsInProgressTimer.TimerFinishedEvent += OnQuestTimerExpired;

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

    AssessAndUpdateQuestVisibility();
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

  public static void OnQuestSelected(string questId)
  {
    QuestSelectedEvent?.Invoke(questId);
  }
   
  protected void OnQuestStateChanged(QuestStateData stateData)
  {
    AssessAndUpdateQuestVisibility();
    AssessAndUpdateQuestInProgress(stateData);
  }

  protected void AssessAndUpdateQuestVisibility()
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

  protected void AssessAndUpdateQuestInProgress(QuestStateData stateData)
  {
    if (stateData.QuestStatus == QuestStateData.Status.InProgress && !m_questsInProgress.Contains(stateData))
    {
      m_questsInProgress.Add(stateData);
      double timeLeft = Math.Max(m_questsInProgressTimer.TimeLeft, 0);
      DateTime expireTime = DateTime.UtcNow + new TimeSpan((long)timeLeft);

      // if the timer has already run out or the new in progress quest will complete sooner
      // than the one currently running, reset the timer
      if (timeLeft <= 0 || expireTime > stateData.CompletionTime)
      {
        ResetAndStartQuestTimer(stateData);
      }
    }
    else if (stateData.QuestStatus != QuestStateData.Status.InProgress)
    {
      m_questsInProgress.Remove(stateData);
    }
  }

  void OnQuestTimerExpired(SLT.Timer timer)
  {
    QuestStateData nextQuestToFinish = null;

    int count = m_questsInProgress.Count;
    for (int i = 0; i < count; i++)
    {
      QuestStateData stateData = m_questsInProgress[i];
      if (stateData.HasCompletionTimeExpired)
      {
        // first, remove the quest from those being tracked
        m_questsInProgress.Remove(stateData);
        i--;
        count = m_questsInProgress.Count;

        // second, update the quest state database with the new quest state
        QuestStateData newStateData = new QuestStateData(
          id: stateData.ID,
          status: QuestStateData.Status.Resolved,
          resultData: stateData.ResultData);
        QuestStateDatabase.SetQuestState(newStateData);
      }
      else if (nextQuestToFinish == null || nextQuestToFinish.CompletionTime > stateData.CompletionTime)
      {
        nextQuestToFinish = stateData;
      }
    }

    ResetAndStartQuestTimer(nextQuestToFinish);
  }

  void ResetAndStartQuestTimer(QuestStateData stateData)
  {
    if (stateData != null)
    {
      // +1 to round up completion time delta since the timer does not account for the DateTime's 
      // milliseconds which could cause the timer to expire before the quest is actually complete
      m_questsInProgressTimer.Timeout = Math.Max((stateData.CompletionTime - DateTime.UtcNow).TotalSeconds, 0) + 1;
      m_questsInProgressTimer.ResetAndRun();
    }
  }
}
