using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorActionCtrl : MonoBehaviour
{
    [SerializeField]
    protected ActorCtrl m_actor;

    [SerializeField]
    protected int m_priorityRating;

    public int Priority { get { return m_priorityRating; } }

    public abstract void UpdateAction(EncounterCtrl encounter);

    public ActorCtrl Actor
    {
        get { return m_actor; }
        set { m_actor = value; }
    }

    public abstract IEnumerator ProcessAction(EncounterCtrl encounter);
}
