using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    protected List<ActorCtrl> m_actors = new List<ActorCtrl>();

    [SerializeField]
    protected Queue<ActorCtrl> m_actorTurnQueue = new Queue<ActorCtrl>();

    // Start is called before the first frame update
    void Start()
    {
        m_encounterState = EncounterState.Starting;

        if (m_actors != null && m_actors.Count > 0)
        {
            StartEncounter();
        }
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
            foreach (ActorCtrl actor in m_actors)
            {
                if (actor != null)
                {
                    actor.UpdateActor(this);
                }

                yield return ProcessTurns();
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
