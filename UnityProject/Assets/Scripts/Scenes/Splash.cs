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
  [SerializeField]
  SplashView m_splashView;

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
    m_splashView.SetSplashText("Loading...");
    yield return new WaitUntil(() => IsLoaded);

    // load player    
    bool playerLoaded = false;
    Player player = Instantiate(m_playerPrototype);
    Action<Player, List<Exception>> onPlayerLoaded = 
      (loadedPlayer, exceptions) => 
        {
          player = loadedPlayer;
          PlayerManager playerManager = PlayerManager.Instance;
          playerManager.AddPlayer(LocalPlayer.k_localPlayerId, player);
          player.transform.parent = playerManager.transform;          
          playerLoaded = true;
        };    
    player.Load(onPlayerLoaded);

    // load services
    bool servicesLoaded = false;
    GameServices.Services = m_services;
    Action<Exception[]> onServicesLoaded =
      (exceptions) =>
        {
          GameServices.Services.GetService<QuestManager>().SetPlayer(player);
          servicesLoaded = true;
        };
    GameServices.Services.LoadCompletedCallback += onServicesLoaded;
    GameServices.Services.LoadServices(this);

    yield return new WaitUntil(() => playerLoaded && servicesLoaded);
    yield return new WaitForSeconds(2.0f);

    GameServices.Services.LoadCompletedCallback -= onServicesLoaded;
    m_splashView.SetSplashText("Press to Start");
    m_splashView.StartSelected += OnStartSelected;
  }
  
  public void OnStartSelected()
  {
    m_splashView.StartSelected -= OnStartSelected;
    SceneManager.Instance.SwitchScene("QuestInterfaceScene", (GameObject)Resources.Load("Prefabs/Jalopy/UI/Loading/SimpleLoadingView"));
  }
}
