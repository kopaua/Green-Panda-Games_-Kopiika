using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singelton<PlayerManager>
{

    public delegate void GoldCounter(int currentGold, int DestinationGold);
    public event GoldCounter OnGold;

    public int Level { get; private set; }
    public int SpeedTable { get; private set; }
    public int Gold { get; private set; }

    protected override void InitManager()
    {
        Level = 1;
        SpeedTable = 2;
    }   

    public void AddGold(int _count)
    {
        OnGold.Invoke(Gold, Gold + _count);
        Gold += _count;       
    }

}
