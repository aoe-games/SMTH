using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestCondition 
{
  public enum Type { QuestComplete }

  [SerializeField]
  Type m_type;

  [SerializeField]
  string m_id;

  [SerializeField]
  int m_value;

  public Type ConditionType { get => m_type; }
  public string ID { get => m_id; }
  public int Value { get => m_value; }
}
