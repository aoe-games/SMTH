using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory : MonoBehaviour
{
    [SerializeField]
    ActionRegistry m_actionRegistry = null;

    public ActorCtrl CreateActor(ActorData actorData)
    {
        GameObject actorObject = new GameObject(actorData.Name);
        actorObject.transform.SetParent(transform);

        ActorCtrl actorCtrl = actorObject.AddComponent<ActorCtrl>();
        actorCtrl.Initialize(actorData);

        // for each action ID, create an action controller for this actor game object
        foreach (ActionID actionID in actorData.Actions)
        {
            ActionData actionData = m_actionRegistry[actionID];
            ActorActionCtrlFactory.CloneAndAttachAction(actorObject, actionData);
        }

        return actorCtrl;
    }
}
