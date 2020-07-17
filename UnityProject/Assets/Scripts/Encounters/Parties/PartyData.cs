using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PartyData", order = 1)]
public class PartyData : ScriptableObject
{
    [SerializeField]
    public int m_teamID = 0;

    [SerializeField]
    public List<EntityData> m_partyMembers = new List<EntityData>();
}
