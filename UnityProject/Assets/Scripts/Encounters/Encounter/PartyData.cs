using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyData", menuName = "ScriptableObjects/PartyData", order = 1)]
public class PartyData : ScriptableObject
{
    // TODO: protect these fields        
    [SerializeField]
    public string m_name = string.Empty;
    [SerializeField]
    public List<EntityData> m_partyMembers = new List<EntityData>();
}
