using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelayActionCtrl : ActorActionCtrl
{
    [SerializeField]
    protected float m_secondsToDelay = 3f;

    public float SecondsToDelay
    {
        get { return m_secondsToDelay; }
        protected set { m_secondsToDelay = value; }
    }

    public TimeDelayActionCtrl(float secondsToDelay)
    {
        SecondsToDelay = secondsToDelay;
    }

    public override void UpdateAction(EncounterCtrl encounter)
    {
        
    }

    public override IEnumerator ProcessAction(EncounterCtrl encounter)
    {
        Debug.Log("TimeDelayActionCtrl started: " + Time.time);
        yield return new WaitForSeconds(SecondsToDelay);
        Debug.Log("TimeDelayActionCtrl finished: " + Time.time);
    }
}
