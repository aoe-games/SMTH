using Jalopy.Inventory.New;
using Jalopy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventoryViewCtrl : DataSource
{
  InventoryEntry<EquipmentItemData>[] m_equipmentList;
  EquipmentItemData m_itemSelected = null;

  [SerializeField]
  GameObject m_viewRoot = null;
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
    m_itemSelected = m_equipmentList[index].Data;
    m_scrollView.RefreshView();
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
