using System.Collections;
using System.Collections.Generic;

public class QuestController
{
    public List<Quest> allQuest = new List<Quest>();

    public void LoadMission()
    {
        Quest quest = new Quest("0", "Cutting Trees", "Could you help me to cut 5 trees and take me the trunks? I'd like to exchange with some apples.", "10 Apples");
        allQuest.Add(quest);


    }

}
