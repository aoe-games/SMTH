using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// The encounter controller facilitates the interations between actors and 
/// is modelled after basic turn based RPG systems. This controller also
/// leverages a state machine to break up logic into descrete paths at
/// different points in the encounter (TODO: convert to Jalopy::StateMachine)
/// </summary>
public class EncounterCtrl : MonoBehaviour
{
    enum EncounterState { Initializing, Starting, Running, Complete, Ending };
 
    EncounterState m_encounterState = EncounterState.Initializing;
    Coroutine m_runningCRT = null;

    // DEBUG
    public KeyCode m_stepKeyCode = KeyCode.Space;
    bool m_manualStepTriggered = false;

    [SerializeField]
    protected bool m_autoStepEnabled = false;
    
    [SerializeField]
    protected List<ActorCtrl> m_actors = new List<ActorCtrl>();

    [SerializeField]
    protected Queue<ActorCtrl> m_actorTurnQueue = new Queue<ActorCtrl>();

    public ReadOnlyCollection<ActorCtrl> Actors
    {
        get
        {
            ReadOnlyCollection<ActorCtrl> actors = null;

            if (m_actors != null)
            {
                actors = m_actors.AsReadOnly();
            }

            return actors;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_encounterState = EncounterState.Starting;

        if (m_actors != null && m_actors.Count > 0)
        {
            SetupActionPointCaps();
            StartEncounter();
        }
    }

    void Update()
    {
        if (m_manualStepTriggered == false)
        {
            m_manualStepTriggered = Input.GetKeyDown(m_stepKeyCode);
        }
    }

    /// <summary>
    /// On each update, each actor will add action points based on their speed.
    /// When the ActionPointCap is reached, the actor will perform an action.
    /// This method normalizes the action cap for all actors based on the fastest
    /// actor's speed so that the fastest actor will act on every update, thus
    /// ensuring there are no wasted update cycles waiting for the action cap to be hit.  
    /// </summary>
    protected void SetupActionPointCaps()
    {
        if (m_actors != null && m_actors.Count > 0)
        {            
            int highestSpeed = 0;

            // find the highest speed
            int actorCount = m_actors.Count;
            for (int i = 0; i < actorCount; i++)
            {
                ActorData actorData = GetActorDataForIndex(i);
                if (actorData != null)
                {
                    highestSpeed = Math.Max(highestSpeed, actorData.Speed);
                }
            }

            // apply the highest speed to each actor as the action point cap
            for (int i = 0; i < actorCount; i++)
            {
                ActorData actorData = GetActorDataForIndex(i);
                if (actorData != null)
                {
                    actorData.ActionPointCap = highestSpeed;
                }
            }
        }
    }

    ActorData GetActorDataForIndex(int index)
    {
        ActorData actorData = null;

        if (m_actors != null && m_actors.Count > 0)
        {
            ActorCtrl actorCtrl = m_actors[index];
            if (actorCtrl != null)
            {
                actorData = actorCtrl.ActorData;
            }
        }

        return actorData;
    }

    void StartEncounter()
    {
        m_encounterState = EncounterState.Running;
        m_runningCRT = StartCoroutine(RunEncounter());
    }

    IEnumerator RunEncounter()
    {
        while (m_encounterState == EncounterState.Running)
        {
            if (m_autoStepEnabled || m_manualStepTriggered)
            {
                foreach (ActorCtrl actor in m_actors)
                {
                    if (actor != null)
                    {
                        actor.UpdateActor(this);
                    }

                    yield return ProcessTurns();
                }

                m_manualStepTriggered = false;
            }
            else // simulation step skipped, so simply allow the coroutine to return
            {
                yield return null;
            }
        }
    }

    public void AddActor(ActorCtrl actor)
    {
        if (m_actors != null && !m_actors.Contains(actor))
        {
            m_actors.Add(actor);
        }
    }

    public void EnqueueActorTurn(ActorCtrl actor)
    {
        m_actorTurnQueue.Enqueue(actor);
    }

    IEnumerator ProcessTurns()
    {
        if (m_actorTurnQueue != null)
        {
            while (m_actorTurnQueue.Count > 0)
            {
                ActorCtrl actor = m_actorTurnQueue.Dequeue();
                yield return actor.ProcessTurn(this);
            }
        }
    }
}
