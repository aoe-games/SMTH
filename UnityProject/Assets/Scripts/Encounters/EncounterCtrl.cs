using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterCtrl : MonoBehaviour
{
    bool m_isEncounterRunning = true;

    Coroutine m_coroutine = null;

    [SerializeField]
    protected List<ActorCtrl> m_actors = new List<ActorCtrl>();

    [SerializeField]
    protected Queue<ActorCtrl> m_actorTurnQueue = new Queue<ActorCtrl>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_coroutine = StartCoroutine(RunEncounter());
    }

    IEnumerator RunEncounter()
    {
        while (m_isEncounterRunning)
        {
            if (m_actorTurnQueue != null)
            {
                while (m_actorTurnQueue.Count > 0)
                {
                    ActorCtrl actor = m_actorTurnQueue.Dequeue();
                    yield return actor.ProcessTurn(this);
                }
            }

            if (m_actors != null)
            { 
                foreach (ActorCtrl actor in m_actors)
                {
                    actor.ActorUpdate(this);
                }
            }

            yield return new WaitForEndOfFrame();
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
}
