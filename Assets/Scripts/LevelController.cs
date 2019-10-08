using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    private UIController canvas;
    private UIController UIPanel;
    private Transform questPanel;
    private SnakeController snake;
    public List<Vector3> usedPoints;
    public GameObject Apples;
    public MiniMapMark miniMapMark;
    public TextMeshProUGUI currentQuestNameTMP;
    private ToolBar toolBar;

    public float speed;
    public int score;
    public int goal;
    //public int wood;
    public int apple;
    public QuestController questController;
    public List<Quest> myQuest;
    public int currentQuestID;

    public float xValueStartDesert = 160.0f;
    public float xValueStartSea = -160.0f;

    public Vector3[] mannulUsedPoints;



    void Awake()
    {
        myQuest = new List<Quest>();
        questController = new QuestController();
        questController.LoadMission();
        //wood = 4;
        apple = 0;
        usedPoints = new List<Vector3>();
        usedPoints.Add(new Vector3(0, 0, 0));
        //
        foreach (var item in mannulUsedPoints)
        {
            usedPoints.Add(item);
        }
        //
        canvas = GameObject.Find("Canvas").GetComponent<UIController>();
        UIPanel = GameObject.Find("UIPanel").GetComponent<UIController>();
        questPanel = GameObject.Find("MissionPanel").transform.GetChild(0).transform;

        snake = GameObject.Find("SnakeHead").GetComponent<SnakeController>();
        miniMapMark = GameObject.Find("MiniMapMark").GetComponent<MiniMapMark>();
        currentQuestNameTMP = GameObject.Find("QuestInfoPanel").transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        toolBar = GameObject.Find("ToolBar").GetComponent<ToolBar>();

        currentQuestID = -1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubmitQuest(string id)
    {
        switch (id)
        {
            //case "0":

            //    break;
            case "1":

                if (toolBar.GetItemCount("Wood") >= 5)
                {
                    //wood -= 5;
                    //
                    toolBar.SetItemCount("Wood", -5);
                    toolBar.GotItem(new Items("HealthPotion", true, 1), 5);
                    toolBar.GotItem(new Items("EnergyPotion", true, 1), 5);
                    //UIPanel.UpdateWood(wood);
                    //GameObject reward = Instantiate(Apples) as GameObject;
                    //reward.transform.position = snake.transform.position + new Vector3(2, 2, 0);
                    Quest quest = questController.allQuest.Find(x => x.ID.Equals(id));
                    RemoveQuest(quest);
                    quest.finished = true;
                    miniMapMark.EndShowMark();
                }
                else
                {

                }
                break;
            default:
                break;
        }
    }

    public void ShowMissionPanel(Quest quest)
    {
        questPanel.gameObject.SetActive(true);
        questPanel.GetChild(0).name = quest.ID.ToString();
        questPanel.GetChild(1).GetComponent<TextMeshProUGUI>().text = quest.questName;
        questPanel.GetChild(2).GetComponent<TextMeshProUGUI>().text = quest.description + "\nReward: " + quest.reward;

    }

    public void AddQuest(Quest newQuest)
    {
        if (currentQuestID == myQuest.Count - 1)
        {
            myQuest.Add(newQuest);
            currentQuestID++;
        }
        else
        {
            myQuest.Insert(currentQuestID + 1, newQuest);
            currentQuestID++;
        }
        currentQuestNameTMP.text = myQuest[currentQuestID].questName;
    }

    public void RemoveQuest(Quest quest)
    {
        for (int i = 0; i < myQuest.Count; i++)
        {
            if (myQuest[i].ID == quest.ID)
            {
                if (i < currentQuestID)
                {
                    currentQuestID--;
                    myQuest.Remove(quest);
                    return;
                }
                else
                {
                    myQuest.Remove(quest);
                    return;
                }
            }
        }
    }

    //public void ChangeCurrentQuest(string id)
    //{

    //}

    public void ChangeCurrentQuest(bool toLeft)
    {
        if (toLeft)
        {
            if (currentQuestID > 0)
            {
                currentQuestID--;
            }
            else
            {
                currentQuestID = myQuest.Count - 1;
            }
        }
        else
        {
            if (currentQuestID < myQuest.Count - 1)
            {
                currentQuestID++;
            }
            else
            {
                currentQuestID = 0;
            }
        }
        currentQuestNameTMP.text = myQuest[currentQuestID].questName;
    }

    public void Restart()
    {
        speed = 10;
        score = 0;
    }

    public void SnakeCanSpeedUp(bool canSpeedUp)
    {
        snake.canSpeedUp = canSpeedUp;
    }

    public void ExtendBody(int bodies)
    {
        score += bodies;
        if (score >= goal)
        {
            GameWin();
        }
        snake.ExtendBody(bodies);
    }

    public void AddToUI(string pickable)
    {
        switch (pickable)
        {
            case "Apple":
                apple++;
                break;
                //   case "Wood":
                //  wood++;
                // UIPanel.UpdateWood(wood);
                //     break;
        }
    }

    public void ShowMiniMapMark(Vector3 targetPosi)
    {
        miniMapMark.StartShowMark(targetPosi);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        canvas.GameOver();
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        canvas.GameWin();
    }

    public void DamageSnake(float damage)
    {
        snake.GotDamage(damage);
    }
    //public void RestoreEnergy(float amount)
    //{
    //    snake.RestoreEnergy(amount);
    //}
    public void RestoreSnakeEnergy(float amount)
    {
        snake.RestoreEnergy(amount);
    }

    public void RestoreSnakeHealth(float health)
    {
        snake.RestoreHealth(health);
    }

    public void WeaponChanged(List<Transform> equipments, int current)
    {
        UIPanel.WeaponChanged(equipments, current);
    }
}
