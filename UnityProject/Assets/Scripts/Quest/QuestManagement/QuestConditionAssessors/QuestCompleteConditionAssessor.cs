using Jalopy;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using SMTH;

public static class QuestCompleteConditionAssessor
{
  public static bool AssessCondition(QuestCondition questCompleteCondition, QuestStateDatabase questStateDatabase)
  {
    bool conditionMet = false;
              
    QuestStateData stateData = questStateDatabase[questCompleteCondition.ID];
    if (stateData != null)
    {
      conditionMet = stateData.QuestStatus == QuestStateData.Status.Complete;
    }

    return conditionMet;
  }
}
