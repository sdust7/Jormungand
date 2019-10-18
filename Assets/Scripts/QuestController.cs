﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<Quest> allQuest = new List<Quest>();

    public void LoadMission()
    {
        allQuest.Add(new Quest("0", "Find Fenrir", "Empty.", "Empty", GameObject.Find("AxeWolf").transform, true));
        //Quest quest = new Quest("0", "Cutting Trees", "Could you help me to cut 5 trees and take me the trunks? I'd like to exchange with some apples.", "10 Apples", GameObject.Find("AxeWolf").transform, true);
        allQuest.Add(new Quest("1", "Cutting Trees", "Could you help me to cut 5 trees and take me the trunks? I'd like to exchange with some potions.", "5 Health Potion  \n5 Energy Potion", GameObject.Find("AxeWolf").transform, true));
        allQuest.Add(new Quest("2", "Kill Sheep", "Kill 10 sheep and bring me the bones, I'll make you a magic weapon with them.", "Secret Weapon", GameObject.Find("FenrirLocked").transform, true));
        allQuest.Add(new Quest("3", "Beat Octopus", "Empty.", "Empty", GameObject.Find("Octopus").transform, true));
    }

}
