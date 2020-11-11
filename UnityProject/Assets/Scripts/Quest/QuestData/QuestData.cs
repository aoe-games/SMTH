﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest Data", fileName = "QuestData.qd.asset")]
public class QuestData : ScriptableObject
{
  public enum QuestType : byte { Mainline, Secondary, Hero }

  [SerializeField]
  string m_id;

  [SerializeField]
  TimeSpan timeSpan;

  [SerializeField]
  List<QuestReward> m_rewards;

  public string ID { get => m_id; }
}
