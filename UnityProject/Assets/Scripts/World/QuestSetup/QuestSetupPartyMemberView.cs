using Jalopy.ResourceManagement;
using System;
using System.Collections;
using System.Collections.Generic;
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
