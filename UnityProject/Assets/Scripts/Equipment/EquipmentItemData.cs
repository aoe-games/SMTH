using Jalopy.Inventory.New;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObjects/Equipment/Equipment Data", fileName = "EquipmentData")]
public class EquipmentItemData : IItemData
{
  [SerializeField]
  private string m_id;
  [SerializeField]
  private string m_name;
  [SerializeField]
  private int m_tag;
  [SerializeField]
  private string m_assignedToId;
  [SerializeField]
  private StatsData m_statsData;
  [SerializeField]
  private string m_visualRefId;

  public string Id { get => m_id; }
  public string Name { get => m_name; }
  public int Tag { get => m_tag; }
  public string AssignedToId { get => m_assignedToId; }
  public StatsData StatsData { get => m_statsData; }
  public string VisualRefId { get => m_visualRefId; }

  public EquipmentItemData(
    string id, 
    string name, 
    int tag, 
    string assignedToId, 
    StatsData statsData, 
    string visualRefId)
  {
    m_id = id;
    m_name = name;
    m_tag = tag;
    m_assignedToId = assignedToId;
    m_statsData = statsData;
    m_visualRefId = visualRefId;
  }
}
