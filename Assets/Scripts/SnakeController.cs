using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private GameObject bodyPrefabs;
    private GlobalController global;
    private Transform firstBody;
    private Transform head;

    private Vector3 direction;
    private Vector3 preDirection;
    private int length;

    private float timer;

    private bool gameOver;


    //私货
    // Direction of snake at the beginning
    public Vector3 startDirection = Vector3.forward;



    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("GlobalController").GetComponent<GlobalController>();
        head = GameObject.Find("Head").transform;
        bodyPrefabs = Resources.Load("Prefabs/Body") as GameObject;

        gameOver = false;
        length = 3;
        direction = startDirection;

        for (int n = 0; n < length; n++)
        {
            GameObject body = Instantiate(bodyPrefabs, transform);
            body.transform.position = new Vector3(head.position.x, head.position.y, head.position.z - (n + 1));
            if (n == 0)
            {
                body.name = "FirstBody";
            }
        }
        firstBody = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

        

        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (firstBody.position.z <= head.position.z)
                {
                    direction = Vector3.forward;
                    head.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (firstBody.position.z >= head.position.z)
                {
                    direction = Vector3.back;
                    head.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (firstBody.position.x >= head.position.x)
                {
                    direction = Vector3.left;
                    head.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (firstBody.position.x <= head.position.x)
                {
                    direction = Vector3.right;
                    head.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
            if (timer > global.threhold)
            {
                for (int n = length - 1; n > 0; n--)
                {
                    transform.GetChild(n).transform.position = transform.GetChild(n - 1).transform.position;
                }
                firstBody.position = head.position;
                head.position += direction;
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    public void extendBody()
    {
        GameObject body = Instantiate(bodyPrefabs, transform);
        body.transform.position = transform.GetChild(length - 1).position;
        length++;
    }


    public void GameOver()
    {
        gameOver = true;
    }
}
