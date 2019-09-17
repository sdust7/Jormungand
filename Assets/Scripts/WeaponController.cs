using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Equipments
{
    Axe = 0,
    FireworkStand = 1
}


public class WeaponController : MonoBehaviour
{
    private GameObject applePrefab;
    private GameObject woodPrefab;
    private Transform snake;
    private Transform woodInVoid;
    private Transform appleInVoid;
    private Transform allWoods;
    private Transform allApple;

    private Transform fireworkInVoid;

    public Equipments thisEquipmentIs;
    public float fireworkReloadTime;
    public float fireworkCoolDownTime;
    private float fireworkReloadTimer;
    private float fireworkCDTimer;

    private Transform leftFirework;
    private Transform rightFirework;

    // Start is called before the first frame update
    void Start()
    {
        woodPrefab = Resources.Load<GameObject>("Prefabs/Wood");
        applePrefab = Resources.Load<GameObject>("Prefabs/Apple");

        woodInVoid = GameObject.Find("WoodInVoid").transform;
        allWoods = GameObject.Find("Woods").transform;

        appleInVoid = GameObject.Find("AppleInVoid").transform;
        allApple = GameObject.Find("Apples").transform;

        snake = GameObject.Find("Head").transform;

        fireworkInVoid = GameObject.Find("FireworkInVoid").transform;
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(0).GetComponent<FireworkController>().fireworkInVoid = fireworkInVoid;
        //}
        fireworkCDTimer = fireworkCoolDownTime;
        fireworkReloadTimer = 0;

        leftFirework = transform.GetChild(0);
        rightFirework = transform.GetChild(1);
    }

    // Update is called once per frame


    void FixedUpdate()
    {

        if (thisEquipmentIs == Equipments.FireworkStand)
        {
            FireworkActions();
        }

    }

    private void FireworkActions()
    {
        fireworkCDTimer += Time.fixedDeltaTime;

        if (transform.childCount < 2)
        {
            fireworkReloadTimer += Time.fixedDeltaTime;
        }

        if (fireworkCDTimer >= fireworkCoolDownTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (transform.childCount > 0)
                {
                    fireworkCDTimer = 0;
                    fireworkReloadTimer = 0;
                    if (transform.childCount == 2 && transform.GetChild(0) == rightFirework)
                    {
                        leftFirework.GetComponent<FireworkController>().Launch();
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<FireworkController>().Launch();
                    }
                }
            }
        }

        if (fireworkReloadTimer >= fireworkReloadTime)
        {
            if (fireworkInVoid.childCount > 0)
            {
                fireworkReloadTimer = 0;
                if (fireworkInVoid.childCount == 2)
                {
                    leftFirework.GetComponent<FireworkController>().Reuse(-1);
                }
                else
                {

                    if (fireworkInVoid.GetChild(0) == leftFirework)
                    {
                        leftFirework.GetComponent<FireworkController>().Reuse(-1);
                    }
                    else
                    {
                        rightFirework.GetComponent<FireworkController>().Reuse(1);
                    }
                }

                //switch (transform.childCount)
                //{
                //    case 0:
                //        fireworkReloadTimer = 0;
                //        // fireworkInVoid.GetChild(0).gameObject.SetActive(true);
                //        fireworkInVoid.GetChild(0).GetComponent<FireworkController>().Reuse(1);

                //        break;
                //    case 1:
                //        fireworkReloadTimer = 0;
                //        fireworkInVoid.GetChild(0).GetComponent<FireworkController>().Reuse(-1);
                //        //fireworkInVoid.GetChild(0).gameObject.SetActive(true);

                //        break;
                //    case 2:
                //        break;
                //}

            }

        }
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
