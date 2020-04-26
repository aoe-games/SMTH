using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionCtrl : ActorActionCtrl
{
    [SerializeField]
    protected float m_secondsToDelay = 3f;

    public float SecondsToDelay
    {
        get { return m_secondsToDelay; }
        protected set { m_secondsToDelay = value; }
    }

    public override IEnumerator ProcessAction(ActorBehaviourCtrl behaviour, EncounterCtrl encounter)
    {
        Debug.Log("TimeDelayActionCtrl started: " + Time.time);
        yield return new WaitForSeconds(SecondsToDelay);
        Debug.Log("TimeDelayActionCtrl finished: " + Time.time);
    }
}
