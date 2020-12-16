using System;
using System.Collections;
using System.Collections.Generic;
using Jalopy;
using UnityEngine;

public class PlayerRoster : PlayerComponent
{
  [SerializeField]
  PartyData m_initialRosterData = null; // NOTE: temporary - this will come from a persistent, serialized party file

  public EntityRoster Roster { get; } = new EntityRoster();

  public override void Load(Action<PlayerComponent, Exception> loadCompletedCallback)
  {
    StartCoroutine(LoadAsync(loadCompletedCallback));
  }

  private IEnumerator LoadAsync(Action<PlayerComponent, Exception> loadCompletedCallback)
  {
    yield return new WaitForSeconds(0);

    foreach (EntityData entityData in m_initialRosterData.m_partyMembers)
    {
      Roster.SetEntityData(entityData.Clone());
    }
    m_initialRosterData = null;

    loadCompletedCallback?.Invoke(this, null);
  }
}


