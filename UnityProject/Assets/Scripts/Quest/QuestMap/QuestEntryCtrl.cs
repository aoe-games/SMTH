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

  PlayerQuestLog m_questLog = null;

  protected virtual void Start()
  {
    Player player = PlayerManager.Instance[LocalPlayer.k_localPlayerId];
    m_questLog = player.GetComponent<PlayerQuestLog>();

    QuestStateDatabase questStateDatabase = m_questLog.QuestStateDatabase;
    questStateDatabase.QuestStateChangedEvent += OnQuestStateUpdated;

    QuestStateData questStateData = questStateDatabase[m_questId];
    if (questStateData == null)
    {
      m_entryView.SetVisible(false);
    }
    else
    {
      EvaluateAndUpdateView(questStateData);
    }
  }

  public void OnQuestSelected()
  {
    GameServices.Services.GetService<QuestManager>().OnQuestSelected(m_questId);
  }

  protected void OnQuestStateUpdated(QuestStateData questStateData)
  {
    EvaluateAndUpdateView(questStateData);
  }

  protected void EvaluateAndUpdateView(QuestStateData questStateData)
  {
    if (questStateData != null && string.CompareOrdinal(questStateData.ID, m_questId) == 0)
    {
      m_entryView.SetVisible(true);

      if (questStateData.QuestStatus == QuestStateData.Status.Complete)
      {
        m_entryView.SetComplete();
      }
    }    
  }
}
