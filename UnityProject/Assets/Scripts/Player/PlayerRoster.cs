using System;
using System.Collections;
using System.Collections.Generic;
using Jalopy;
using UnityEngine;

public class PlayerRoster : PlayerComponent
{         
  [SerializeField]
  PartyData m_playerRoster = null; // NOTE: temporary - this will come from a persistent, serialized party file

  public PartyData RosterData { get => m_playerRoster; }

  public override void Load(Action<PlayerComponent, Exception> loadCompletedCallback)
  {
    StartCoroutine(LoadAsync(loadCompletedCallback));
  }

  private IEnumerator LoadAsync(Action<PlayerComponent, Exception> loadCompletedCallback)
  {
    yield return new WaitForSeconds(0);
    loadCompletedCallback?.Invoke(this, null);
  }
}


