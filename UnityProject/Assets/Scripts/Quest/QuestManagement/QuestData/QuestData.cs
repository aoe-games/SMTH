using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quest/Quest Data", fileName = "QuestData.qd.asset")]
public class QuestData : ScriptableObject
{
  public enum QuestType : byte { Mainline, Secondary, Hero }

  [SerializeField]
  string m_id;
  [SerializeField]
  TimeSpan m_duration;
  [SerializeField]
  List<QuestReward> m_rewards;
  [SerializeField]
  PartyData m_enemyParty;
  [SerializeField]
  List<QuestCondition> m_visibilityConditions = new List<QuestCondition>();

  public PartyData EnemyParty { get => m_enemyParty; }

  public string ID { get => m_id; }

  public ReadOnlyCollection<QuestCondition> VisibilityConditions { get => m_visibilityConditions.AsReadOnly(); }
}
