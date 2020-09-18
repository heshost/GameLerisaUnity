using System;
using System.Collections.Generic;


[Serializable]
public class dataJasonOrang
{
    public string playerName;
    public List<balllist> balls;
}

[Serializable]
public class balllist
{
    public string name;
    public string description;
    public int price;
    public string image;
    public int size;
    public string weight;
    public string free;
}

[Serializable]
public class loadingImages
{
    public string image;
    
}

[Serializable]
public class startupTips
{
    public string tip;

}
