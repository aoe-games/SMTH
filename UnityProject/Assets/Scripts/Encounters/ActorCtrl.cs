using System;
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
    private List<ActorActionCtrl> m_registeredActions = new List<ActorActionCtrl>();

    private Queue<ActorActionCtrl> m_actionQueue = new Queue<ActorActionCtrl>();
    
    #endregion

    #region Events

    public Action<ActorCtrl> KnockedOut = null;

    #endregion

    #region Properties

    public List<ActorActionCtrl> RegisteredActions { get { return m_registeredActions; } }

    public ActorData ActorData { get { return m_actorData; } }

    public bool IsActive
    {
        get
        {
            bool isActive = false;
            if (m_actorData != null)
            {
                isActive = m_actorData.Health > 0;
            }
            return isActive;
        }
    }

    public int TeamID
    {
        get
        {
            int teamID = 0;
            if (m_actorData != null)
            {
                teamID = m_actorData.TeamID;
            }
            return teamID;
        }
    }

    #endregion

    #region Callbacks

    protected void OnHealthChanged(int health, int delta)
    {
        if (health <= 0)
        {
            KnockedOut?.Invoke(this);
        }
    }

    #endregion

    public void Initialize(ActorData actorData)
    {
        m_actorData = actorData;
    }

    private void Awake()
    {
        m_behaviourCtrl = new ActorBehaviourCtrl(this) {
            m_debugKeyCode = m_debugKeyCode
        };
    }

    private void Start()
    {
        if (ActorData != null)
        {
            ActorData.HealthAdjusted += OnHealthChanged;
        }
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

    public bool RegisterAction(ActorActionCtrl actionCtrl)
    {
        bool actionAdded = false;
        if (RegisteredActions != null && !RegisteredActions.Contains(actionCtrl))
        {
            RegisteredActions.Add(actionCtrl);
        }        
        return actionAdded;
    }

    public bool DeregisterAction(ActorActionCtrl actionCtrl)
    {
        bool actionDeregistered = false;
        if (RegisteredActions != null)
        {
            actionDeregistered = RegisteredActions.Remove(actionCtrl);
        }
        return actionDeregistered;
    }

    public void UpdateActor(EncounterCtrl encounterCtrl)
    {
        SpeedConfig speedConfig = null;

        if (m_behaviourCtrl != null)
        {
            m_behaviourCtrl.UpdateBehaviour(encounterCtrl);
            m_behaviourCtrl.GetSpeedConfig(ref speedConfig);
        }

        if (speedConfig != null && ActorData != null)
        {
            ActorData.ActionPoints += ActorData.Speed;

            // continue to action until we are out of action points
            while (ActorData.ActionPoints >= ActorData.ActionPointCap)
            {
                encounterCtrl.EnqueueActorTurn(this);
                ActorData.ActionPoints -= ActorData.ActionPoints;
            }
        }
        
        // if any specific actions were queued, enqueue for a turn to process them
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
                yield return actionCtrl.ProcessAction(encounterCtrl); ;
            }
        }
    }

    public IEnumerator PerformAttack(ActorCtrl target, Action<AttackConfig> attackPerformedCallback)
    {
        AttackConfig attackConfig = null;

        if (m_behaviourCtrl != null)
        {
            m_behaviourCtrl.GetAttackConfig(ref attackConfig);
        }

        attackPerformedCallback?.Invoke(attackConfig);
        yield return null;
    }

    /// <summary>
    /// The actor will process the attack by allowing its behaviour to take in a 
    /// </summary>
    /// <param name="config"></param>
    /// <returns>damage taken</returns>
    public IEnumerator ProcessAttack(AttackConfig attackConfig, Action<AttackResult> attackProcessedCallback)
    {        
        DefenceConfig defenceConfig = null;
        if (m_behaviourCtrl != null)
        {
            m_behaviourCtrl.GetDefenceConfig(ref defenceConfig);
        }

        AttackResult attackResult = new AttackResult();
        if (defenceConfig != null && ActorData != null)
        {
            int physDmg = attackConfig.PhysicalAttack - defenceConfig.PhysicalDefense;
            int sprDmg = attackConfig.SpiritualAttack - defenceConfig.SpiritualDefense;
            attackResult.DamageTaken = physDmg + sprDmg;
            ActorData.AdjustHealth(-attackResult.DamageTaken);
        }

        attackProcessedCallback?.Invoke(attackResult);
        yield return null;
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
