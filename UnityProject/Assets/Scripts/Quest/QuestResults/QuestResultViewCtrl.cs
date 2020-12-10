using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultViewCtrl : QuestViewCtrl
{
  [SerializeField]
  QuestResultView m_victoryView;
  [SerializeField]
  QuestResultView m_defeatView;

  public void SetConfig(ResultDisplayConfig resultViewModel)
  {
    m_questView = resultViewModel.State == ResultDisplayConfig.ResultState.Victory ? m_victoryView : m_defeatView;
  }
}
