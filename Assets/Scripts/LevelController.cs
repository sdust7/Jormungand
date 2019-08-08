using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private UIController canvas;
    private SnakeController snake;
    public float speed;
    public int score;
    public int goal;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<UIController>();
        snake = GameObject.Find("SnakeHead").GetComponent<SnakeController>();
        speed = 10;
        score = 0;
        goal = 5;
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

    public void GetApple()
    {
        score++;
        speed++;
        if (score >= goal)
        {
            GameWin();
        }
        snake.GetApple();
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


}
