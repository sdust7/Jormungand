using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public string name;
    public bool usable;
    public int count;

    public Items()
    {
        name = "Empty";
        usable = false;
        count = 1;
    } 

    public Items(string name, bool usable, int count)
    {
        this.name = name;
        this.usable = usable;
        this.count = count;
    }
}
