using Jalopy.Cataloging;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
  public enum EntityState { Available, Questing, Injured }

  #region Fields

  [SerializeField]
  protected string m_ID;
  [SerializeField]
  protected string m_name;
  [SerializeField]
  protected EntityState m_state;

  [SerializeField]
  protected int m_bruteAttack;
  [SerializeField]
  protected int m_bruteDefense;
  [SerializeField]
  protected int m_spiritAttack;
  [SerializeField]
  protected int m_spiritDefense;
  [SerializeField]
  protected int m_precisionAttack;
  [SerializeField]
  protected int m_precisionDefense;
  [SerializeField]
  protected int m_speed;
  [SerializeField]
  protected int m_maxHealth;
  [SerializeField]
  protected int m_currentHealth;
  [SerializeField]
  protected List<ActionID> m_actions;
  [SerializeField]
  protected ResourcePathCatalog m_viewCatalog;

  // used as indeces for the view catalog path
  public enum ViewTypes : byte
  {
    RosterPortrait = 0
  }

  #endregion

  #region Properties

  public string ID { get => m_ID; set => m_ID = value; }
  public string Name { get => m_name; set => m_name = value; }
  public EntityState State { get => m_state; set => m_state = value; }

  public int BruteAttack { get => m_bruteAttack; set => m_bruteAttack = value; }
  public int BruteDefense { get => m_bruteDefense; set => m_bruteDefense = value; }
  public int SpiritAttack { get => m_spiritAttack; set => m_spiritAttack = value; }
  public int SpiritDefense { get => m_spiritDefense; set => m_spiritDefense = value; }
  public int PrecisionAttack { get => m_precisionAttack; set => m_precisionAttack = value; }
  public int PrecisionDefense { get => m_precisionDefense; set => m_precisionDefense = value; }

  public int Speed { get => m_speed; set => m_speed = value; }
  public int MaxHealth { get => m_maxHealth; set => m_maxHealth = value; }
  public int CurrentHealth { get => m_currentHealth; set => m_currentHealth = value; }
  public List<ActionID> Actions { get => m_actions; set => m_actions = value; }
  public ResourcePathCatalog ViewCatalog { get => m_viewCatalog; set => m_viewCatalog = value; }

  public string RosterPortraitPath
  {
    get => m_viewCatalog.GetPath(ID, (int)ViewTypes.RosterPortrait);
  }

  #endregion

  public EntityData Clone()
  {
    EntityData clone = (EntityData)MemberwiseClone();
    clone.m_actions = new List<ActionID>(m_actions);
    return clone;
  }
}
