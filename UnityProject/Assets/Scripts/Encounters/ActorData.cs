using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorData
{
    public string ID;
    public int TeamID;
    public int Health;
    public int Attack;
    public int Defence;
    public int Speed;
    public int ActionPoints;

    public override string ToString()
    {
        string str = "ID:{0} Team:[1]";
        str = string.Format(str, ID, TeamID);

        return str;            
    }
}
