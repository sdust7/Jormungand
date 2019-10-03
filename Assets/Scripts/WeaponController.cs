using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Equipments
{
    Empty = 0,
    Axe = 1,
    FireworkStand = 2
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

    //private Transform fireworkInVoid;

    public Equipments thisEquipmentIs;
    public float fireworkReloadTime;
    // public float fireworkCoolDownTime;
    private float fireworkReloadTimer;
    private float fireworkReloadTimer2;
    // private float fireworkCDTimer;

    [SerializeField]
    private Transform leftFirework;
    [SerializeField]
    private Transform rightFirework;

    // Start is called before the first frame update
    void Start()
    {
        snake = GameObject.Find("Head").transform;

        woodPrefab = Resources.Load<GameObject>("Prefabs/Wood");
        applePrefab = Resources.Load<GameObject>("Prefabs/Apple");

        woodInVoid = GameObject.Find("WoodInVoid").transform;
        allWoods = GameObject.Find("Woods").transform;

        appleInVoid = GameObject.Find("AppleInVoid").transform;
        allApple = GameObject.Find("Apples").transform;


        if (thisEquipmentIs == Equipments.FireworkStand)
        {
            //fireworkInVoid = GameObject.Find("FireworkInVoid").transform;
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    transform.GetChild(0).GetComponent<FireworkController>().fireworkInVoid = fireworkInVoid;
            //}
            // fireworkCDTimer = fireworkCoolDownTime;
            fireworkReloadTimer = 0;
            fireworkReloadTimer2 = 0;

            leftFirework = transform.GetChild(0);
            rightFirework = transform.GetChild(1);
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (thisEquipmentIs == Equipments.FireworkStand)
        {
            FireworkActions();
        }
    }

    void FixedUpdate()
    {
        //if (thisEquipmentIs == Equipments.FireworkStand)
        //{
        //    FireworkActions();
        //}
    }

    private void FireworkActions()
    {
        if (transform.childCount == 0)
        {
            fireworkReloadTimer += Time.deltaTime;
            fireworkReloadTimer2 += Time.deltaTime;
        }
        else if (transform.childCount == 1)
        {
            if (transform.GetChild(0) == leftFirework)
            {
                fireworkReloadTimer2 += Time.deltaTime;
            }
            else
            {
                fireworkReloadTimer += Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (transform.childCount == 2)
            {
                leftFirework.GetComponent<FireworkController>().Launch();
            }
            else if (transform.childCount == 1)
            {
                transform.GetChild(0).GetComponent<FireworkController>().Launch();
                //if (transform.GetChild(0) == leftFirework)
                //{
                //    leftFirework.GetComponent<FireworkController>().Launch();
                //}
                //else
                //{
                //    rightFirework.GetComponent<FireworkController>().Launch();
                //}
            }
        }

        if (fireworkReloadTimer >= fireworkReloadTime)
        {
            leftFirework.GetComponent<FireworkController>().Reuse(-1);
            fireworkReloadTimer = 0;
        }
        if (fireworkReloadTimer2 >= fireworkReloadTime)
        {
            rightFirework.GetComponent<FireworkController>().Reuse(1);
            fireworkReloadTimer2 = 0;
        }

    }




    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //  switch (transform.name)
    //    switch (thisEquipmentIs)
    //    {
    //        //case "AXE":
    //        case Equipments.Axe:
    //            if (collision.transform.name == "Trunk")
    //            {
    //                if (collision.transform.parent.GetComponent<SpriteRenderer>().enabled)
    //                {
    //                    collision.transform.GetComponent<CircleCollider2D>().enabled = false;
    //                    GameObject wood;
    //                    float x;
    //                    float y;
    //                    if (woodInVoid.childCount != 0)
    //                    {
    //                        wood = woodInVoid.GetChild(0).gameObject;
    //                        wood.gameObject.SetActive(true);
    //                        wood.transform.position = snake.position + snake.up * 5;
    //                        wood.transform.Rotate(0, 0, Random.Range(0, 360));
    //                        wood.transform.SetParent(allWoods);
    //                    }
    //                    else
    //                    {
    //                        wood = Instantiate(woodPrefab);
    //                        wood.transform.position = snake.position + snake.up * 5;
    //                        wood.transform.Rotate(0, 0, Random.Range(0, 360));
    //                        wood.transform.SetParent(allWoods);

    //                    }

    //                    for (int n = 0; n < Random.Range(0, 3); n++)
    //                    {
    //                        x = Random.Range(-5.0f, 5.0f);
    //                        y = Random.Range(-5.0f, 5.0f);
    //                        if (appleInVoid.childCount != 0)
    //                        {
    //                            GameObject apple = appleInVoid.GetChild(0).gameObject;
    //                            apple.gameObject.SetActive(true);
    //                            apple.transform.position = new Vector3(wood.transform.position.x + x, wood.transform.position.y + y, 0);
    //                            apple.transform.Rotate(0, 0, Random.Range(0, 360));
    //                            apple.transform.SetParent(allApple);
    //                        }
    //                        else
    //                        {
    //                            GameObject apple = Instantiate(applePrefab);
    //                            apple.transform.position = new Vector3(wood.transform.position.x + x, wood.transform.position.y + y, 0);
    //                            apple.transform.Rotate(0, 0, Random.Range(0, 360));
    //                            apple.transform.SetParent(allApple);


    //                        }
    //                    }

    //                    collision.transform.parent.GetComponent<SpriteRenderer>().enabled = false;

    //                }

    //            }
    //            break;
    //    }
    //}
}
