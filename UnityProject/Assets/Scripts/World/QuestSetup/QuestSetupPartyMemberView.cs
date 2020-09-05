using Jalopy.ResourceManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSetupPartyMemberView : MonoBehaviour
{
    EntityData m_entityData = null;

    public event Action<int> MemberSelectedEvent = null;

    public int Index { get; set; }

    #region View Elements

    [SerializeField]
    Image m_portrait = null;
    [SerializeField]
    TextMeshProUGUI m_nameTxt = null;
    [SerializeField]
    TextMeshProUGUI m_physAtkTxt = null;
    [SerializeField]
    TextMeshProUGUI m_physDefTxt = null;
    [SerializeField]
    TextMeshProUGUI m_sprtAtkTxt = null;
    [SerializeField]
    TextMeshProUGUI m_sprtDefTxt = null;
    [SerializeField]
    TextMeshProUGUI m_healthTxt = null;
    [SerializeField]
    TextMeshProUGUI m_speedTxt = null;

    #endregion

    public void UpdateView(EntityData entityData)
    {
        if (m_entityData != entityData)
        {
            m_entityData = entityData;

            if (m_entityData != null)
            {
                // reset all view elements using data model
                m_portrait.sprite = ResourceManager.Instance.Inventory.GetRawResourceAtPath<Sprite>(entityData.RosterPortraitPath);
                m_nameTxt.text = entityData.Name;
                m_healthTxt.text = entityData.MaxHealth.ToString();
                m_speedTxt.text = entityData.Speed.ToString();
                m_physAtkTxt.text = entityData.PhysicalAttack.ToString();
                m_physDefTxt.text = entityData.PhysicalDefense.ToString();
                m_sprtAtkTxt.text = entityData.SpiritualAttack.ToString();
                m_sprtDefTxt.text = entityData.SpiritualDefense.ToString();                
            }
            else
            {
                // reset to an empty view
            }
        }
    }

    public void Deselect()
    {

    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPortraitSelected()
    {
        MemberSelectedEvent?.Invoke(Index);
    }
}
