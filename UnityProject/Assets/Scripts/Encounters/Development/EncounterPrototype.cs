using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jalopy;

namespace Encounter
{

  public class EncounterPrototype : MonoBehaviour
  {
    public int Simulations = 100;
    public bool Debug_ShowActionOrder = true;

    public Entity[] heroes = { new Entity(1, 1), new Entity(2, 1) };
    public Entity[] opponents = { new Entity(3, 2), new Entity(4, 2) };

    protected int GetTargetUsingTeamID(int targetTeamID, IReadOnlyList<Entity> potentialTargets)
    {
      WeightedRandom<int> targetIndices = new WeightedRandom<int>();
      for (int i = 0; i < potentialTargets.Count; i++)
      {
        Entity e = potentialTargets[i];
        if (e.TeamID == targetTeamID)
        {
          targetIndices.Add(i, e.Agro);
        }
      }

      int index = targetIndices.Get();
      return index;
    }

    public void OnEnable()
    {
      Dictionary<int, int> teamWins = new Dictionary<int, int>();

      int simulations = Simulations;
      for (int s = 0; s < simulations; s++)
      {
        List<Entity> activeEntities = new List<Entity>();
        //activeEntities.AddRange(heroes);
        //activeEntities.AddRange(opponents);        

        activeEntities.Add(new Entity(heroes[0]));
        activeEntities.Add(new Entity(heroes[1]));
        activeEntities.Add(new Entity(opponents[0]));
        activeEntities.Add(new Entity(opponents[1]));

        int participatingTeams = 1 | 2;

        bool isBattleInProgress = true;
        while (isBattleInProgress)
        {
          // setup actionList based on speed
          WeightedRandom<Entity> speedCheckPool = new WeightedRandom<Entity>();
          for (int i = 0; i < activeEntities.Count; i++)
          {
            Entity e = activeEntities[i];
            speedCheckPool.Add(e, e.Guile);
          }

          activeEntities.Clear();

          while (speedCheckPool.Count > 0)
          {
            Entity e = speedCheckPool.Get();
            activeEntities.Add(e);
            speedCheckPool.Remove(e);
          }

          if (Debug_ShowActionOrder)
          {
            string order = "";
            order += activeEntities[0].ID;
            for (int spd = 1; spd < activeEntities.Count; spd++)
            {
              order += ", " + activeEntities[spd].ID;
            }
            Debug.Log("Turn Order:" + order);
          }

          // run through action list to complete activity round
          for (int j = 0; j < activeEntities.Count; j++)
          {
            Entity actor = activeEntities[j];
            int targetIndex = -1;

            // select a target to hit
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
              for (int k = 0; k < activeEntities.Count; k++)
              {
                teamFlags |= activeEntities[k].TeamID;
              }

              if (teamFlags == participatingTeams)
              {
                // there are still opponents on both teams, so keep computing
                j--; // counter the for-loop iterator since we have changed the size of the collection
              }
              else
              {
                isBattleInProgress = false;
                break;
              }
            }

          } // continue to next action in the round 

        } // continue to next action round

        // encounter statistics
        Entity winner = activeEntities[0];        
        if (!teamWins.ContainsKey(winner.TeamID))
        {
          teamWins.Add(winner.TeamID, 0);
        }
        teamWins[winner.TeamID] += 1;
 
      } // continue the simulation

      foreach (var kp in teamWins)
      {
        Debug.Log("Team: " + kp.Key + " wins: %" + 100 * ((float)kp.Value/simulations) );
      }
    }

  }

  [System.Serializable]
  public class Entity
  {
    public Entity(Entity e) : this(e.ID, e.TeamID) { }
    public Entity(int id, int teamID) { ID = id; TeamID = teamID; }

    public int ID = -1;
    public int Level = 1;
    public float Attack = 10;
    public float Defence = 2;
    public float Health = 100;
    public float Agro = 10;
    public float Guile = 10;
    public float CriticalHit = 0.1f;
    public float Evasion = 0.1f;

    public int TeamID = -1;
  }

}
