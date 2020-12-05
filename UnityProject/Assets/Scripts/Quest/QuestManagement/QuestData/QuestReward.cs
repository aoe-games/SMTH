using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct QuestReward
{
  public enum Type : byte { Hero, Gold }

  [SerializeField]
  string m_id;

  [SerializeField]
  Type type; 

  [SerializeField]
  int m_value; 
}
