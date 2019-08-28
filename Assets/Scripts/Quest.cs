using System.Collections;
using System.Collections.Generic;

public class Quest
{
    public string ID;
    public string questName;
    public string description;
    public string reward;

    public Quest(string ID, string name, string des,string reward)
    {
        this.ID = ID;
        questName = name;
        description = des;
        this.reward = reward;
    }
    

    
}
