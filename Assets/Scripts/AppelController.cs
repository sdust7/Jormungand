using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppelController : MonoBehaviour
{
    private LevelController lvControl;
    private Transform snake;
    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        snake = GameObject.Find("SnakeHead").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Snake")
        {
            lvControl.ExtendBody(1);
            int x = Random.Range(-10,10);
            int y = Random.Range(-10,10);
            transform.position = new Vector2(x+snake.position.x, y+snake.position.y);
        }else if (col.tag == "Deadly")
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);
            transform.position = new Vector2(x + snake.position.x, y + snake.position.y);
        }
    }

   
}
