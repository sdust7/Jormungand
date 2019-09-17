using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
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
        applePrefab = Resources.Load<GameObject>("Prefabs/Apple");
        woodPrefab = Resources.Load<GameObject>("Prefabs/Wood");
        woodInVoid = GameObject.Find("WoodInVoid").transform;
        allWoods = GameObject.Find("Woods").transform;
        appleInVoid = GameObject.Find("AppleInVoid").transform;
        allApple = GameObject.Find("Apples").transform;
        snake = GameObject.Find("Head").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (transform.name)
        {
            case "AXE":
                if (collision.transform.name == "Trunk")
                {
                    if (collision.transform.parent.GetComponent<SpriteRenderer>().enabled)
                    {
                        collision.transform.GetComponent<CircleCollider2D>().enabled = false;
                        GameObject wood;
                        float x;
                        float y;
                        if (woodInVoid.childCount!=0)
                        {
                             wood = woodInVoid.GetChild(0).gameObject;
                            wood.gameObject.SetActive(true);
                            wood.transform.position = snake.position + snake.up*5;
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

                        for (int n = 0; n < Random.Range(0, 3); n++)
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

                        collision.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
                        
                    }

                }
                break;
        }
    }
}
