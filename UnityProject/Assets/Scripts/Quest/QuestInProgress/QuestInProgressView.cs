using Jalopy;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestInProgressView : QuestView
{
  [SerializeField]
  TextMeshProUGUI m_timerTxt;

  public void SetTimeLeft(TimeSpan timeLeft)
  {
    string timerStr = DateTimeHelper.GetTimeDisplayString(timeLeft);
    m_timerTxt.text = timerStr;
  }
}
