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
    [SerializeField]
    protected ActorBehaviourCtrl m_behaviourCtrl = null;

    protected Queue<ActorActionCtrl> m_actionQueue = new Queue<ActorActionCtrl>();

    [SerializeField]
    protected List<ActorActionCtrl> m_registeredActions = new List<ActorActionCtrl>();
    public List<ActorActionCtrl> RegisteredActions { get { return m_registeredActions; } }

    private void Awake()
    {
        m_behaviourCtrl = new ActorBehaviourCtrl(this);
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

}
