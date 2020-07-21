using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionRegistry", menuName = "ScriptableObjects/Actions/ActionRegistry", order = 1)]
public class ActionRegistry : ScriptableObject
{
    [SerializeField]
    public List<ActionData> m_actionDataList = new List<ActionData>();

    Dictionary<ActionID, ActionData> m_actionDataDict = new Dictionary<ActionID, ActionData>();

    public ActionData this[ActionID actionID] { get => GetActionDataForID(actionID); }

    public ActionData GetActionDataForID(ActionID actionID)
    {
        // map action data to dictionary for more efficient look-up at runtime
        if (m_actionDataDict.Count <= 0)
        {
            foreach (ActionData data in m_actionDataList)
            {
                m_actionDataDict.Add(data.ID, data);
            }
        }                                                         

        ActionData actionData = m_actionDataDict.ContainsKey(actionID) ? m_actionDataDict[actionID] : null;       

        return actionData;
    }
}
