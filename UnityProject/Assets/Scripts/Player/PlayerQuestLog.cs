using System;
using System.Collections;
using System.Collections.Generic;
using Jalopy;
using UnityEngine;

public class PlayerQuestLog : PlayerComponent
{
  [SerializeField]
  QuestStateDatabase m_questStateDatabase = new QuestStateDatabase();

  public QuestStateDatabase QuestStateDatabase
  {
    get => m_questStateDatabase;
  }

  public override void Load(Action<PlayerComponent, Exception> loadCompletedCallback)
  {
    loadCompletedCallback?.Invoke(this, null);
  }
}


