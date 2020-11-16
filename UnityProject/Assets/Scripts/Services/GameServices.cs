using Jalopy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Services/GameServices", fileName = "GameServices")]
public class GameServices : ServiceRegistry
{
  static GameServices m_services = null;

  public static GameServices Services
  {
    get => m_services;
    set
    {
      if (m_services != null )
      {
        throw new System.Exception("m_services already set");
      }

      m_services = value;
    }
  }
}
