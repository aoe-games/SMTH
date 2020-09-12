using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestEnemyRosterCtrl : QuestRosterCtrl
{
     

    public override PartyData Roster
    {
        set
        {
            base.Roster = value;

        }
    }
}
