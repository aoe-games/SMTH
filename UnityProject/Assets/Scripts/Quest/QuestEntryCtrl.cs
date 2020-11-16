using Jalopy;
using SMTH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEntryCtrl : MonoBehaviour
{
  [SerializeField]
  string m_questId = string.Empty;
  [SerializeField]
  QuestEntryView m_entryView = null;

  protected virtual void Start()
  {
    Player player = PlayerManager.Instance[LocalPlayer.k_localPlayerId];
    QuestStateDatabase questStateDatabase = player.GetComponent<PlayerQuestLog>().QuestStateDatabase;
    questStateDatabase.QuestStateChangedEvent += OnQuestStateUpdated;

    QuestStateData questStateData = questStateDatabase[m_questId];
    EvaluateAndUpdateQuestCompletionState(questStateData);
  }

  public void OnQuestSelected()
  {
    GameServices.Services.GetService<QuestManager>().OnQuestSelected(m_questId);
  }

  protected void OnQuestStateUpdated(QuestStateData questData)
  {
    EvaluateAndUpdateQuestCompletionState(questData);
  }

  protected void EvaluateAndUpdateQuestCompletionState(QuestStateData questData)
  {
    if(questData != null && 
      string.CompareOrdinal(questData.ID, m_questId) == 0 && 
      questData.QuestStatus == QuestStateData.Status.Complete)
    {
      m_entryView.SetComplete();
    }
  }
}
