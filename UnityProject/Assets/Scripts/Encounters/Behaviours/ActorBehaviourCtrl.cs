using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class determines how an actor should behave in the context of what
/// actions an actor can take and the environment (encounter) in which they can take them.
/// </summary>
/// 
/// This class utilizes a decorator pattern in order to extend functionality 
/// and filter behaviour. For example, during action selection, an actor that is
/// behaving confused may select a random action and target overriding 
/// the standard behaviour profile. 
[Serializable]
public class ActorBehaviourCtrl
{
    // DEBUG
    [SerializeField]
    public KeyCode m_debugKeyCode_DEBUG = KeyCode.Space;
    protected bool m_actionTriggered_DEBUG = false;

    protected ActorCtrl m_actor = null;
    protected ActorBehaviourCtrl m_decoratedBehaviour = null;

    protected ActorData ActorData
    {
        get
        {
            ActorData actorData = null;

            if (m_actor != null)
            {
                actorData = m_actor.ActorData;
            }

            return actorData;
        }
    }

    public ActorBehaviourCtrl(ActorCtrl actor)
    {
        m_actor = actor;
    }

    public void Update()
    {
        if (m_actionTriggered_DEBUG == false)
        {
            m_actionTriggered_DEBUG = Input.GetKeyDown(m_debugKeyCode_DEBUG);
        }

        if (m_decoratedBehaviour != null)
        {
            m_decoratedBehaviour.Update();
        }
    }

    public virtual void UpdateBehaviour(EncounterCtrl encounterCtrl)
    {
        if (m_actionTriggered_DEBUG)
        {
            encounterCtrl.EnqueueActorTurn(m_actor);
            m_actionTriggered_DEBUG = false;
        }

        if (m_decoratedBehaviour != null)
        {
            m_decoratedBehaviour.UpdateBehaviour(encounterCtrl);
        }
    }

    public virtual ActorActionCtrl ChooseAction(EncounterCtrl encounterCtrl)
    {
        // update actions and take highest priority action
        ActorActionCtrl actionToTake = null;

        if (m_decoratedBehaviour != null)
        {
            actionToTake = m_decoratedBehaviour.ChooseAction(encounterCtrl);
        }

        if (actionToTake == null && m_actor != null)
        {
            List<ActorActionCtrl> actions = m_actor.RegisteredActions;
            int actionCount = actions.Count;
            
            for (int i = 0; i < actionCount; i++)
            {
                ActorActionCtrl action = actions[i];

                if (action != null)
                {
                    action.UpdateAction(this, encounterCtrl);
                    int priorityRating = action.Priority;

                    if (actionToTake == null || actionToTake.Priority < action.Priority)
                    {
                        actionToTake = action;
                    }
                }
            }
        }

        return actionToTake;
    }

    public virtual IEnumerator ProcessAction(ActorActionCtrl action, EncounterCtrl encounter)
    {
        yield return action.ProcessAction(this, encounter);
    }

    public AttackConfig GetAttackConfig()
    {
        AttackConfig config = null;

        if (ActorData != null)
        {
            config = new AttackConfig
            {
                Attacker = m_actor,
                BaseAttack = ActorData.Attack
            };
        }

        return config;
    }

    public DefenceConfig GetDefenceConfig()
    {
        DefenceConfig config = null;

        if (ActorData != null)
        {
            config = new DefenceConfig { BaseDefence = ActorData.Defence };
        }

        return config;
    }

    public int GetSpeed()
    {
        int value = 0;

        if (ActorData != null)
        {
            value = ActorData.Speed;
        }

        return value;
    }
}
