using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<Quest> allQuest = new List<Quest>();

    public void LoadMission()
    {
        allQuest.Add(new Quest("0", "Find Fenrir", "Empty.", "Empty", GameObject.Find("AxeWolf").transform, true));
        //Quest quest = new Quest("0", "Cutting Trees", "Could you help me to cut 5 trees and take me the trunks? I'd like to exchange with some apples.", "10 Apples", GameObject.Find("AxeWolf").transform, true);
        allQuest.Add(new Quest("1", "Cutting Trees", "Could you help me to cut 5 trees and take me the trunks? I'd like to exchange with some apples.", "10 Apples", GameObject.Find("AxeWolf").transform, true));


    }

}
