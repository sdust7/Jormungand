using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour
{
    private const float accelerateCost = 10.0f;
    private const float excuteTimesPerSecond = 1 / 50.0f;
    private const float barLength = 315.0f;
    private int length;
    private LevelController lvControl;
    private GameObject bodyPrefab;
    private Transform allBody;
    private Transform firstBody;
    private Transform snake;
    private RectTransform energyBar;

    private RectTransform healthBar;

    private float movingSpeed;
    private float steeringSpeed;

    private float currentEnergy;
    private float maxEnergy;

    private float currentHealth;
    private float maxHealth;


    private Rigidbody2D rigi;
    float timer;
  
    // Start is called before the first frame update
    void Start()
    {
        snake = transform.parent;
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        movingSpeed = lvControl.speed;
        steeringSpeed = 10.0f;

        maxEnergy = 100.0f;
        currentEnergy = maxEnergy;

        maxHealth = 100.0f;
        currentHealth = maxHealth;
        healthBar = GameObject.Find("Health").GetComponent<RectTransform>();
        energyBar = GameObject.Find("Energy").GetComponent<RectTransform>();
        bodyPrefab = Resources.Load<GameObject>("Prefabs/Body");
        allBody = GameObject.Find("SnakeBody").transform;
        rigi = snake.GetComponent<Rigidbody2D>();
        length = 20;

        for (int n = 0; n < length; n++)
        {
            GameObject newBody =  Instantiate(bodyPrefab,allBody);
            newBody.transform.position = new Vector2(snake.position.x, snake.position.y);
        }
        firstBody = allBody.GetChild(0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        rigi.velocity = snake.up*movingSpeed;
        firstBody.position = snake.position;
        //firstBody.GetComponent<Rigidbody2D>().velocity = rigi.velocity;
        for (int n = length-1; n > 0; n--)
        {
            allBody.GetChild(n).transform.position =allBody.GetChild(n-1).transform.position;
            //allBody.GetChild(n).GetComponent<Rigidbody2D>().velocity = allBody.GetChild(n - 1).GetComponent<Rigidbody2D>().velocity;
        }
        AbilitiesDetection();
        MovementDetection();
      
    }

    public void GotDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.anchoredPosition = new Vector2(healthBar.anchoredPosition.x - (damage * barLength/maxEnergy), 0);

    }

    public void ExtendBody(int bodies)
    {
        for (int n = 0; n < 5*bodies; n++)
        {
            GameObject newBody = Instantiate(bodyPrefab, allBody);
            newBody.transform.position = new Vector2(allBody.GetChild(length-1).position.x, allBody.GetChild(length - 1).position.y);
        }
        length += 5*bodies;
    }

    private void RecoverEnergy(float value)
    {
        currentEnergy += value;
    }

    private void AbilitiesDetection()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentEnergy >= 5.0f)
            {
                movingSpeed = 20.0f;
                steeringSpeed = 10.0f;
                currentEnergy -= excuteTimesPerSecond * accelerateCost;
                energyBar.anchoredPosition = new Vector2(energyBar.anchoredPosition.x - (excuteTimesPerSecond * accelerateCost * (barLength / maxEnergy)), 0);


            }
            else
            {
                movingSpeed = 10.0f;
                steeringSpeed = 10.0f;
            }
        }
        else
        {
            movingSpeed = 10.0f;
            steeringSpeed = 10.0f;
        }

    }

    private void MovementDetection()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.fixedDeltaTime;
            if (timer <= 0.5f)
            {
                snake.Rotate(0, 0, steeringSpeed * timer);
            }
            else
            {
                snake.Rotate(0, 0, 5);

            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            timer += Time.fixedDeltaTime;
            if (timer <= 0.5f)
            {
                snake.Rotate(0, 0, -steeringSpeed * timer);
            }
            else
            {
                snake.Rotate(0, 0, -5);

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                GotDamage(5.0f);
                break;
            case "Sheep":
                collision.transform.GetComponent<SheepController>().CollideWithSnake();
                break;
            default:
                break;
        }
    }
}
