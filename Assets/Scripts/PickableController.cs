using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    private LevelController lvControl;
    private Transform snake;
    private Transform woodInVoid;
    private Transform appleInVoid;


    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        snake = GameObject.Find("SnakeHead").transform;
        woodInVoid = GameObject.Find("WoodInVoid").transform;
        appleInVoid = GameObject.Find("AppleInVoid").transform;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (transform.tag)
        {
            case "Apple":
                if (col.tag == "Snake")
                {
                    lvControl.ExtendBody(1);
                    lvControl.AddToUI("Apple");
                    transform.gameObject.SetActive(false);
                    transform.SetParent(appleInVoid);
                }
                /*
                if (col.tag == "Snake")
                {
                    lvControl.ExtendBody(1);
                    int x = Random.Range(-10, 10);
                    int y = Random.Range(-10, 10);
                    transform.position = new Vector2(x + snake.position.x, y + snake.position.y);
                }
                else if (col.tag == "Deadly")
                {
                    int x = Random.Range(-10, 10);
                    int y = Random.Range(-10, 10);
                    transform.position = new Vector2(x + snake.position.x, y + snake.position.y);
                }*/

                break;
            case "Wood": 
                if(col.tag == "Snake")
                {
                    transform.gameObject.SetActive(false);
                    transform.SetParent(woodInVoid);
                    lvControl.AddToUI("Wood");
                }
                break;
        }
       
    }

   
}
