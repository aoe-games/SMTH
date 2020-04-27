using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The actor controller is the interface between the environment and the actor's internal
/// logic. Actors harbour behaviour and actions that it uses to decide how to interact
/// with other actors within during an encounter.
/// </summary>
public class ActorCtrl : MonoBehaviour
{
    #region Fields

    public KeyCode m_debugKeyCode = KeyCode.Space;

    [SerializeField]
    protected ActorData m_actorData = null;

    [SerializeField]
    protected ActorBehaviourCtrl m_behaviourCtrl = null;

    [SerializeField]
    protected List<ActorActionCtrl> m_registeredActions = new List<ActorActionCtrl>();

    protected Queue<ActorActionCtrl> m_actionQueue = new Queue<ActorActionCtrl>();

    #endregion

    #region Properties

    public List<ActorActionCtrl> RegisteredActions { get { return m_registeredActions; } }

    public ActorData ActorData { get { return m_actorData; } }

    #endregion

    private void Awake()
    {
        m_behaviourCtrl = new ActorBehaviourCtrl(this);
        m_behaviourCtrl.m_debugKeyCode = m_debugKeyCode;
    }

    private void Update()
    {
        if (m_behaviourCtrl != null)
        {
            m_behaviourCtrl.Update();
        }
    }

    public void EnqueueAction(ActorActionCtrl action)
    {
        if (m_actionQueue != null)
        {
            m_actionQueue.Enqueue(action);
        }
    }

    public void UpdateActor(EncounterCtrl encounterCtrl)
    {
        if (m_behaviourCtrl != null)
        {
            m_behaviourCtrl.UpdateBehaviour(encounterCtrl);
        }

        if (m_actionQueue.Count > 0)
        {
            encounterCtrl.EnqueueActorTurn(this);
        }
    }

    public IEnumerator ProcessTurn(EncounterCtrl encounterCtrl)
    {
        // if we don't have an action and it's our turn, find something to do!
        if (m_actionQueue.Count <= 0)
        {
            ActorActionCtrl actionCtrl = m_behaviourCtrl.ChooseAction(encounterCtrl);
            m_actionQueue.Enqueue(actionCtrl);
        }

        // process enqueued actions
        while (m_actionQueue.Count > 0)
        {
            ActorActionCtrl actionCtrl = m_actionQueue.Dequeue();
            if (actionCtrl != null)
            {
                yield return m_behaviourCtrl.ProcessAction(actionCtrl, encounterCtrl);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    /// <returns>damage taken</returns>
    public int ProcessAttack(AttackConfig config)
    {
        int damageTaken = 0;
        return damageTaken;
    }

    public AttackConfig GetAttackConfig()
    {
        AttackConfig config = null;

        if (m_behaviourCtrl != null)
        {
            config = m_behaviourCtrl.GetAttackConfig();
        }

        return config;
    }

    public DefenceConfig GetDefenceConfig()
    {
        DefenceConfig config = null;

        if (m_behaviourCtrl != null)
        {
            config = m_behaviourCtrl.GetDefenceConfig();
        }

        return config;
    }

    public ActorCtrl GetTarget(EncounterCtrl encounterCtrl)
    {
        ActorCtrl target = null;

        if (m_behaviourCtrl != null)
        {
            target = m_behaviourCtrl.GetTarget(encounterCtrl);
        }

        return target;
    }

    public override string ToString()
    {
        string retStr = string.Empty;

        if (m_actorData != null)
        {
            return m_actorData.ToString();
        }

        return retStr;
    }
}
