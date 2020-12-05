using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActionCtrlFactory : MonoBehaviour
{
    static Dictionary<ActionID, System.Type> m_actionDict = new Dictionary<ActionID, System.Type>() {
        { ActionID.Attack, typeof(AttackActionCtrl) }
    };

    public static Component CloneAndAttachAction(GameObject gameObject, ActionData actionData)
    {
        System.Type actionType = null;
        m_actionDict.TryGetValue(actionData.ID, out actionType);

        ActorActionCtrl actionCtrl = gameObject?.AddComponent(actionType) as ActorActionCtrl;

        if (actionCtrl)
        {
            actionCtrl.ActionData = actionData;
        }

        return actionCtrl;
    }
}
