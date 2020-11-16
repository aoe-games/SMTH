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

  [SerializeField]
  GameServices m_services;

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

    // load player
    bool playerLoaded = false;
    Action<Player, List<Exception>> onPlayerLoaded = 
      (player, exceptions) => 
        {
          PlayerManager playerManager = PlayerManager.Instance;
          playerManager.AddPlayer(LocalPlayer.k_localPlayerId, player);
          player.transform.parent = playerManager.transform;
          playerLoaded = true;
        };

    Player newPlayer = Instantiate(m_playerPrototype);
    newPlayer.Load(onPlayerLoaded);

    // load services
    bool servicesLoaded = false;
    GameServices.Services = m_services;
    GameServices.Services.LoadCompletedCallback += (exceptions) => 
      {
        servicesLoaded = true;
      };

    GameServices.Services.LoadServices(this);

    yield return new WaitUntil(() => playerLoaded && servicesLoaded);

    SceneManager.Instance.SwitchScene("QuestInterfaceScene", (GameObject)Resources.Load("Prefabs/Jalopy/UI/Loading/SimpleLoadingView"));
  }
  
}
