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
  PartyData m_enemyParty;

  [Header("Quest Duration")]
  [SerializeField]
  int m_hours;
  [SerializeField]
  int m_minutes;
  [SerializeField]
  int m_seconds;  
   
  [SerializeField, Header("Rewards")]
  List<QuestReward> m_rewards;
  
  [SerializeField, Header("Conditions")]
  List<QuestCondition> m_visibilityConditions = new List<QuestCondition>();

  public string ID { get => m_id; }
  public TimeSpan Duration { get => new TimeSpan(m_hours, m_minutes, m_seconds); }
  public PartyData EnemyParty { get => m_enemyParty; }
                                    
  public ReadOnlyCollection<QuestCondition> VisibilityConditions { get => m_visibilityConditions.AsReadOnly(); }
}
