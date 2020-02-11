using System.Collections;
using System;

public enum eFood
{
    Margerita,
    Olive
}

[Serializable]
public struct Food
{
    public eFood FoodName;
    public int Cost;
    public int Parts; 
}
