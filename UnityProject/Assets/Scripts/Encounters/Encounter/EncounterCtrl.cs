using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// The encounter controller facilitates the interactions between actors and 
/// is modelled after basic turn based RPG systems. This controller also
/// leverages a state machine to break up logic into descrete paths at
/// different points in the encounter (TODO: convert to Jalopy::StateMachine)
/// </summary>
public class EncounterCtrl : MonoBehaviour
{
    public enum State { Stopped, Starting, Running, Ending };

    #region Fields

    protected State m_state = State.Stopped;
    Coroutine m_runningCRT = null;

    int m_participatingTeams = 0;
    
    // DEBUG
    public KeyCode m_stepKeyCode = KeyCode.Space;
    bool m_manualStepTriggered = false;

    [SerializeField]
    protected bool m_autoStepEnabled = false;      
    [SerializeField]
    protected List<ActorCtrl> m_actors = new List<ActorCtrl>();
    [SerializeField]
    protected Queue<ActorCtrl> m_actorTurnQueue = new Queue<ActorCtrl>();

    #endregion

    #region Properties

    public State EncounterState { get => m_state; }

    #endregion

    #region Events

    public event Action<ActorCtrl> ActorAdded;
    void FireActorAddedEvent(ActorCtrl actorCtrl)
    {
        ActorAdded?.Invoke(actorCtrl);
    }

    public event Action<State> StateChanged;
    void FireStateChangedEvent(State encounterState)
    {
        StateChanged?.Invoke(encounterState);
    }

    public event Action EncounterReset;
    void FireEncounterResetEvent()
    {
        EncounterReset?.Invoke();
    }

    public event Action<EncounterResultData> EncounterCompleted;
    void FireEncounterCompletedEvent(EncounterResultData encounterResult)
    {
      EncounterCompleted?.Invoke(encounterResult);
    }

  #endregion

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

    protected void SetState(State encounterState)
    {
        if (m_state != encounterState)
        {
            m_state = encounterState;
            FireStateChangedEvent(encounterState);
        }
    }

    public void Reset()
    {
        StopEncounter();

        int count = m_actors.Count;
        while (m_actors.Count > 0)
        {
            Destroy(m_actors[0].gameObject);
            m_actors.RemoveAt(0);
        }

        FireEncounterResetEvent();
    }

    public void StopEncounter()
    {
        m_actorTurnQueue.Clear();
        StopAllCoroutines(); 
        SetState(State.Stopped);
    }

    // Start is called before the first frame update
    public void StartEncounter()
    {
        SetState(State.Starting);

        SetupActors();
        RunEncounter();
    }
        
    void Update()
    {
        if (m_manualStepTriggered == false)
        {
            m_manualStepTriggered = Input.GetKeyDown(m_stepKeyCode);
        }
    }
   
    void RunEncounter()
    {
        SetState(State.Running);
        m_runningCRT = StartCoroutine(EncounterRunning());
    }

    IEnumerator EncounterRunning()
    {
        while (m_state == State.Running)
        {
            if (m_autoStepEnabled || m_manualStepTriggered)
            {
                int count = m_actors.Count;
                for (int i = 0; i < count; i++)
                {
                    // update the actors so they can run logic to queue actions
                    ActorCtrl actorCtrl = m_actors[i];
                    if (actorCtrl != null && actorCtrl.IsActive)
                    {
                        actorCtrl.UpdateActor(this);
                    }

                    // process any turns actors may have queued up during their update
                    yield return ProcessTurns();
                }

                m_manualStepTriggered = false;
            }
            else // simulation step skipped, so simply allow the coroutine to return
            {
                yield return null;
            }
        }

        StopEncounter();
        Debug.Log("EncounterCtrl.RunEncounter - exiting");
    }

    public void EnqueueActorTurn(ActorCtrl actor)
    {
        m_actorTurnQueue.Enqueue(actor);
    }

    IEnumerator ProcessTurns()
    {
        if (m_actorTurnQueue != null)
        {
            while ((m_actorTurnQueue.Count > 0) && (m_state == State.Running))
            {
                ActorCtrl actor = m_actorTurnQueue.Dequeue();
                yield return actor.ProcessTurn(this);
            }
        }
    }

    public void AddActor(ActorCtrl actorCtrl)
    {
        if (!m_actors.Contains(actorCtrl))
        {
            m_actors.Add(actorCtrl);
            FireActorAddedEvent(actorCtrl);
        }
    }

    /// <summary>
    /// On each update, each actor will add action points based on their speed.
    /// When the ActionPointCap is reached, the actor will perform an action.
    /// This method normalizes the action cap for all actors based on the fastest
    /// actor's speed so that the fastest actor will act on every update, thus
    /// ensuring there are no wasted update cycles waiting for the action cap to be hit.  
    /// </summary>
    protected void SetupActors()
    {
        if (m_actors != null && m_actors.Count > 0)
        {
            int highestSpeed = 0;
                        
            int actorCount = m_actors.Count;
            for (int i = 0; i < actorCount; i++)
            {
                // register for actor events
                ActorCtrl actorCtrl = m_actors[i];
                if (actorCtrl != null)
                {
                    m_participatingTeams |= actorCtrl.TeamID;
                    actorCtrl.KnockedOut += OnActorKnockedOut;
                }

                // find the highest speed
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

    #region Callbacks

    protected void OnActorKnockedOut(ActorCtrl actorCtrl)
    {
        int activeTeams = 0;

        int count = m_actors.Count;
        for (int i = 0; i < count; i++)
        {
            ActorCtrl actor = m_actors[i];
            if (actor != null && actor.IsActive)
            {
                activeTeams |= actor.TeamID;
            }
        }

        // when there are no longer teams available to conflict, 
        // the encounter ends
        if (activeTeams != m_participatingTeams)
        {
            SetState(State.Ending);

            EncounterResultDataBuilder builder =
              new EncounterResultDataBuilder()
              .WithWinningPartyId(activeTeams)
              .WithParticipants(GetParticipantIDs());

            FireEncounterCompletedEvent(builder.CreateResultData());
        }
    }

    List<string> GetParticipantIDs()
    {
      List<string> participants = new List<string>(m_actors.Count);

      int count = m_actors.Count;
      for (int i = 0; i < count; i++)
      {
        participants.Add(m_actors[i].ActorData.ID);
      }

      return participants; 
    }

    #endregion

}
