using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string ID;
    public string questName;
    public string description;
    public string reward;
    public bool finished;
    public Transform targetTrans;
    public bool showMapMark;

    public Quest(string ID, string name, string des, string reward, Transform targetTransform, bool showMiniMapMark)
    {
        this.ID = ID;
        questName = name;
        description = des;
        this.reward = reward;
        finished = false;
        targetTrans = targetTransform;
        showMapMark = showMiniMapMark;
    }

    //public Quest(string iD, string questName, string description, string reward, bool finished, Transform targetTransform)
    //{
    //    ID = iD;
    //    this.questName = questName;
    //    this.description = description;
    //    this.reward = reward;
    //    this.finished = finished;
    //    this.targetTransform = targetTransform;
    //}
}
