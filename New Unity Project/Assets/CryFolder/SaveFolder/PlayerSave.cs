using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public int level;
    public string Playername;

    public PlayerSave(int level, string Playername)
    {
        this.level = level;
        this.Playername = Playername;
    }
}
