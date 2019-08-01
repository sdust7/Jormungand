using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    // Player
    [SerializeField]
    private SnakeController snake;
    private MainUI canvas;
    // current score
    private int score;

    public float threhold;




    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        BindGameObjects();
    }
    
    // Update is called once per frame
    public void BindGameObjects()
    {
        score = 0;
        threhold = 1.0f;
        snake = GameObject.Find("SnakeBody").GetComponent<SnakeController>();
        canvas = GameObject.Find("Canvas").GetComponent<MainUI>();
    }

    public void GameOver()
    {
        snake.GameOver();
        canvas.GameOver();
    }

    public void GetApple()
    {
        score++;
        if (threhold > 0.3f)
        {
            threhold -= 0.05f;
        }
        snake.extendBody();
    }
}
