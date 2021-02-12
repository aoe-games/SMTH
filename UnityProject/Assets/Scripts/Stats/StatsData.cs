using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatsData
{
  [SerializeField]
  private int m_speed;
  [SerializeField]
  private int m_maxHealth;
  [SerializeField]
  private int m_bruteAttack;
  [SerializeField]
  private int m_bruteDefense;
  [SerializeField]
  private int m_spiritAttack;
  [SerializeField]
  private int m_spiritDefense;
  [SerializeField]
  private int m_precisionAttack;
  [SerializeField]
  private int m_precisionDefense;

  public int Speed { get => m_speed; set => m_speed = value; }
  public int Health { get => m_maxHealth; set => m_maxHealth = value; }
  public int BruteAttack { get => m_bruteAttack; set => m_bruteAttack = value; }
  public int BruteDefense { get => m_bruteDefense; set => m_bruteDefense = value; }
  public int SpiritAttack { get => m_spiritAttack; set => m_spiritAttack = value; }
  public int SpiritDefense { get => m_spiritDefense; set => m_spiritDefense = value; }
  public int PrecisionAttack { get => m_precisionAttack; set => m_precisionAttack = value; }
  public int PrecisionDefense { get => m_precisionDefense; set => m_precisionDefense = value; }

  public int TotalAttack { get { return (m_bruteAttack + m_spiritAttack + m_precisionAttack); } }
  public int TotalDefense { get { return (m_bruteDefense + m_spiritDefense + m_precisionDefense); } }

  public StatsData(
    int speed,
    int maxHealth,
    int bruteAtk,
    int bruteDef,
    int spiritAtk,
    int spiritDef,
    int precisionAtk,
    int precisionDef
  )
  {
    m_speed = speed;
    m_maxHealth = maxHealth;
    m_bruteAttack = bruteAtk;
    m_bruteDefense = bruteDef;
    m_spiritAttack = spiritAtk;
    m_spiritDefense = spiritDef;
    m_precisionAttack = precisionAtk;
    m_precisionDefense = precisionDef;
  }

  public StatsData(StatsData copy) :
    this(
      copy.Speed,
      copy.Health,
      copy.BruteAttack,
      copy.BruteDefense,
      copy.SpiritAttack,
      copy.SpiritDefense,
      copy.PrecisionAttack,
      copy.PrecisionDefense
    )
  { }

}
