using Jalopy;
using Jalopy.SceneManagement;
using SMTH;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : Scene
{
  [SerializeField]
  Player m_playerPrototype;
  
  public override IEnumerator LoadScene(ProgressDelegate progressDelegate = null)
  {    
    progressDelegate(1.0f, "", (int)Status.Succeeded);
    IsLoaded = true;

    yield return null;
  }

  public override IEnumerator UnloadScene(ProgressDelegate progressDelegate = null)
  {
    IsLoaded = false;
    
    progressDelegate(1.0f, "", (int)Status.Succeeded);
    yield return null;
  }
  
  protected IEnumerator Start()
  {
    yield return new WaitUntil(() => IsLoaded);
   
    Action<Player, List<Exception>> onPlayerLoaded = 
      (player, exceptions) => 
        {
          PlayerManager playerManager = PlayerManager.Instance;
          playerManager.AddPlayer(LocalPlayer.k_localPlayerId, player);
          player.transform.parent = playerManager.transform;

          SceneManager.Instance.SwitchScene("Encounter_Dev", (GameObject)Resources.Load("Prefabs/Jalopy/UI/Loading/SimpleLoadingView"));
        };

    Player newPlayer = Instantiate(m_playerPrototype);
    newPlayer.Load(onPlayerLoaded);
  }
  
}
