using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionCtrl : ActorActionCtrl
{
    public override IEnumerator ProcessAction(EncounterCtrl encounterCtrl)
    {
        // debug
        string str = "{0}.AttackActionCtrl.ProcessAction: Started";
        str = string.Format(str, Actor.ActorData.ID);
        Debug.Log(str);

        if (Actor)
        {
            ActorCtrl target = Actor.GetTarget(encounterCtrl);

            if (target != null)
            {
                AttackConfig attackConfig = null;                
                yield return Actor.PerformAttack(target, (config) => {
                    attackConfig = config;
                });

                AttackResult attackResult = null;
                yield return target.ProcessAttack(attackConfig, (result) => {
                    attackResult = result;
                });

                // debug
                str = "Target: {0} damageTaken: {1} healthRemaining: {2}";
                str = string.Format(str, target.ActorData.ToString(), attackResult.DamageTaken, target.ActorData.Health);
                Debug.Log(str);
            }
        }

        yield return null;
    }
}
