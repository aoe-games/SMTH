using Jalopy;
using Jalopy.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInterfaceScene : Scene
{
  public override IEnumerator LoadScene(ProgressDelegate progressDelegate = null)
  {
    bool isLoaded = false;
    StartCoroutine(SceneManager.Instance.LoadScene("World", (progress, message, code) => {
      isLoaded = progress >= 1.0f;
      progressDelegate(progress, message, (int)Status.Succeeded);
    }, false));

    yield return new WaitUntil(() => isLoaded);

    IsLoaded = isLoaded;
    yield return null;
  }

  public override IEnumerator UnloadScene(ProgressDelegate progressDelegate = null)
  {
    IsLoaded = false;

    progressDelegate(1.0f, "", (int)Status.Succeeded);
    yield return null;
  }
}
