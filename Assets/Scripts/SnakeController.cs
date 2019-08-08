using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private int length;
    private GameObject bodyPrefab;
    private Transform allBody;
    private Transform firstBody;

    private Rigidbody2D rigi;
    float timer;
  
    // Start is called before the first frame update
    void Start()
    {
        bodyPrefab = Resources.Load<GameObject>("Prefabs/Body");
        allBody = GameObject.Find("SnakeBody").transform;
        rigi = transform.GetComponent<Rigidbody2D>();
        length = 20;
        for(int n = 0; n < length; n++)
        {
            GameObject newBody =  Instantiate(bodyPrefab,allBody);
            newBody.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        firstBody = allBody.GetChild(0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rigi.velocity = transform.up*10;
        firstBody.position = transform.position;
        for (int n = length-1; n > 0; n--)
        {
            allBody.GetChild(n).transform.position =allBody.GetChild(n-1).transform.position;
        }
        MovementDetection();
      
    }

    public void GetApple()
    {
        for (int n = 0; n < 5; n++)
        {
            GameObject newBody = Instantiate(bodyPrefab, allBody);
            newBody.transform.position = new Vector2(allBody.GetChild(length-1).position.x, allBody.GetChild(length - 1).position.y);
        }
        length += 5;

    }

    private void MovementDetection()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.fixedDeltaTime;
            if (timer <= 0.5f)
            {
                transform.Rotate(0, 0, 10 * timer);
            }
            else
            {
                transform.Rotate(0, 0, 5);

            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            timer += Time.fixedDeltaTime;
            if (timer <= 0.5f)
            {
                transform.Rotate(0, 0, -10 * timer);
            }
            else
            {
                transform.Rotate(0, 0, -5);

            }

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            timer = 0;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            timer = 0;
        }
    }
}
