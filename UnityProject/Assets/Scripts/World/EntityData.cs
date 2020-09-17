using Jalopy.Cataloging;
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
    protected ResourcePathCatalog m_viewCatalog;

    // used as indeces for the view catalog path
    public enum ViewTypes : byte
    {
        RosterPortrait = 0
    }

    [SerializeField]
    protected List<ActionID> m_actions;    

    #endregion

    #region Properties

    public string ID { get { return m_ID; } }
    public string Name { get { return m_name; } }

    public int BruteAttack { get { return m_bruteAttack; } }      
    public int BruteDefense { get { return m_bruteDefense; } }
    public int SpiritAttack { get { return m_spiritAttack; } }
    public int SpiritDefense { get { return m_spiritDefense; } }
    public int PrecisionAttack { get { return m_precisionAttack; } }
    public int PrecisionDefense { get { return m_precisionDefense; } }
    public int Speed { get { return m_speed; } }
    public int MaxHealth { get { return m_maxHealth; } }
    public int CurrentHealth { get { return m_currentHealth; } }
    public List<ActionID> Actions { get { return m_actions; } }

    public string RosterPortraitPath
    {
        get => m_viewCatalog.GetPath(ID, (int)ViewTypes.RosterPortrait);
    }

    #endregion

    public EntityData(
        string id = "New Entity",
        string name = "No Name",
        int bruteAttack = 10,        
        int bruteDefense = 5,
        int spiritAttack = 10,
        int spiritDefense = 5,
        int precisionAttack = 10,
        int precisionDefense = 5,
        int speed = 5,
        int maxHealth = 25,
        int currentHealth = 25,
        List<ActionID> actions = null              
    )
    {
        m_ID = id;
        m_name = name;
        m_bruteAttack = bruteAttack;
        m_bruteDefense = bruteDefense;
        m_spiritAttack = spiritAttack;        
        m_spiritDefense = spiritDefense;
        m_precisionAttack = precisionAttack;
        m_precisionDefense = precisionDefense;
        m_speed = speed;
        m_maxHealth = maxHealth;
        m_currentHealth = currentHealth;
        m_actions = actions;                              
    }
}
