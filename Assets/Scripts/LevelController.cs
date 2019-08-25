using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private UIController canvas;
    private UIController UIPanel;
    private SnakeController snake;
    public List<Vector3> usedPoints;
    public float speed;
    public int score;
    public int goal;
    public int wood;
    public int apple;

    public float xValueStartDesert = 160.0f; 

    void Awake()
    {
        wood = 0;
        apple = 0;
        usedPoints = new List<Vector3>();
        usedPoints.Add(new Vector3(0, 0, 0));
        canvas = GameObject.Find("Canvas").GetComponent<UIController>();
        UIPanel = GameObject.Find("UIPanel").GetComponent<UIController>();

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
                UIPanel.addWood(wood);
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
