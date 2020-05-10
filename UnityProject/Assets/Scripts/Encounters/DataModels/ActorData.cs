using System;
using UnityEngine;

[Serializable]
public class ActorData
{
    public string ID;
    public int TeamID;
    public int Attack;
    public int Defence;
    public int Speed;
    public int ActionPoints;
    public int ActionPointCap;

    [SerializeField]
    protected int m_health;

    public int Health
    {
        get { return m_health; }
    }

    #region Events

    public Action<int/*currentHealth*/, int/*delta*/> HealthAdjusted = null;

    #endregion

    public void AdjustHealth(int delta)
    {
        int previousHealth = m_health;
        m_health += delta;

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
