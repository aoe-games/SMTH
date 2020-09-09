using Jalopy.ResourceManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSetupPartyMemberView : MonoBehaviour
{
    const string k_unknownPortraitPath = "Sprites/Development/Portraits/portrait_unknown";

    EntityData m_entityData = null;

    public event Action<int> MemberSelectedEvent = null;
    public event Action<int> RemoveSelectedEvent = null;

    public int index { get; set; }

    #region View Elements

    [SerializeField]
    QuestSetupPartyMemberPortraitView m_portraitView = null;
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
    [SerializeField]
    Button m_removeBtn = null;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        ResetView();
    }

    public void UpdateView(EntityData entityData)
    {
        if (m_entityData != entityData)
        {
            m_entityData = entityData;

            if (m_entityData != null)
            {
                // reset all view elements using data model
                m_removeBtn.gameObject.SetActive(true);
                m_portraitView.SetPortraitImage(entityData.RosterPortraitPath);
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
                ResetView(); 
            }
        }
    }

    void ResetView()
    {
        // reset to an empty view
        m_removeBtn.gameObject.SetActive(false);
        m_portraitView.SetPortraitImage(k_unknownPortraitPath);
        m_nameTxt.text = "Select Hero";

        const string defaultValue = "-";
        m_healthTxt.text = defaultValue;
        m_speedTxt.text = defaultValue;
        m_physAtkTxt.text = defaultValue;
        m_physDefTxt.text = defaultValue;
        m_sprtAtkTxt.text = defaultValue;
        m_sprtDefTxt.text = defaultValue;
    }

    public void SetSelected(bool isSelected)
    {
        m_portraitView.SetHighlighted(isSelected);
    }
   
    public void OnPortraitSelected()
    {
        MemberSelectedEvent?.Invoke(index);
    }

    public void OnRemoveSelected()
    {
        RemoveSelectedEvent?.Invoke(index);
    }
}
