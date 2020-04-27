using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionCtrl : ActorActionCtrl
{
    public override IEnumerator ProcessAction(EncounterCtrl encounterCtrl)
    {
        Debug.Log("AttackActionCtrl: ProcessAction - Started");

        if (Actor)
        {
            ActorCtrl target = Actor.GetTarget(encounterCtrl);

            if (target != null)
                Debug.Log(target.ActorData.ToString());
        }

        yield return null;
    }
}
