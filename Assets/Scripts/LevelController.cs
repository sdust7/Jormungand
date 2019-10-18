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
    public int currentMyQuestIndex;

    public float xValueStartDesert = 160.0f;
    public float xValueStartSea = -160.0f;

    public Vector3[] mannulUsedPoints;
    private bool damaged;
    private float damageCooldown;

    public Vector3 currentCheckPoint = new Vector3(0, 8, 0);

    public GameObject debugPanel;

    void Awake()
    {
        damaged = false;
        damageCooldown = 0;
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

        debugPanel = GameObject.Find("DebugPanel");

        toolBar = GameObject.Find("ToolBar").GetComponent<ToolBar>();

        currentMyQuestIndex = -1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DebugPanelOnOff()
    {
        if (debugPanel.activeSelf)
        {
            debugPanel.SetActive(false);
        }
        else
        {
            debugPanel.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageCooldown += Time.deltaTime;
            if (damageCooldown > 0.5f)
            {
                damageCooldown = 0;
                damaged = false;
            }
        }

    }

    public void SnakeRespawn()
    {
        canvas.GameRestart();
        snake.RespwanSettings(currentCheckPoint);
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
                    // quest.finished = true;
                    // miniMapMark.EndShowMark();
                }
                else
                {

                }
                break;
            case "2":
                if (toolBar.GetItemCount("SheepBone") >= 10)
                {
                    toolBar.SetItemCount("SheepBone", -10);
                    snake.AddEquipment(Equipments.FireworkStand);
                    Quest quest = questController.allQuest.Find(x => x.ID.Equals(id));
                    RemoveQuest(quest);
                    AddQuest(questController.allQuest[3]);
                    // quest.finished = true;
                    //  miniMapMark.EndShowMark();
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

        if (currentMyQuestIndex == myQuest.Count - 1)
        {
            myQuest.Add(newQuest);
            currentMyQuestIndex++;
        }
        else
        {
            Debug.Log(currentMyQuestIndex);

            myQuest.Insert(currentMyQuestIndex + 1, newQuest);
            currentMyQuestIndex++;
        }

        currentQuestNameTMP.text = myQuest[currentMyQuestIndex].questName;
        if (myQuest[currentMyQuestIndex].showMapMark)
        {
            miniMapMark.StartShowMark(myQuest[currentMyQuestIndex].targetTrans.position);
        }
    }

    public void RemoveQuest(Quest quest)
    {
        for (int i = 0; i < myQuest.Count; i++)
        {
            if (myQuest[i].ID == quest.ID)
            {
                if (i <= currentMyQuestIndex)
                {
                    currentMyQuestIndex--;
                    //if (currentMyQuestIndex >= 0 && myQuest[currentMyQuestIndex].showMapMark)
                    //{
                    //    miniMapMark.ChangeMarkPosi(myQuest[currentMyQuestIndex].targetTrans.position);
                    //}
                }
                quest.finished = true;
                myQuest.Remove(quest);
            }
        }
        if (myQuest.Count == 0)
        {
            currentMyQuestIndex = -1;
            currentQuestNameTMP.text = "None";
            miniMapMark.EndShowMark();
        }
        else
        {
            currentQuestNameTMP.text = myQuest[currentMyQuestIndex].questName;
            if (myQuest[currentMyQuestIndex].showMapMark)
            {
                miniMapMark.StartShowMark(myQuest[currentMyQuestIndex].targetTrans.position);
            }
            else
            {
                miniMapMark.EndShowMark();
            }
            //foreach (var item in myQuest)
            //{
            //    if (item.showMapMark)
            //    {
            //        return;
            //    }
            //}
            ////currentQuestNameTMP.text = "None";
            //miniMapMark.EndShowMark();
            //return;
        }
    }

    public void ChangeCurrentQuest(bool toLeft)
    {
        if (currentMyQuestIndex >= 0)
        {
            if (toLeft)
            {
                if (currentMyQuestIndex > 0)
                {
                    currentMyQuestIndex--;
                }
                else
                {
                    currentMyQuestIndex = myQuest.Count - 1;
                }
            }
            else
            {
                if (currentMyQuestIndex < myQuest.Count - 1)
                {
                    currentMyQuestIndex++;
                }
                else
                {
                    currentMyQuestIndex = 0;
                }
            }
            currentQuestNameTMP.text = myQuest[currentMyQuestIndex].questName;
            if (myQuest[currentMyQuestIndex].showMapMark)
            {
                miniMapMark.StartShowMark(myQuest[currentMyQuestIndex].targetTrans.position);
            }
        }
    }

    //public void Restart()
    //{

    //    //speed = 10;
    //    //score = 0;
    //}

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
        canvas.GameOver();
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        canvas.GameWin();
    }

    public void DamageSnake(float damage)
    {
        if (!damaged)
        {
            snake.GotDamage(damage);
            damaged = true;
        }
    }

    public bool RestoreSnakeEnergy(float amount)
    {
        if (!snake.EnergyIsfull())
        {
            snake.RestoreEnergy(amount);
            return true;
        }
        return false;
    }

    public bool RestoreSnakeHealth(float health)
    {
        if (!snake.HealthIsfull())
        {
            snake.RestoreHealth(health);
            return true;
        }
        return false;
    }

    public void WeaponChanged(List<Transform> equipments, int current)
    {
        UIPanel.WeaponChanged(equipments, current);
    }
}
