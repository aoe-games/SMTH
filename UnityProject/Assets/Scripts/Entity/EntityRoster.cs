using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityRoster
{
  public event Action<EntityData> EntityDataChanged;

  Dictionary<string, EntityData> m_entityData = new Dictionary<string, EntityData>();

  public void SetEntityData(EntityData entityData)
  {
    m_entityData[entityData.ID] = entityData;
    EntityDataChanged?.Invoke(entityData);
  }

  public void RemoveEntityData(EntityData entityData)
  {
    m_entityData.Remove(entityData.ID);
    EntityDataChanged?.Invoke(entityData);
  }

  public EntityData GetEntityData(string ID)
  {
    EntityData entityData = null;
    m_entityData.TryGetValue(ID, out entityData);
    return entityData;
  }

  public EntityData[] GetEntityDataCollection()
  {
    EntityData[] entityData = new EntityData[m_entityData.Count];
    m_entityData.Values.CopyTo(entityData, 0);
    return entityData;
  }
}
