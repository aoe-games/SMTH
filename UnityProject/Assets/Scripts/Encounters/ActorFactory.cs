using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActorFactory
{
    public static ActorCtrl CreateActorFromEntityData(EntityData entityData, int teamID, Transform parent = null)
    {
        ActorData actorData = new ActorData(entityData, teamID);

        GameObject gObj = new GameObject(
            entityData.Name,
            typeof(AttackActionCtrl)
        );                             

        gObj.transform.SetParent(parent);
                               
        ActorCtrl actorCtrl = gObj.AddComponent<ActorCtrl>();
        actorCtrl.Initialize(actorData);

        return actorCtrl;
    }



}
