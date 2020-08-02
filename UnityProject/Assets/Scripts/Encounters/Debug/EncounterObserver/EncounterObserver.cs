using Jalopy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterObserver : DataSource
{
    public List<ActorData> m_actorData = new List<ActorData>();

    [SerializeField]
    EncounterCtrl m_encounterCtrl;
    [SerializeField]
    InfiniteScrollManager m_actorScrollView;

    public override int NumberOfCells
    {
        get => m_actorData.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_encounterCtrl.ActorAdded += OnActorAdded;
        m_encounterCtrl.StateChanged += OnEncounterStateChanged;
        m_encounterCtrl.EncounterReset += OnEncounterReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_encounterCtrl.EncounterState == EncounterCtrl.State.Running)
        {
            RefreshListView();
        }
    }

    public void RefreshListView()
    {
        m_actorScrollView?.RefreshView();
    }

    public override void CellAtIndex(InfiniteScrollCell cell, int index)
    {
        ActorView view = cell as ActorView;
        if (view)
        {
            ActorData actorData = m_actorData[index];
            view.Name = actorData.Name;
            view.HealthRatio = (float)actorData.Health / actorData.MaxHealth;
        }
    }

    #region EncounterCtrl Callbacks

    void OnActorAdded(ActorCtrl actorAdded)
    {
        ActorData actorData = actorAdded.ActorData;
        m_actorData.Add(actorData);
    }

    void OnEncounterReset()
    {
        m_actorData.Clear();
    }

    void OnEncounterStateChanged(EncounterCtrl.State encounterState)
    {
        // reset scrolling view with actors
        switch (encounterState)
        {
            case EncounterCtrl.State.Starting:
                {
                    m_actorScrollView.ResetView();
                }
                break;
            case EncounterCtrl.State.Stopped:
                {
                    RefreshListView();
                }
                break;
            default:
                break;
        }
    }

    #endregion
}
