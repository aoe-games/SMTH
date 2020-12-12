using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplashView : MonoBehaviour
{
  public event Action StartSelected;

  [SerializeField]
  TextMeshProUGUI m_splashText;

  public void SetSplashText(string text)
  {
    m_splashText.text = text;
  }

  public void OnStartSelected()
  {
    StartSelected?.Invoke();
  }
}
