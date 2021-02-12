using Jalopy.ResourceManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class EquipmentDetailsView : MonoBehaviour
{
  [SerializeField]
  TMP_Text m_nameTxt;
  [SerializeField]
  TMP_Text m_descriptionTxt;
  [SerializeField]
  TMP_Text m_totalAttackTxt;
  [SerializeField]
  TMP_Text m_totalDefenseTxt;
  [SerializeField]
  TMP_Text m_bonusHealthTxt;
  [SerializeField]
  TMP_Text m_topSpeedTxt;
  [SerializeField]
  TMP_Text m_assignedToTxt;
  [SerializeField]
  Image m_equipmentPortrait;

  public string Name { set => m_nameTxt.text = value; }
  public string Description { set => m_descriptionTxt.text = value; }
  public int TotalAttack { set => m_totalAttackTxt.text = value.ToString(); }
  public int TotalDefense { set => m_totalDefenseTxt.text = value.ToString(); }
  public int HealthBonus { set => m_bonusHealthTxt.text = value.ToString(); }
  public int SpeedLimit { set => m_topSpeedTxt.text = value.ToString(); }
  public string AssignedToName { set => m_assignedToTxt.text = "Assigned To: " + value; }
  public string EquipmentImagePath
  {
    set
    {
      if (string.CompareOrdinal(value, string.Empty) == 0)
      {
        m_equipmentPortrait.gameObject.SetActive(false);
      }
      else
      {
        m_equipmentPortrait.sprite = ResourceManager.Instance.Inventory.GetRawResourceAtPath<Sprite>(value);
        m_equipmentPortrait.gameObject.SetActive(true);
      }
    }
  }
}
