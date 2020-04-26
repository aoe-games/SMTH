using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All logic pertaining to what an actor does resides in these action
/// controller classes, unlike behaviours that provide context for how an
/// actor should do it. 
/// </summary>
/// 
/// For example, an actor's behaviour may decide an attack is a good action to take on 
/// the actor's next turn. When it's time to act, the attack action then processes an attack 
/// by finding targets, getting an attack value, and then invoking the attack interface on the
/// target. The values for each of those steps is diven by the actor's behaviour. If the
/// actor is confused, the list of targets might change to allies. If the actor is enraged,
/// the attack value of the actor may rise, but their accuracy falls. In any case, this does
/// not change what an actor does, but does impact how they do it in a data driven manner.
/// 
/// CREATING NEW ACTIONS
/// To create a new action, simply derive from this class
/// and ensure the action gets registered with the actor. By moving this functionality
/// into separate classes, the system becomes more scalable since the actor class
/// does not need to grow, but is an still take any number of actions.
public abstract class ActorActionCtrl : MonoBehaviour
{
    [SerializeField]
    protected ActorCtrl m_actor;

    [SerializeField]
    protected int m_priorityRating;

    public int Priority { get { return m_priorityRating; } }

    public virtual void UpdateAction(ActorBehaviourCtrl behaviour, EncounterCtrl encounter) { }

    public ActorCtrl Actor
    {
        get { return m_actor; }
        set { m_actor = value; }
    }

    public abstract IEnumerator ProcessAction(ActorBehaviourCtrl behaviour, EncounterCtrl encounter);
}
