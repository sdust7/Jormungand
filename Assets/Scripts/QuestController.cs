using System.Collections;
using System.Collections.Generic;

public class QuestController
{
    public List<Quest> allQuest= new List<Quest>();

    public void LoadMission()
    {
        Quest quest = new Quest("0", "Annoying Trees", "These trees always hurt me!! Could you help me to cut them off? This is your axe!","10 Apples");
        allQuest.Add(quest);
    }

}
