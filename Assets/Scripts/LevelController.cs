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
    public float speed;
    public int score;
    public int goal;
    public int wood;
    public int apple;
    public QuestController questController;
    public List<Quest> myQuest;

    public float xValueStartDesert = 160.0f;
    public float xValueStartSea = -160.0f;

    void Awake()
    {
        myQuest = new List<Quest>();
        questController = new QuestController();
        questController.LoadMission();
        wood = 4;
        apple = 0;
        usedPoints = new List<Vector3>();
        usedPoints.Add(new Vector3(0, 0, 0));
        canvas = GameObject.Find("Canvas").GetComponent<UIController>();
        UIPanel = GameObject.Find("UIPanel").GetComponent<UIController>();
        questPanel = GameObject.Find("MissionPanel").transform.GetChild(0).transform;

        snake = GameObject.Find("SnakeHead").GetComponent<SnakeController>();
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
            case "0":
                if (wood>=5)
                {
                    wood -= 5;
                    UIPanel.UpdateWood(wood);
                    GameObject reward = Instantiate(Apples) as GameObject;
                    reward.transform.position = snake.transform.position + new Vector3(2, 2,0);
                    Quest quest = questController.allQuest.Find(x => x.ID.Equals(id));
                    myQuest.Remove(quest);
                }
                else
                {

                }
                break;
            case "1":
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
        questPanel.GetChild(2).GetComponent<TextMeshProUGUI>().text = quest.description+"\nReward: "+quest.reward;

    }

    public void Restart()
    {
        speed = 10;
        score = 0;
    }

    public void ExtendBody(int bodies)
    {
        score+=bodies;
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
            case "Wood":
                wood++;
                UIPanel.UpdateWood(wood);
                break;
        }
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


}
