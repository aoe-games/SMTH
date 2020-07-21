using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    #region Fields

    [SerializeField]
    protected string m_ID;
    [SerializeField]
    protected string m_name;
    [SerializeField]
    protected int m_attack;
    [SerializeField]
    protected int m_defence;
    [SerializeField]
    protected int m_speed;
    [SerializeField]
    protected int m_maxHealth;
    [SerializeField]
    protected int m_currentHealth;
    [SerializeField]
    protected List<ActionID> m_actions;

    #endregion

    #region Properties

    public string ID { get { return m_ID; } }
    public string Name { get { return m_name; } }
    public int Attack { get { return m_attack; } }
    public int Defence { get { return m_defence; } }
    public int Speed { get { return m_speed; } }
    public int MaxHealth { get { return m_maxHealth; } }
    public int CurrentHealth { get { return m_currentHealth; } }
    public List<ActionID> Actions { get { return m_actions; } }

    #endregion

    public EntityData(
        string id = "New Entity",
        string name = "No Name",
        int attack = 10,
        int defence = 5,
        int speed = 5,
        int maxHealth = 25,
        int currentHealth = 25,
        List<ActionID> actions = null
    )
    {
        m_ID = id;
        m_name = name;
        m_attack = attack;
        m_defence = defence;
        m_speed = speed;
        m_maxHealth = maxHealth;
        m_currentHealth = currentHealth;
        m_actions = actions;
    }
}
