using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data used to define and hold the data model for ActorCtrl objects.
/// This class wraps an EntityData object to abstract the world entity interface
/// from the encounter system. Additionally, the reference to the EntityData
/// reduced copy overhead as this is a reference to existing data in the program.
/// </summary>
[Serializable]
public class ActorData
{
    #region Fields

    [SerializeField]
    protected EntityData m_entityData;
    [SerializeField]
    protected int m_teamID;
    [SerializeField]
    protected int m_health;
    [SerializeField]
    protected int m_actionPoints;
    [SerializeField]
    protected int m_actionPointCap;

    #endregion

    #region Properties

    // wrapped properties
    public string ID { get { return m_entityData.ID; } }
    public string Name { get { return m_entityData.Name; } }
    public int MaxHealth { get { return m_entityData.MaxHealth; } }
    public int Attack { get { return m_entityData.Attack; } }
    public int Defence { get { return m_entityData.Defence; } }
    public int Speed { get { return m_entityData.Speed; } }
    public List<ActionID> Actions { get { return m_entityData.Actions; } }

    // actor properties
    public int TeamID { get { return m_teamID; } }
    public int Health { get { return m_health; } }

    public int ActionPoints
    {
        get { return m_actionPoints; }
        set { m_actionPoints = value; }
    }

    public int ActionPointCap
    {
        get { return m_actionPointCap; }
        set { m_actionPointCap = value; }
    }

    #endregion

    #region Events

    public Action<int/*currentHealth*/, int/*delta*/> HealthAdjusted = null;

    #endregion

    #region Constructor

    public ActorData(
        EntityData entityData,
        int teamID = 0       
    )
    {
        m_entityData = entityData;
        m_teamID = teamID;
        m_health = entityData.CurrentHealth;
    }
       
    #endregion

    public void AdjustHealth(int delta)
    {
        int previousHealth = m_health;
        m_health = Math.Max(0, Math.Min(m_health + delta, m_entityData.MaxHealth));

        if (previousHealth != m_health)
        {
            HealthAdjusted?.Invoke(m_health, delta);
        }
    }

    public override string ToString()
    {
        string str = "ID:{0} Team:{1}";
        str = string.Format(str, ID, TeamID);

        return str;            
    }
}
