using Jalopy;
using Jalopy.Inventory.New;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : PlayerComponent
{
  Inventory<EquipmentItemData> m_weaponInventory = new Inventory<EquipmentItemData>();

  public Inventory<EquipmentItemData> WeaponInventory
  {
    get => m_weaponInventory;
  }

  public override void Load(Action<PlayerComponent, Exception> loadCompletedCallback)
  {
    StatsData statsData = new StatsData(5, 6, 3, 1, 1, 2, 3, 1);
    EquipmentItemData equipmentData = new EquipmentItemData("broadsword_00001", "First Sword", "The first sword created for S&L", 0, "sandra_ranger", statsData, "Sprites/Development/Equipment/Weapons/sword_broadsword");
    InventoryEntry<EquipmentItemData> entry = new InventoryEntry<EquipmentItemData>(0, 0, int.MaxValue, equipmentData);
    m_weaponInventory.AddEntry(entry);

    statsData = new StatsData(2, 14, 7, 1, 0, 2, 2, 0);
    equipmentData = new EquipmentItemData("warhammer_00001", "First Hammer", "The first hammer created for S&L", 0, "dwayne_brawler", statsData, "Sprites/Development/Equipment/Weapons/hammer_dwarven");
    entry = new InventoryEntry<EquipmentItemData>(0, 0, int.MaxValue, equipmentData);
    m_weaponInventory.AddEntry(entry);

    base.Load(loadCompletedCallback);
  }
}
