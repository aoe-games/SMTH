using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCtrl : MonoBehaviour
{
    public KeyCode m_debugKeyCode = KeyCode.Space;
    bool m_keyDown = false;

    protected Queue<ActorActionCtrl> m_actionQueue = new Queue<ActorActionCtrl>();

    [SerializeField]
    protected List<ActorActionCtrl> m_registeredActions = new List<ActorActionCtrl>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // TODO: Move this to a behaviour class
        m_keyDown = Input.GetKeyDown(m_debugKeyCode);
    }

    public void ActorUpdate(EncounterCtrl encounterCtrl)
    {
        // TODO: Move this to a behaviour class
        if (m_keyDown)
        {
            m_keyDown = false;
            encounterCtrl.EnqueueActorTurn(this);

            // update actions and take highest priority action
            ActorActionCtrl actionToTake = null;
            for (int i = 0; i < m_registeredActions.Count; i++)
            {
                ActorActionCtrl controller = m_registeredActions[i];

                if (controller != null)
                {
                    controller.UpdateAction(encounterCtrl);
                    int priorityRating = controller.Priority;

                    if (actionToTake == null || actionToTake.Priority < controller.Priority)
                    {
                        actionToTake = controller;
                    }
                }
            }

            m_actionQueue.Enqueue(actionToTake);
        }
    }

    public IEnumerator ProcessTurn(EncounterCtrl encounterCtrl)
    {
        // process enqueued actions
        while (m_actionQueue.Count > 0)
        {
            ActorActionCtrl controller = m_actionQueue.Dequeue();
            if (controller != null)
            {
                yield return controller.ProcessAction(encounterCtrl);
            }
        }
    }

}
