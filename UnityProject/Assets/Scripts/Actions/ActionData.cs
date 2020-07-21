using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionID : ushort
{
    Attack
}

[CreateAssetMenu(fileName = "ActionData", menuName = "ScriptableObjects/Actions/ActionData", order = 1)]
public class ActionData : ScriptableObject
{    
    [SerializeField]
    protected ActionID m_ID;
    [SerializeField]
    protected List<KeyValuePair<string, int>> m_values = new List<KeyValuePair<string, int>>();                  

    public ActionID ID { get => m_ID; }

    public IReadOnlyList<KeyValuePair<string, int>> Values { get => m_values.AsReadOnly(); }
}
