using Jalopy.Inventory.New;
using Jalopy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventoryViewCtrl : DataSource
{
  InventoryEntry<EquipmentItemData>[] m_equipmentList;
  EquipmentItemData m_itemSelected = null;

  EntityRoster m_heroRoster = null;

  [SerializeField]
  GameObject m_viewRoot = null;
  [SerializeField]
  EquipmentDetailsView m_equipmentDetailsView = null;
  [SerializeField]
  InfiniteScrollManager m_scrollView = null;
  int m_cellsPerView = 0;

  public override int NumberOfCells
  {
    get => (m_equipmentList.Length / m_cellsPerView) + 1;
  }

  protected void Awake()
  {
    QuestRosterCellView view = m_scrollView.m_cellPrototype.GetComponent<QuestRosterCellView>();
    if (view != null)
    {
      m_cellsPerView = view.PortraitCount;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    Player player = PlayerManager.Instance.GetPlayer(SMTH.LocalPlayer.k_localPlayerId);
    m_equipmentList = player.GetComponent<PlayerInventory>().WeaponInventory.ToArray();
    m_heroRoster = player.GetComponent<PlayerRoster>().Roster;

    m_equipmentDetailsView.Name = string.Empty;
    m_equipmentDetailsView.Description = string.Empty;
    m_equipmentDetailsView.TotalAttack = 0;
    m_equipmentDetailsView.TotalDefense = 0;
    m_equipmentDetailsView.HealthBonus = 0;
    m_equipmentDetailsView.SpeedLimit = 0;
    m_equipmentDetailsView.AssignedToName = string.Empty;
    m_equipmentDetailsView.EquipmentImagePath = string.Empty;
  }
   
  public override void CellAtIndex(InfiniteScrollCell cell, int index)
  {
    for (int i = 0; i < m_cellsPerView; i++)
    {
      QuestRosterCellView view = cell as QuestRosterCellView;
      if (view != null)
      {
        bool shouldShow = index + i < m_equipmentList.Length;
        view.ShowPortraitAtIndex(shouldShow, i);

        if (shouldShow)
        {
          EquipmentItemData itemData = m_equipmentList[index + i].Data;

          view.SetImageForIndex(itemData.VisualRefId, i);
          view.SetHighlighted(m_itemSelected != null && m_itemSelected.Id == itemData.Id, i);
        }

        view.OnCellSelectedEvent -= OnItemSelected;
        view.OnCellSelectedEvent += OnItemSelected;
      }

    }
  }

  protected void OnItemSelected(QuestRosterCellView view, int index)
  {
    EquipmentItemData data = m_equipmentList[index].Data;

    m_itemSelected = data;
    m_scrollView.RefreshView();

    m_equipmentDetailsView.Name = data.Name;
    m_equipmentDetailsView.Description = data.Description;
    m_equipmentDetailsView.TotalAttack = data.StatsData.TotalAttack;
    m_equipmentDetailsView.TotalDefense = data.StatsData.TotalDefense;
    m_equipmentDetailsView.HealthBonus = data.StatsData.Health;
    m_equipmentDetailsView.SpeedLimit = data.StatsData.Speed;
    m_equipmentDetailsView.AssignedToName = data.AssignedToId != string.Empty ? m_heroRoster[data.AssignedToId].Name : string.Empty;
    m_equipmentDetailsView.EquipmentImagePath = data.VisualRefId;

  }

  public void OnOpenSelected()
  {
    m_viewRoot.SetActive(true);
  }

  public void OnCloseSelected()
  {
    m_viewRoot.SetActive(false);
  }
}
