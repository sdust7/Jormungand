﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obstacles
{
    Tree = 0,
    Stone = 1,
    Catcus = 2,
    SandStone = 3,
}

public class ObstacleController : MonoBehaviour
{
    public Obstacles thisObstacle;

    private GameObject treeTop;

    private GameObject applePrefab;
    private GameObject woodPrefab;
    private Transform snake;
    private Transform woodInVoid;
    private Transform appleInVoid;
    private Transform allWoods;
    private Transform allApple;

    // Start is called before the first frame update
    void Start()
    {
        switch (thisObstacle)
        {
            case Obstacles.Tree:
                treeTop = transform.parent.gameObject;
                snake = GameObject.Find("Head").transform;

                woodPrefab = Resources.Load<GameObject>("Prefabs/Wood");
                applePrefab = Resources.Load<GameObject>("Prefabs/Apple");

                woodInVoid = GameObject.Find("WoodInVoid").transform;
                allWoods = GameObject.Find("Woods").transform;

                appleInVoid = GameObject.Find("AppleInVoid").transform;
                allApple = GameObject.Find("Apples").transform;

                break;

        }


    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.gameObject.tag);
        switch (thisObstacle)
        {
            case Obstacles.Tree:
                if (collision.collider.gameObject.tag == "AXE" || collision.gameObject.tag == "Firework")
                {
                    //Debug.Log("111");
                    if (treeTop.GetComponent<SpriteRenderer>().enabled)
                    {
                        //Debug.Log("2222");
                        GetComponent<CircleCollider2D>().enabled = false;
                        GameObject wood;
                        float x, y;
                        if (woodInVoid.childCount != 0)
                        {
                            wood = woodInVoid.GetChild(0).gameObject;
                            wood.gameObject.SetActive(true);
                            wood.transform.position = snake.position + snake.up * 5;
                            wood.transform.Rotate(0, 0, Random.Range(0, 360));
                            wood.transform.SetParent(allWoods);
                        }
                        else
                        {
                            wood = Instantiate(woodPrefab);
                            wood.transform.position = snake.position + snake.up * 5;
                            wood.transform.Rotate(0, 0, Random.Range(0, 360));
                            wood.transform.SetParent(allWoods);
                        }
                        for (int i = 0; i < Random.Range(0, 3); i++)
                        {
                            x = Random.Range(-5.0f, 5.0f);
                            y = Random.Range(-5.0f, 5.0f);
                            if (appleInVoid.childCount != 0)
                            {
                                GameObject apple = appleInVoid.GetChild(0).gameObject;
                                apple.gameObject.SetActive(true);
                                apple.transform.position = new Vector3(wood.transform.position.x + x, wood.transform.position.y + y, 0);
                                apple.transform.Rotate(0, 0, Random.Range(0, 360));
                                apple.transform.SetParent(allApple);
                            }
                            else
                            {
                                GameObject apple = Instantiate(applePrefab);
                                apple.transform.position = new Vector3(wood.transform.position.x + x, wood.transform.position.y + y, 0);
                                apple.transform.Rotate(0, 0, Random.Range(0, 360));
                                apple.transform.SetParent(allApple);
                            }
                        }
                        treeTop.GetComponent<SpriteRenderer>().enabled = false;
                    }




                }


                break;

        }


    }
}
