using System.Collections.Generic;
using UnityEngine;

namespace Encounter2
{

  [System.Serializable]
  public class Entity
  {
    public Entity(Entity e) : this(e.ID, e.TeamID)
    {
      Attack = e.Attack;
      Defence = e.Defence;
      Health = e.Health;
      Speed = e.Speed;
    }

    public Entity(int id, int teamID)
    {
      ID = id;
      TeamID = teamID;
      ActionPoints = Speed;
    }

    public const int HERO_TEAM = 1;
    public const int ENEMY_TEAM = 2;

    public int ID = -1;
    public int TeamID = -1;

    // stats
    public int Attack = 10;
    public int Defence = 2;
    public int Health = 100;
    public int Speed = 10;

    // combat variables
    public int ActionPoints;    
  }

  public class EncounterPrototype2 : MonoBehaviour
  {
    public int Simulations = 100;
    public bool Debug_ShowActionOrder = true;

    public Entity[] heroes = { new Entity(1, Entity.HERO_TEAM), new Entity(2, Entity.HERO_TEAM) };
    public Entity[] opponents = { new Entity(3, Entity.ENEMY_TEAM), new Entity(4, Entity.ENEMY_TEAM) };

    protected int GetTargetUsingTeamID(int targetTeamID, IReadOnlyList<Entity> potentialTargets)
    {
      // select the target with the most remaining health

      int index = -1;
      int count = potentialTargets.Count;
      for (int i = 0; i < count; i++)
      {
        Entity e = potentialTargets[i];
        if (e.TeamID == targetTeamID)
        {
          if (index == -1 || e.Health > potentialTargets[i].Health)
          {
            index = i;
          }
        }
      }

      return index;
    }

    protected void SetupEntities(ref List<Entity> activeEntities)
    {
      activeEntities.Clear();
      activeEntities.Capacity = heroes.Length + opponents.Length;

      activeEntities.Add(new Entity(heroes[0]));
      activeEntities.Add(new Entity(heroes[1]));
      activeEntities.Add(new Entity(opponents[0]));
      activeEntities.Add(new Entity(opponents[1]));
      //activeEntities.AddRange(heroes);
      //activeEntities.AddRange(opponents);
    }

    public void OnEnable()
    {
      List<Entity> activeEntities = new List<Entity>();
      SetupEntities(ref activeEntities);

      int totalHeroHealth = 0;
      foreach (Entity e in activeEntities)
      {
        if (e.TeamID == Entity.HERO_TEAM)
        {
          totalHeroHealth += e.Health;
        }
      }
           
      int participatingTeams = 1 | 2;

      bool isBattleInProgress = true;
      while (isBattleInProgress)
      {
        // setup actionList based on speed
        activeEntities.Sort(delegate (Entity e1, Entity e2)
        {
          return Mathf.RoundToInt(e2.ActionPoints - e1.ActionPoints);
        });

        // complete activity round
        Entity actor = activeEntities[0];
        actor.ActionPoints = 0;

        // select a target to hit
        int targetIndex = -1;        
        if (actor.TeamID == 1)
        {
          // select an opponent
          targetIndex = GetTargetUsingTeamID(2, activeEntities);
        }
        else
        {
          // select a hero
          targetIndex = GetTargetUsingTeamID(1, activeEntities);
        }

        Entity targetEntity = activeEntities[targetIndex];

        // attack target
        targetEntity.Health -= actor.Attack - targetEntity.Defence;

        // if target is KO'd, remove them from the action sequence
        if (targetEntity.Health <= 0)
        {
          activeEntities.Remove(targetEntity);

          // see if there are any opponents left for either team
          int teamFlags = 0;
          for (int i = 0; i < activeEntities.Count; i++)
          {
            teamFlags |= activeEntities[i].TeamID;            
          }

          // if there are no opponents, stop computing
          if (teamFlags != participatingTeams)
          {
            isBattleInProgress = false;
            break;
          }
        }

        // update action points
        foreach (Entity e in activeEntities)
        {
          e.ActionPoints += e.Speed;
        }

      } // continue to next action round

      // encounter statistics
      int remainingHeroHealth = 0;
      foreach (Entity e in activeEntities)
      {
        if (e.TeamID == Entity.HERO_TEAM)
        {
          remainingHeroHealth += e.Health;
        }
      }
      Debug.Log("result: " + (float)remainingHeroHealth / totalHeroHealth);
    }

  }

}
